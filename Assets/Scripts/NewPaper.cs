using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPaper : MonoBehaviour {
    public GameObject paperPrefab;
    bool mouseHeld = false;
    AudioManager am;
    public AudioClip newPaperAudio;
    public float z;

    void Start()
    {
        am = AudioManager.GetInstance();
    }

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
            am.PlayOnce(am.localAudioSource, newPaperAudio);
            Vector3 position = new Vector3(transform.position.x, transform.position.y, z);
            GameObject paper = Instantiate(paperPrefab, position, transform.rotation);
            paper.GetComponent<PhysicsDraggable>().ForceGrabbing(true);
        }
    }
}
