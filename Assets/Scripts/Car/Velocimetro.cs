using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Velocimetro : MonoBehaviour
{
    public Image aguja;
    public Rigidbody coche;

    public float ajusteAguja;
    public float speed;

    public TextMeshProUGUI speedText;
    // Start is called before the first frame update
    void Start()
    {
        ajusteAguja = 245;
    }

    // Update is called once per frame
    void Update()
    {
        speed= coche.velocity.magnitude;
        aguja.transform.eulerAngles= new Vector3 (0,0,0 + speed*-3+ajusteAguja);

        speedText.text=((int)speed *4).ToString ();
    }
}
