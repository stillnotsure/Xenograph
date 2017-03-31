using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Texture2D handCursor;

    void Start()
    {
        if (!handCursor) handCursor = Resources.Load<Texture2D>("CursorHand");
    }
    void OnMouseOver()
    {      
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
