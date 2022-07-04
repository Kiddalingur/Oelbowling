using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Hand : NetworkBehaviour
{

    public GameObject PickUp;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Pickup")) return;

        CmdPickUp(other.gameObject);
        Debug.Log("Should pick up other gameObject");
    }

    [Command]
    public void CmdPickUp(GameObject ball)
    {
        PickUp = ball;
        ball.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        ball.GetComponent<BallPick>().Parent = gameObject;
    }

    [Command]
    public void CmdDropOff()
    {
        PickUp.GetComponent<BallPick>().Parent = null;
    }

}
