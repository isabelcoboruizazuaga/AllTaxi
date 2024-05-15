using System.Collections;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private GameObject player;
   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
        {
            if (Input.GetKey(KeyCode.E))
            {

                //Object anim
                Animator anim = this.GetComponentInParent<Animator>();
                anim.SetTrigger("jump");

                StartCoroutine(DeactivatePlayer(other.gameObject));
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

    // Update is called once per frame
    void Update()
    {

    }
}
