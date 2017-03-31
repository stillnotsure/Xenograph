using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

    public bool moving = false;
    private bool bellRung = false;
    public float bellX = -6.65f;
    public float farLeftX = -7.47f; 
    public float distancePerLetter = -0.1f;
    public float distancePerSpace = -0.07f;
    public float distancePerLine = -0.2f;

    //public float distanceWhenFull = 26;
    private GameObject paperLoadedCollider;
    private AudioManager am;
    private AudioSource source;
    public AudioClip carriageReturn;
    public AudioClip carriageThud;
    public AudioClip bell;

	void Start()
    {
        paperLoadedCollider = GameObject.Find("PaperLoadedCollider");
        am = AudioManager.GetInstance();
        source = GetComponent<AudioSource>();
    }

	public void MoveBar(string key)
    {
        if (transform.localPosition.x > farLeftX && !moving)
        {
            if (!bellRung && transform.localPosition.x < bellX)
            {
                am.PlayOnce(am.localAudioSource, bell);
                bellRung = true;
            }
            if (key == " ")
            {
                transform.Translate(distancePerSpace, 0, 0);
            }
            else
            {
                transform.Translate(distancePerLetter, 0, 0);
            }
        }
    }

    public bool IsFarLeft()
    {
        return (transform.localPosition.x < farLeftX);
    }

    public void CarriageReturn()
    {
        /*
        GameObject.Find("Paper").transform.Translate(0, distancePerLine, 0);
        */
        GameObject paper = GameObject.FindGameObjectWithTag("Active Paper");
        if (paper != null)
        {
            paper.transform.FindChild("TextInput").GetComponent<Output>().NewLine();
            paper.GetComponent<Paper>().MoveUp(distancePerLine);
        }

    }

    IEnumerator MoveRight()
    {

        source.Play();
        if (!moving)
        { // do nothing if already moving
            moving = true; // signals "I'm moving, don't bother me!"
            float time = 2f / (1.42f - transform.position.x );
            Vector3 targetPos = new Vector3(1.42f, transform.position.y, transform.position.z);
            while (transform.position != targetPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime*4);
                yield return 0; // leave the routine and return here in the next frame
            }
            moving = false; // finished moving
            source.Stop();
            source.PlayOneShot(carriageThud);
            bellRung = false;
            CarriageReturn();
        }
    }

    public void PullLever()
    {
        StartCoroutine(MoveRight());
    }

    public void ResetPaperHeight()
    {
        paperLoadedCollider.transform.localPosition = new Vector3(paperLoadedCollider.transform.localPosition.x, -2.9f);
    }
}
