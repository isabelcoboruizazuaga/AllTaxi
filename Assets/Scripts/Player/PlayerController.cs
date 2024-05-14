using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movimientos Básicos
    public float velocidadMovimiento = 5f;
    public float velocidadRotacion = 100f;

    public float x, y;

    public float velocidadInicial;
    public float sprint = 1;

    public Animator anim;

    public bool IsWalking
    {
        get { return _walking; }
        set
        {
            if (_walking != value)
            {
                _walking = value;
                if (_walking == true)
                {
                    anim.SetBool("walking", true);
                }
                else
                {
                    anim.SetBool("walking", false);
                }
            }
        }
    }

    private bool _walking=false;

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

        if (y != 0 && y != 0)
        {
            IsWalking = true;
        }
        else
        {
            IsWalking = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) )
        {
            sprint = 1.5f;
            anim.SetFloat("sprint", sprint);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)||!IsWalking)
        {
            sprint = 1f;
            anim.SetFloat("sprint", sprint);
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
