using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   
    private CharacterController _controller;
    
    [Header("Controller Settings")]
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _jumpHeight = 8f;
    [SerializeField]
    private float _gravity = 20f;

    [Header("Camera Settings")]
    [SerializeField]
    private float _cameraSensitivity = 1f;

    private Vector3 _direction;

    private Camera _mainCamera;

    private float _yVelocity;



    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        _mainCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        CameraRotation();
       

        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    void CalculateMovement()
    {
          
        float horizontal = Input.GetAxis("Horizontal");

        float vertical = Input.GetAxis("Vertical");

        _direction = new Vector3(horizontal, 0, vertical);

        Vector3 velocity = _direction * _speed;

        velocity = transform.TransformDirection(velocity);

        if (_controller.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
               _yVelocity = _jumpHeight;
            }
        }
        else
        {
            _yVelocity -= _gravity * Time.deltaTime;
        }

        velocity.y = _yVelocity;

       

        _controller.Move(velocity * Time.deltaTime);

      

    }

    void CameraRotation()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * _cameraSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * _cameraSensitivity;

        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + mouseX, transform.localEulerAngles.z);

        //look left and right
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX;

        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);


        //look up and down
        Vector3 currentCameraRotation = _mainCamera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY * _cameraSensitivity ;
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0,58);
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);

    }

}
