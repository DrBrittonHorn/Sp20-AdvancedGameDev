using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private PlayerInputController controls;
    public CharacterController player;
    public bool invertYAxis;
    private Vector2 look;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerInputController();
        controls.Gameplay.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => look = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        if (look.y != 0)
            Debug.Log(look.y);
        /*
        if (transform.eulerAngles.x > 80.0)
        {
            Debug.Log("clamping positive");
            look.y = 0;
            //ClampXAxisRotationToValue(280.0f);
        }
        else if (transform.eulerAngles.x < -80.0)
        {
            Debug.Log("clamping negative");
            look.y = 0;
            //ClampXAxisRotationToValue(80.0f);
        }
        */
        transform.Rotate(-1 * Vector3.left * look.y);
        //player.Rotate(Vector3.up * look.x, Space.World);


        /*
        xAxisClamp += look.y;

        if (xAxisClamp > 80.0)
        {
            xAxisClamp = 80.0f;
            look.y = 0;
            ClampXAxisRotationToValue(280.0f);
        }
        else if (xAxisClamp < -80.0)
        {
            xAxisClamp = -80.0f;
            look.y = 0;
            ClampXAxisRotationToValue(80.0f);
        }
        if (invertYAxis)
            GetComponentInChildren<Camera>().transform.Rotate(Vector3.left * look.y * -1f);
        else
            GetComponentInChildren<Camera>().transform.Rotate(Vector3.left * look.y);
        transform.Rotate(Vector3.up * look.x);
        */
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
