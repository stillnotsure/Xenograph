using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour {

    public bool loaded = true;
    public bool inFrontOfInkPoint = false;


    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Enter " + coll.transform.name);
        if (coll.transform.name == "InkPoint")
        {
            inFrontOfInkPoint = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        Debug.Log("EExit " + coll.transform.name);
        if (coll.transform.name == "InkPoint")
        {
            inFrontOfInkPoint = false;
        }
    }
}
