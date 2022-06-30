using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;


public class PlayerMovementController : NetworkBehaviour
{

    

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
                mouseLook();
            }

            
        }
    }


    public void SetPosition()
    {
        transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
    }

    public void Movement()
    {
        float xDirection = Input.GetAxis("Horizontal") * Time.deltaTime;
        float zDirection = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);

        transform.position += moveDirection * Speed;


    }

    public void mouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        

    }

   
    
}
