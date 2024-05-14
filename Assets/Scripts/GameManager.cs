using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] vehicles;
    private GameObject player;
    [SerializeField] public static GameObject currentVehicle;
// Start is called before the first frame update
void Start()
    {
        vehicles = GameObject.FindGameObjectsWithTag("Vehicle");

       DeactivateVehicles();

        player= GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Cameras>().ActivateCamera();

    }
    private void DeactivateVehicles()
    { 
        //Finds all vehicles and deactivates its controlls
        for (int i = 0; i < vehicles.Length; i++)
        {
            var vehicle = vehicles[i];

            if (vehicle.GetComponent<CarControllerTest>() != null)
            {
                vehicle.GetComponent<CarControllerTest>().enabled = false;
                vehicle.GetComponent<Cameras>().enabled = false;
            }
            else
            {
                if (vehicle.GetComponent<HelicopterController>() != null)
                {
                    vehicle.GetComponent<HelicopterController>().enabled = false;
                    vehicle.GetComponent<Cameras>().enabled = false;
                }
            }
        }

    }

    //Given a vehicle game objects set its controllers active or inactive
    public static void SetControllerStatus( GameObject gotoActivate, bool status)
    {
        gotoActivate.GetComponent<Cameras>().enabled = status;
        gotoActivate.GetComponent<Cameras>().ActivateCamera();

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
        if(Input.GetKeyDown(KeyCode.F)) {
            player.SetActive(true);
            //The player is placed on the player spawn
            player.transform.position=currentVehicle.transform.GetChild(0).gameObject.transform.position;
            player.transform.rotation=currentVehicle.transform.GetChild(0).gameObject.transform.rotation;

            SetControllerStatus(currentVehicle, false);
        }
    }
}
