using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movimientos Básicos
    public float velocidadMovimiento = 5f;
    public float velocidadRotacion = 100f;

    public float x, y;

    public float velocidadInicial;
    public float sprint = 1;



    // Start is called before the first frame update
    void Start()
    {

        //Agachado
        velocidadInicial = velocidadMovimiento;

    }

    // Update is called once per frame
    void Update()
    {


        //Lectura cursores
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprint = 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprint = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
        }
    }

    //Para las fisicas
    private void FixedUpdate()
    {

        //Movimiento
        transform.Rotate(0, x * Time.fixedDeltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.fixedDeltaTime * velocidadMovimiento * sprint);
    }


}
