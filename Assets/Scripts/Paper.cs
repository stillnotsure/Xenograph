using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour {

    public GameObject flatPaperObject;
    public bool loaded = false;
    public bool inFrontOfInkPoint = false;
    private float lockedHeight = -3.1f;

    void FixedUpdate()
    {
        if (loaded)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, lockedHeight, gameObject.transform.position.z);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Enter " + coll.transform.name);
        if (coll.transform.name == "InkPoint")
        {
            inFrontOfInkPoint = true;
        }
        else if (coll.transform.name == "PaperLoadedCollider")
        {
            transform.tag = "Active Paper";
            transform.SetParent(GameObject.Find("Typewriter-Bar").transform);
            GetComponent<Rigidbody2D>().isKinematic = true;
            loaded = true;
            GameObject.FindGameObjectWithTag("Input Device").GetComponent<KeyManager>().SetTarget(gameObject);
        }
        else if (coll.transform.name == "Outbox")
        {
            Destroy(gameObject);
            Instantiate(flatPaperObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        Debug.Log("Exit " + coll.transform.name);
        if (coll.transform.name == "InkPoint")
        {
            inFrontOfInkPoint = false;
        }
        else if (coll.transform.name == "PaperLoadedCollider")
        {
            loaded = false;
        }
    }

    void PickedUp(Vector3 mouseCoords)
    {
        GameObject.FindGameObjectWithTag("Input Device").GetComponent<KeyManager>().SetTarget(null);
        transform.SetParent(null);
        transform.tag = "Untagged";
        loaded = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    public void MoveUp(float increment)
    {
        Debug.Log("Previous locked height" + lockedHeight);
        Debug.Log("Previous locked height" + increment);
        lockedHeight -= increment;

        GameObject paperLoadedCollider = GameObject.Find("PaperLoadedCollider");
        paperLoadedCollider.transform.position = new Vector3(paperLoadedCollider.transform.position.x, paperLoadedCollider.transform.position.y-increment, paperLoadedCollider.transform.position.x);
    }

}
