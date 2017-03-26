using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Texture2D handCursor;

    void Start()
    {
        if (!handCursor) handCursor = Resources.Load<Texture2D>("CursorHand");
        Debug.Log(handCursor);
    }
    void OnMouseOver()
    {      
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Debug.Log("test exit");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
