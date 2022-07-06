using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class SceneObject : NetworkBehaviour
{

    [SyncVar(hook = nameof(OnChangeEquipment))]
    public EquippedItem equippedItem;

    public GameObject ballPrefab;


    void OnChangeEquipment(EquippedItem oldEquippedItem, EquippedItem newEquippedItem)
    {
        StartCoroutine(ChangeEquipment(newEquippedItem));
    }

    // Since Destroy is delayed to the end of the current frame, we use a coroutine
    // to clear out any child objects before instantiating the new one
    IEnumerator ChangeEquipment(EquippedItem newEquippedItem)
    {
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            yield return null;
        }

        // Use the new value, not the SyncVar property value
        SetEquippedItem(newEquippedItem);
    }

    // SetEquippedItem is called on the client from OnChangeEquipment (above),
    // and on the server from CmdDropItem in the PlayerEquip script.
    public void SetEquippedItem(EquippedItem newEquippedItem)
    {
        switch (newEquippedItem)
        {
            case EquippedItem.ball:
                Instantiate(ballPrefab, transform);
                break;

        }
    }

    public void OnMouseDown()
    {
        NetworkClient.localPlayer.GetComponent<PlayerEquip>().CmdPickupItem(gameObject);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NetworkClient.localPlayer.GetComponent<PlayerEquip>().CmdPickupItem(gameObject);
        }
    }

}



