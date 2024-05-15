using UnityEngine;

public class Cameras : MonoBehaviour
{
    public Camera camera1, camera2, camera3;

    // Start is called before the first frame update
    void Awake()
    {
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;       
    }

    public void ControlCameras(bool activate)
    {
        if(activate) { ActivateCamera(); }
        else { DeactivateCameras(); }
    }


    private void ActivateCamera()
    {
        camera1.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
    }
    private void DeactivateCameras()
    {
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            camera1.enabled = true;
            camera2.enabled = false;
            camera3.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            camera1.enabled = false;
            camera2.enabled = true;
            camera3.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            camera1.enabled = false;
            camera2.enabled = false;
            camera3.enabled = true;
        }
    }
}
