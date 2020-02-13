using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CapsuleController : MonoBehaviour
{
    public static int playerNum = 0;
    Vector2 inputMovement;
    Vector2 look;
    Vector3 movementVector = Vector3.zero;
    public float moveSpeed = 10f;
    public float gravity = 10f;
    public float jumpForce = 10f;
    public bool invertYAxis = true;
    public float maxCameraAngle;
    private GameObject playerCamera;
    CharacterController cc;

    // Start is called before the first frame update
    void Awake()
    {
        cc = GetComponent<CharacterController>();
        cc.enabled = false;
        cc.transform.position = transform.position;
        cc.enabled = true;
        //playerCamera = this.transform.GetChild(0).gameObject;
        playerCamera = GetComponentInChildren<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleCamera();
        Rotate();
    }

    void OnMove(InputValue value)
    {
        inputMovement = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }

    void OnJump()
    {
        //transform.Translate(transform.up);
        
        if (cc.isGrounded)
        {
            Debug.Log("Jumping");
            movementVector.y = jumpForce;
        }
        movementVector.y -= gravity * Time.deltaTime;
        
    }

    void OnDown()
    {
        //transform.Translate(-transform.up);
    }

    void Move()
    {
        //movementVector = new Vector3(inputMovement.x, 0, inputMovement.y);
        //transform.Translate(movementVector * moveSpeed * Time.deltaTime);
        float oldY = movementVector.y;
        oldY -= gravity * Time.deltaTime;
        movementVector = new Vector3(inputMovement.x * moveSpeed, oldY, inputMovement.y * moveSpeed);
        movementVector = transform.TransformDirection(movementVector);
        cc.Move(movementVector * Time.deltaTime);
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * look.x);
    }

    void HandleCamera()
    {
        int invertFactor = 1;
        if (invertYAxis)
            invertFactor = -1;
        //Debug.Log(playerCamera.transform.eulerAngles.x);
        float xAngle = (float)System.Math.Round(playerCamera.transform.eulerAngles.x, 2);
        //float xAngle = playerCamera.transform.eulerAngles.x;
        float adjustedAngle = xAngle > 180 ? xAngle - 360 : xAngle;
        //Debug.Log("adjusted: " + adjustedAngle);
        if (adjustedAngle >= maxCameraAngle && look.y * invertFactor < 0)
        {
            //Debug.Log("clamping positive");
            look.y = 0;
            Vector3 newEuler = playerCamera.transform.eulerAngles;
            newEuler.x = maxCameraAngle;
            playerCamera.transform.eulerAngles = newEuler;
            //ClampXAxisRotationToValue(280.0f);
        }
        else if (adjustedAngle <= -maxCameraAngle && look.y * invertFactor > 0)
        {
            //Debug.Log("clamping negative");
            look.y = 0;
            Vector3 newEuler = playerCamera.transform.eulerAngles;
            newEuler.x = 360 - maxCameraAngle;
            playerCamera.transform.eulerAngles = newEuler;
            //ClampXAxisRotationToValue(80.0f);
        }
        else
            playerCamera.transform.Rotate(invertFactor * Vector3.left * look.y);
    }
}
