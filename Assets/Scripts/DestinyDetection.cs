using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinyDetection : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject directionArrow;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        directionArrow.SetActive(true);
    }
    private void OnDisable()
    {
        directionArrow.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Beacon"))
        {
            gameManager.NextDestination();
            Destroy(other.gameObject);
            GameManager.money+=20;
        }

    }
}
