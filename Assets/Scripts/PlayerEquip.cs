using UnityEngine;
using System.Collections;
using Mirror;

public enum EquippedItem : byte
{
    nothing,
    ball
}

public class PlayerEquip : NetworkBehaviour
{
    public GameObject sceneObjectPrefab;

    public GameObject rightHand;

    public GameObject ballPrefab;




    [SyncVar(hook = nameof(OnChangeEquipment))]
    public EquippedItem equippedItem;

    void OnChangeEquipment(EquippedItem oldEquippedItem, EquippedItem newEquippedItem)
    {
        StartCoroutine(ChangeEquipment(newEquippedItem));
    }

    // Since Destroy is delayed to the end of the current frame, we use a coroutine
    // to clear out any child objects before instantiating the new one
    IEnumerator ChangeEquipment(EquippedItem newEquippedItem)
    {
        while (rightHand.transform.childCount > 0)
        {
            Destroy(rightHand.transform.GetChild(0).gameObject);
            yield return null;
        }

        switch (newEquippedItem)
        {
            case EquippedItem.ball:
                Instantiate(ballPrefab, rightHand.transform);
                break;

        }
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.Alpha1) && equippedItem != EquippedItem.nothing)
            CmdChangeEquippedItem(EquippedItem.nothing);
        if (Input.GetKeyDown(KeyCode.Alpha2) && equippedItem != EquippedItem.ball)
            CmdChangeEquippedItem(EquippedItem.ball);




        if (Input.GetKeyDown(KeyCode.X) && equippedItem != EquippedItem.nothing)
            CmdDropItem();



    }

    [Command]
    void CmdChangeEquippedItem(EquippedItem selectedItem)
    {
        equippedItem = selectedItem;
    }


    [Command]
    void CmdDropItem()
    {
        // Instantiate the scene object on the server
        Vector3 pos = rightHand.transform.position;
        Quaternion rot = rightHand.transform.rotation;
        GameObject newSceneObject = Instantiate(sceneObjectPrefab, pos, rot);

        // set the RigidBody as non-kinematic on the server only (isKinematic = true in prefab)
        newSceneObject.GetComponent<Rigidbody>().isKinematic = false;

        SceneObject sceneObject = newSceneObject.GetComponent<SceneObject>();

        // set the child object on the server
        sceneObject.SetEquippedItem(equippedItem);

        // set the SyncVar on the scene object for clients
        sceneObject.equippedItem = equippedItem;

        // set the player's SyncVar to nothing so clients will destroy the equipped child item
        equippedItem = EquippedItem.nothing;

        // Spawn the scene object on the network for all to see
        NetworkServer.Spawn(newSceneObject);
    }

    // CmdPickupItem is public because it's called from a script on the SceneObject
    [Command]
    public void CmdPickupItem(GameObject sceneObject)
    {
        // set the player's SyncVar so clients can show the equipped item
        equippedItem = sceneObject.GetComponent<SceneObject>().equippedItem;

        // Destroy the scene object
        NetworkServer.Destroy(sceneObject);
    }


}