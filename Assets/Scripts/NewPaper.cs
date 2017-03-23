using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPaper : MonoBehaviour {
    public GameObject paperPrefab;
    bool mouseHeld = false;

    void OnMouseDown()
    {
        mouseHeld = true;
    }

    void OnMouseUp()
    {
        mouseHeld = false;
    }

    void OnMouseExit()
    {

        if (mouseHeld)
        {
            GameObject paper = Instantiate(paperPrefab, transform.position, transform.rotation);
            paper.GetComponent<PhysicsDraggable>().ForceGrabbing(true);
        }
    }
}
