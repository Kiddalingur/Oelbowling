using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;


public class CameraRotation : NetworkBehaviour
{

    public float mouseSensitivity = 1000f;

    public Transform playerBody;

    float xRotation = 0f;
    float yRotation = 0f;

    public float camClamp = 90f;

    public GameObject PlayerModel;

    bool firstLockCheck = false;

    

    void Start()
    {
        
    }

    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Game")
        {

            if (firstLockCheck == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                firstLockCheck = true;
            }

            if (PlayerModel.activeSelf == false)
            {

                PlayerModel.SetActive(true);
            }

            if (hasAuthority)
            {
                MouseMovement();
            }
        }


    }



    public void MouseMovement()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -camClamp, camClamp);

        yRotation = mouseX;
        //transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);



        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);



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


}
