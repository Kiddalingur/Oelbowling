using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;


public class PlayerMovementController : NetworkBehaviour
{

    /*
    Another camera script test, delete if it does not work
    */
    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    [Range(0.1f, 9f)] [SerializeField] float sensitivity = 2f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)] [SerializeField] float yRotationLimit = 88f;

    Vector2 rotation = Vector2.zero;
    const string xAxis = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage
    const string yAxis = "Mouse Y";


    public CharacterController controller;
    


    public float Speed = 0.1f;
    public GameObject PlayerModel;

    public float mouseSensitivity = 100f;
    public GameObject Camera;

    public Transform playerBody;

    float xRotation = 0f;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        PlayerModel.SetActive(false);

    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (PlayerModel.activeSelf == false)
            {
                //Runs once
                SetPosition();
                
                PlayerModel.SetActive(true);
            }

            if (hasAuthority)
            {
                Movement();
                //MouseLook();
                MouseMove();
            }

            
        }
    }


    public void SetPosition()
    {
        playerBody.transform.position = new Vector3(Random.Range(-3, 3), 1, Random.Range(-3, 3));
    }

    public void Movement()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * Speed * Time.deltaTime);



    }

    
    public void MouseMove()
    {
        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        playerBody.transform.localRotation = xQuat * yQuat;



        // Can be changed into a scene changer to acces pausemenu
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {  
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        
        
    }

    public void MouseLook()
    {
        float mouseX = Input.GetAxis(xAxis) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(yAxis) * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(new Vector3(0, 1, 0));
        //playerBody.Rotate(Vector3.up * mouseX);

    }

   
    
}
