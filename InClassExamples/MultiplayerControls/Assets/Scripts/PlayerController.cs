using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private PlayerInputController controls;
    private CharacterController cc;
    private Vector2 move;
    private Vector2 look;
    private Vector3 movementVector;
    private GameObject playerCamera;
    public float moveForce;
    public float jumpForce;
    public float gravity;
    public float maxCameraAngle;
    public bool invertYAxis;
    public int num;


    private void Start()
    {
        cc = this.GetComponent<CharacterController>();
        playerCamera = this.transform.GetChild(0).gameObject;
    }

    private void Awake()
    {
        controls = new PlayerInputController();

        controls.Gameplay.Jump.started += ctx => Jump();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => look = Vector2.zero;
    }

    void Update()
    {
        float oldY = movementVector.y;
        oldY -= gravity * Time.deltaTime;
        movementVector = new Vector3(move.x * moveForce, oldY, move.y * moveForce);
        movementVector = transform.TransformDirection(movementVector);
        cc.Move(movementVector * Time.deltaTime);
        HandlePlayerRotation();
        HandleCamera();
        //HandleCamera();
        //transform.Translate(m, Space.World);

        //cc.attachedRigidbody.velocity = newVelo;
    }

    //void Update()
    //{
    //    HandlePlayerRotation();
    //}

    void LateUpdate()
    {
        //HandleCamera();
    }

    void Jump()
    {
        if (cc.isGrounded)
        {
            movementVector.y = jumpForce;
        }
        movementVector.y -= gravity * Time.deltaTime;
        //cc.attachedRigidbody.AddForce(Vector3.up * jumpForce);
    }

    void HandlePlayerRotation()
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

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void OnMove()
    {
        Debug.Log("moving" + num);
    }

    void OnJump()
    {
        Debug.Log("jumping" + num);
    }

    void OnLook()
    {
        Debug.Log("looking" + num);
    }

}
