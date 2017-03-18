using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

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

    public void PerformAction(ActingDirection.DirectionType action)
    {
        if (action == ActingDirection.DirectionType.SitDown)
        {
            animator.SetTrigger("SitDown");
            animator.ResetTrigger("StandUp");
        }
        else if (action == ActingDirection.DirectionType.StandUp)
        {
            animator.SetTrigger("StandUp");
            animator.ResetTrigger("SitDown");
        }
    }

}
