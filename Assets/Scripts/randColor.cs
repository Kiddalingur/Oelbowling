using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randColor : MonoBehaviour
{

    // NOT WORKING, probably needs to be sent over command 

    public Color myColor;
    public float rFloat;
    public float gFloat;
    public float bFloat;

    public Renderer myRenderer;


    void Start()
    {
        float aFloat = 1;

        rFloat = Random.Range(0, 255);
        gFloat = Random.Range(0, 255);
        bFloat = Random.Range(0, 255);

        myRenderer = gameObject.GetComponent<Renderer>();

        myColor = new Color(rFloat, gFloat, bFloat, aFloat);

    }


}
