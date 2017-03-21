using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    public GameObject head;
    private Animator animator;
    public string actorName;
    public string representation;
    public GameObject speechBubblePrefab;
    private SpeechBubble speechBubbleScript;

    void Start()
    {   
        if (representation == null)
        {
            representation = actorName;
        }
        animator = this.GetComponent<Animator>();
    }

    public void SayLine(string line)
    {
        GameObject speechBubble = GameObject.Instantiate(speechBubblePrefab);
        speechBubbleScript = speechBubble.GetComponent<SpeechBubble>();
        speechBubbleScript.SetSpeaker(this);
        speechBubbleScript.ReceiveText(line);
    }

    public void PerformAnimation(ActingDirection.Animation animation)
    {
        if (animation == ActingDirection.Animation.SitDown)
        {
            animator.SetTrigger("SitDown");
            animator.ResetTrigger("StandUp");
        }
        else if (animation == ActingDirection.Animation.StandUp)
        {
            animator.SetTrigger("StandUp");
            animator.ResetTrigger("SitDown");
        }
    }

    public void SetTalking(bool isTalking)
    {
        if (head)
        {
            head.GetComponent<Animator>().SetBool("Talking", isTalking);
        }
        else
        {
            Debug.Log(actorName + "has no head");
        }
        Debug.Log(actorName + ": " + isTalking);
    }

}
