using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour {

    public bool instructionPaper = false;
    public GameObject flatPaperObject;
    public bool loaded = false;
    public bool inFrontOfInkPoint = false;
    private float lockedHeight = -3.11f;
    private AudioManager am;
    private AudioSource audioSource;
    private AudioAssets audioAssets;
    private float sfxThrottleTimerLength = 0.3f;
    private float sfxThrottleTimer;
    private bool sfxPlayed = false;

    void Start()
    {
        sfxThrottleTimer = sfxThrottleTimerLength;
        am = AudioManager.GetInstance();
        audioSource = GetComponent<AudioSource>();
        audioAssets = GetComponent<AudioAssets>();
    }

    void FixedUpdate()
    {
        sfxThrottleTimer -= Time.deltaTime;
        if (loaded)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, lockedHeight, gameObject.transform.position.z);
        }
        if (sfxThrottleTimer <= 0)
        {
            sfxPlayed = false;
            sfxThrottleTimer = sfxThrottleTimerLength;
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
                Debug.Log("Loaded");
                if (!sfxPlayed)
                {
                    am.PlayOnce(audioSource, audioAssets.assets[2]);
                    sfxPlayed = true;
                }
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
            am.PlayOnce(am.GetComponent<AudioSource>(), audioAssets.assets[1]);
            if (!instructionPaper)
                Destroy(gameObject);
            else
                Destroy(gameObject.transform.parent.gameObject);
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
            Debug.Log("Unloaded");
        }
    }

    void PickedUp(Vector3 mouseCoords)
    {
        am.PlayOnce(audioSource, audioAssets.assets[0]);
        Debug.Log("Picked");
        if (loaded)
        {
            Debug.Log("Loaded");
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
