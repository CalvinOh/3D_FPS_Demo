using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private string mouseXInputName, mouseYInputName;
    [SerializeField]
    private float mouseSensitivity;

    [SerializeField]
    private Transform playerBody;

    private float xAxisClamp;

    [SerializeField]
    float moveSpeed;
    //What direction the player will move in
    private Vector3 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xAxisClamp = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }
    private void FixedUpdate()
    {
        Movement();
    }


    //Rotate Player Camera 
    void RotateCamera()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if(xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotation(270.0f);
        }
        else if(xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotation(90.0f);
        } 

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }
   
    void ClampXAxisRotation(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

    void Movement()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        

        this.GetComponentInParent<Rigidbody>().MovePosition(this.GetComponentInParent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
    }
}
