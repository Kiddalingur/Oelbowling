using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public Rigidbody soccer;
    private bool hasBallFired = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 3 && hasBallFired == false)
        {
            soccer.AddForce(Random.Range(100, 200), 0, 0);
            hasBallFired = true;
        }
        if (Time.time > 7 && hasBallFired == true)
        {
            soccer.drag = 2f;
        }
    }
}
