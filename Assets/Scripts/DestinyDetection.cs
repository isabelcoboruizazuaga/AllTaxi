using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinyDetection : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Beacon"))
        {
            gameManager.NextDestination();

        }

    }
}
