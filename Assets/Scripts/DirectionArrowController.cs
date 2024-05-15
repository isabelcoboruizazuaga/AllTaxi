using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrowController : MonoBehaviour
{
    [SerializeField] private bool helicopterPointer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!helicopterPointer)
        {
            if (GameManager.currentTaxiBeacon != null)
            {
                transform.LookAt(GameManager.currentTaxiBeacon.transform);
            }
        }
        else
        {
            if (GameManager.currentHelicopterBeacon != null)
            {
                transform.LookAt(GameManager.currentHelicopterBeacon.transform);
            }

        }
       
    }
}
