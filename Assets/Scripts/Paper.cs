using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour {

    public bool instructionPaper = false;
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
        if (coll.transform.name == "InkPoint")
        {
            if (!instructionPaper)
            {
                inFrontOfInkPoint = true;
            }
        }
        else if (coll.transform.name == "PaperLoadedCollider")
        {
            if (!instructionPaper)
            {
                transform.tag = "Active Paper";
                transform.SetParent(GameObject.Find("Typewriter-Bar").transform);
                GetComponent<Rigidbody2D>().isKinematic = true;
                loaded = true;
                GameObject.FindGameObjectWithTag("Input Device").GetComponent<KeyManager>().SetTarget(gameObject);
            }
        }
        else if (coll.transform.name == "Outbox")
        {
            if (!instructionPaper){
                transform.Find("TextInput").GetComponent<Output>().SendRecord();
            }
            else {
                GameManager.GetInstance().SetState(GameManager.States.preTrial);
            }
            Destroy(gameObject);
            Instantiate(flatPaperObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
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
        if (loaded)
        {
            Bar bar = GameObject.Find("Typewriter-Bar").GetComponent<Bar>();
            bar.PullLever();
            bar.ResetPaperHeight();
        }
        GameObject.FindGameObjectWithTag("Input Device").GetComponent<KeyManager>().SetTarget(null);
        transform.SetParent(null);
        transform.tag = "Untagged";
        loaded = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    public void MoveUp(float increment)
    {
        lockedHeight -= increment;

        GameObject paperLoadedCollider = GameObject.Find("PaperLoadedCollider");
        paperLoadedCollider.transform.position = new Vector3(paperLoadedCollider.transform.position.x, paperLoadedCollider.transform.position.y-increment, paperLoadedCollider.transform.position.x);
    }

}
