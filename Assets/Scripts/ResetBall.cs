using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ResetBall : NetworkBehaviour
{

    public Transform football;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetBall();
        }
    }

    public void resetBall()
    {
            football.transform.position = new Vector3(Random.Range(-3, 3), 1, Random.Range(-3, 3));
    }



}

