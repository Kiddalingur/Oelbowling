using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class BallPick : NetworkBehaviour
{

    [SyncVar]
    public GameObject Parent;



    void Update()
    {

        if (Parent != null)
        {
            transform.position = Parent.transform.position;
            transform.rotation = Parent.transform.rotation;
        }
    }
}
