using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDraggable : MonoBehaviour {

    private Vector3 gameObjectSreenPoint;
    private Vector3 mousePreviousLocation;
    private Vector3 mouseCurLocation;
    public bool externalBody = false;
    public Rigidbody2D body;

    private Vector3 force;
    private Vector3 objectCurrentPosition;
    private Vector3 objectTargetPosition;
    public float topSpeed = 2;

    void Start()
    {
        if (!externalBody)
        {
            body = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Grabbed " + transform.name);
        //This grabs the position of the object in the world and turns it into the position on the screen
        gameObjectSreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //Sets the mouse pointers vector3
        mousePreviousLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
    }

    void OnMouseDrag()
    {
        mouseCurLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
        force = mouseCurLocation - mousePreviousLocation;//Changes the force to be applied
        mousePreviousLocation = mouseCurLocation;
    }

    public void OnMouseUp()
    {
        //Makes sure there isn't a ludicrous speed
        if (body.velocity.magnitude > topSpeed)
            force = body.velocity.normalized * topSpeed;
    }

    public void FixedUpdate()
    {
        body.velocity = force;
    }
}
