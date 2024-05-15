using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

    private Rigidbody coche;

    //WHEELS
    [Header("WHEELS")]
    public GameObject frontLeftMesh;
    public WheelCollider frontLeftCollider;
    [Space(10)]
    public GameObject frontRightMesh;
    public WheelCollider frontRightCollider;
    [Space(10)]
    public GameObject rearLeftMesh;
    public WheelCollider rearLeftCollider;
    [Space(10)]
    public GameObject rearRightMesh;
    public WheelCollider rearRightCollider;

    //SPEED CONFIG
    [Header("SPEED CONFIG")]
    public float maxSteerAngle = 30;
    public float motorForce = 3000;

    public float velocidadMaxima = 120;
    public int frenar = 3000;
    public int desacelerar = 500;
    public int frenado = 3000;

    [Space(10)]
    public bool arrancado;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        coche = GetComponent<Rigidbody>();
        coche.centerOfMass = Vector3.zero;

        arrancado = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        EfectoRueda();
    }

    private void EfectoRueda()
    {
        speed =coche.velocity.magnitude;
    }


    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (arrancado && coche.velocity.magnitude < 0.1f)
            {
                arrancado = false;
            }
            else if (!arrancado)
            {
                arrancado = true;
            }
        }
        if (!arrancado)
        { return; }

        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");


        if (Input.GetKey(KeyCode.Space))
        {
            frenar = frenado;
        }
        else
        {
            frenar = desacelerar;
        }
    }

    private void FixedUpdate()
    {
        Steer();
        UpdateWheelPoses();
        Acelerar();
        Frenar();
    }

    private void Frenar()
    {
        if (m_verticalInput == 0)
        {
            frontLeftCollider.brakeTorque = frenar;
            frontRightCollider.brakeTorque = frenar;
            rearLeftCollider.brakeTorque = frenar;
            rearRightCollider.brakeTorque = frenar;
        }
        else
        {
            frontLeftCollider.brakeTorque = 0;
            frontRightCollider.brakeTorque = 0;
            rearLeftCollider.brakeTorque = 0;
            rearRightCollider.brakeTorque = 0;
        }
    }

    private void Acelerar()
    {
        if (coche.velocity.magnitude * 4 > velocidadMaxima)
        {
            frontLeftCollider.motorTorque = 0;
            frontRightCollider.motorTorque = 0;
        }
        else
        {
            frontLeftCollider.motorTorque = m_verticalInput * motorForce;
            frontRightCollider.motorTorque = m_verticalInput * motorForce;
        }
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftCollider, frontLeftMesh.transform);
        UpdateWheelPose(frontRightCollider, frontRightMesh.transform);

        UpdateWheelPose(rearLeftCollider, rearLeftMesh.transform);
        UpdateWheelPose(rearRightCollider, rearRightMesh.transform);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = transform.rotation;
        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontLeftCollider.steerAngle = m_steeringAngle;
        frontRightCollider.steerAngle = m_steeringAngle;
    }
}
