using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private RandomPointGenerator randomPointGenerator = new RandomPointGenerator(265, 225, -23, -23); //z -23, 225 y -23 265
    private GameObject[] vehicles;
    private GameObject player;

    public static GameObject currentVehicle;
    public static GameObject currentTaxiBeacon;
    public static GameObject currentHelicopterBeacon;
    public static int money = 0;

    [SerializeField] private GameObject beacon;
    [SerializeField] private GameObject beaconHelicopter;
    [SerializeField] private GameObject textHelicopter;
    [SerializeField] private TextMeshProUGUI textTime;

    [SerializeField] private GameObject textMoneyObject;
    private static TextMeshProUGUI textMoney;

    private float totalTime = 300;


    /*
        * Apareces: Generaci�n destino aleatorio
        * Se marca destino y vehiculo m�s cercano
        * Llegas destino: siguiente destino marcado
        * En cada destino: posibilidad de destino de helic�ptero extra
        *  (si hay destinoH se marca el helicoptero)
        * Taxi normal: 20$ por viaje
        * Helicoptero: 80$ por viaje
        * 
        * Romper coche: medidor da�o 5 golpes. 5 golpes-> coche dep, usa otro (50$), si dineo 0$ pierdes
        */

    // Start is called before the first frame update
    void Start()
    {
        //Find vehicles
        vehicles = GameObject.FindGameObjectsWithTag("Vehicle");

        //Deactivate controlls
        DeactivateVehicles();
        textHelicopter.SetActive(false);

        //GFind player and activate its controlls
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Cameras>().ControlCameras(true);

        //Generates first destiny point
        NextDestination();

        textMoney = textMoneyObject.GetComponent<TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {
        if (currentVehicle != null)
        {
            //Getting out a vehicle
            if (Input.GetKeyDown(KeyCode.F))
            {
                player.SetActive(true);

                //The player is placed on the player spawn
                player.transform.position = currentVehicle.transform.GetChild(0).gameObject.transform.position;
                player.transform.rotation =Quaternion.identity;

                SetControllerStatus(currentVehicle, false);
                currentVehicle = null;
            }
        }

        if (totalTime > 0)
        {
            // Subtract elapsed time every frame
            totalTime -= Time.deltaTime;

            // Divide the time by 60
            float minutes = Mathf.FloorToInt(totalTime / 60);

            // Returns the remainder
            float seconds = Mathf.FloorToInt(totalTime % 60);

            // Set the text string            
            textTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            totalTime = 0;
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void NextDestination()
    {
        Vector3 beaconPoint = randomPointGenerator.GetTaxiPoint();
        currentTaxiBeacon = Instantiate(beacon, beaconPoint, Quaternion.identity);

        if (currentHelicopterBeacon == null)
        {
            beaconPoint = randomPointGenerator.GetHelicopterPoint();
            if (beaconPoint != Vector3.zero)
            {
                currentHelicopterBeacon = Instantiate(beaconHelicopter, beaconPoint, Quaternion.identity);

                textHelicopter.SetActive(true);

            }
        }

    }

    public void CleanHelicopterBeacon()
    {
        currentHelicopterBeacon = null;
    }

    public void DeactivateHelicopterText()
    {
        textHelicopter.SetActive(false);
    }
    public static void UpdateMoney()
    {
        textMoney.text = "Money earned: " + money + "$";
    }

    private void DeactivateVehicles()
    {
        //Finds all vehicles and deactivates its controlls
        for (int i = 0; i < vehicles.Length; i++)
        {
            var vehicle = vehicles[i];
            SetControllerStatus(vehicle, false);
        }

    }

    public static void ChangeVehicle(GameObject newVehicle)
    {
        SetControllerStatus(currentVehicle, false);

        currentVehicle = newVehicle;
        SetControllerStatus(newVehicle, true);
    }

    //Given a vehicle game objects set its controllers active or inactive
    public static void SetControllerStatus(GameObject gotoActivate, bool status)
    {
        gotoActivate.GetComponent<Cameras>().ControlCameras(status);

        if (status) { gotoActivate.GetComponentInChildren<AudioSource>().Play(); }
        else
        {
            gotoActivate.GetComponentInChildren<AudioSource>().Stop();
        }

        //Activate or deactivate camera controller
        gotoActivate.GetComponent<Cameras>().enabled = status;

        //Activate or deactivate destiny detection
        gotoActivate.GetComponentInChildren<DestinyDetection>().enabled = status;

        if (gotoActivate.GetComponent<CarControllerTest>() != null)
        {
            gotoActivate.GetComponent<CarControllerTest>().enabled = status;
        }
        else
        {
            if (gotoActivate.GetComponent<HelicopterController>() != null)
            {
                gotoActivate.GetComponent<HelicopterController>().enabled = status;
            }
        }
    }

}
