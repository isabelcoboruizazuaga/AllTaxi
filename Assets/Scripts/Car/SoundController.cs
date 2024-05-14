using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource sonidoMotor, sonidoPito;
    private Rigidbody cocheRb;
    private CarController cocheGo;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        cocheRb = GetComponent<Rigidbody>();
        cocheGo = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cocheGo.arrancado)
        {
            if(!sonidoMotor.enabled)
            {
                sonidoMotor.enabled = true;
            }
        }
        else
        {
            if(sonidoMotor.enabled)
            {
                sonidoMotor.enabled = false;
            }
        }
        speed = cocheRb.velocity.magnitude;
        sonidoMotor.pitch = (speed / 10) + 0.4f;
        sonidoMotor.volume = (speed / 10) + 0.3f;

        if (Input.GetKeyDown(KeyCode.C))
        {
            sonidoPito.Play();
        }
    }
}
