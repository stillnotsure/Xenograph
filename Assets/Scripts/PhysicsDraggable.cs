using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDraggable : MonoBehaviour {

    private Vector3 gameObjectSreenPoint;
    private Vector3 mousePreviousLocation;
    private Vector3 mouseCurLocation;
    public bool externalBody = false;
    public bool staticUnlessDragged = true;
    public Rigidbody2D body;
    public List<Rigidbody2D> children;

    private Vector3 force;
    private Vector3 objectCurrentPosition;
    private Vector3 objectTargetPosition;
    public float topSpeed = 2;

    private bool interrupt = false;
    private bool forcedDrag = false;

    void Start()
    {
        if (!externalBody)
        {
            body = gameObject.GetComponent<Rigidbody2D>();
        }
        body.WakeUp();
    }

    void OnMouseDown()
    {
        GetScreenPoint();
    }

    void OnMouseDrag()
    {
        UpdateDragging();
    }

    public void OnMouseUp()
    {
        //Makes sure there isn't a ludicrous speed
        force = new Vector3(0, 0, 0);
        interrupt = false;
        forcedDrag = false;
    }

    public void FixedUpdate()
    {

        if (forcedDrag)
        {
            if (Input.GetMouseButtonUp(0))
            {
                force = new Vector3(0, 0, 0);
                forcedDrag = false;
            } else
            {
                UpdateDragging();
            }
        }
        body.velocity = force;
    }

    void Interrupt()
    {
        interrupt = true;
    }

    void GetScreenPoint()
    {
        //This grabs the position of the object in the world and turns it into the position on the screen
        gameObjectSreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //Sets the mouse pointers vector3
        mousePreviousLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
        SendMessage("PickedUp", new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z));
    }

    void UpdateDragging()
    {
        if (!interrupt)
        {
            mouseCurLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
            force = mouseCurLocation - mousePreviousLocation;//Changes the force to be applied
            mousePreviousLocation = mouseCurLocation;
        }
    }

    //Used to smoothly transfer which item is being grabbed
    public void ForceGrabbing(bool x)
    {
        forcedDrag = true;
        GetScreenPoint();
    }
}
