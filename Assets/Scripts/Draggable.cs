using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Draggable : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool interrupt;

    void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        SendMessage("PickedUp", new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (!interrupt)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            float Z = transform.position.z;
            curPosition.z = Z;
            transform.position = curPosition;
        }
    }

    void OnMouseUp()
    {
        interrupt = false;
    }

    void Interrupt()
    {
        interrupt = true;
    }
}
