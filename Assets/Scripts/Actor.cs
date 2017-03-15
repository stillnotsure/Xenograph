using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

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
    }

    public void SayLine(string line)
    {
        GameObject speechBubble = GameObject.Instantiate(speechBubblePrefab);
        DestroyOldSpeechBubble();
        speechBubbleScript = speechBubble.GetComponent<SpeechBubble>();
        speechBubbleScript.SetSpeaker(this);
        speechBubbleScript.ReceiveText(line);
    }

    private void DestroyOldSpeechBubble()
    {
        if (speechBubbleScript)
        {
            speechBubbleScript.Destroy();
        }
    }
}
