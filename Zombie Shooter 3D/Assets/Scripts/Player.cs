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
        if (_controller.isGrounded)
        {

            float horizontal = Input.GetAxis("Horizontal");

            float vertical = Input.GetAxis("Vertical");

            _direction = new Vector3(horizontal, 0, vertical);



            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y = _jumpHeight;
            }


        }
        _direction.y -= _gravity * Time.deltaTime;

        //this changes from local to world
        _direction = transform.TransformDirection(_direction);

        _controller.Move(_direction * _speed * Time.deltaTime);
       
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
        currentCameraRotation.x -= mouseY;
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);

    }

}
