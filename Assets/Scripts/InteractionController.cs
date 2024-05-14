using System.Collections;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider other)
    {



    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log(other.gameObject.name);

                //Object anim
                Animator anim = this.GetComponentInParent<Animator>();
                anim.SetTrigger("jump");

                StartCoroutine(DeactivatePlayer(other.gameObject));

                //"subit al coche", salto personaje y desaparecer
                //activar cámaras del coche
            }
        }

    }

    IEnumerator DeactivatePlayer(GameObject vehicle)
    {
        yield return new WaitForSeconds(1);

        player.gameObject.SetActive(false);

        //Set player as child so it travels with the vehicle
        // player.transform.parent = vehicle.transform;

        //Activar coche
        GameManager.currentVehicle = vehicle;
        GameManager.SetControllerStatus(vehicle, true);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
