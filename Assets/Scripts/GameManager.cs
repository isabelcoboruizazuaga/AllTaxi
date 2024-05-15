using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] vehicles;
    private GameObject player;

    public static GameObject currentVehicle;



    [SerializeField] private GameObject beacon;
    [SerializeField] private GameObject beacon2;

    private RandomPointGenerator randomPointGenerator = new RandomPointGenerator(265,225,-23,-23); //z -23, 225 y -23 265

    /*
        * Apareces: Generación destino aleatorio
        * Se marca destino y vehiculo más cercano
        * Llegas destino: siguiente destino marcado
        * En cada destino: posibilidad de destino de helicóptero extra
        *  (si hay destinoH se marca el helicoptero)
        * Taxi normal: 20$ por viaje
        * Helicoptero: 80$ por viaje
        * 
        * Romper coche: medidor daño 5 golpes. 5 golpes-> coche dep, usa otro (50$), si dineo 0$ pierdes
        */

    // Start is called before the first frame update
    void Start()
    {
        //Find vehicles
        vehicles = GameObject.FindGameObjectsWithTag("Vehicle");

        //Deactivate controlls
        DeactivateVehicles();

        //GFind player and activate its controlls
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Cameras>().ControlCameras(true);

        //Generates first destiny point
        NextDestination();
    }

    public void NextDestination()
    {
        Vector3 beaconPoint = randomPointGenerator.GetTaxiPoint();
        Instantiate(beacon, beaconPoint, Quaternion.identity);
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


    // Update is called once per frame
    void Update()
    {
        //Getting out a vehicle
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.SetActive(true);

            //The player is placed on the player spawn
            player.transform.position = currentVehicle.transform.GetChild(0).gameObject.transform.position;
            player.transform.rotation = currentVehicle.transform.GetChild(0).gameObject.transform.rotation;

            SetControllerStatus(currentVehicle, false);
        }
    }
}
