using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrowController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.currentTaxiBeacon != null)
        {
            transform.LookAt(GameManager.currentTaxiBeacon.transform);
        }
    }
}
