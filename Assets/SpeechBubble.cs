using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


/*Speech bubble for displaying dialogue
 * 
 *  methods: 
 *  Receive lines
 *  some ienumerator for dislaying one char at a time
 *  a timer for destroying self a few seconds after all the text is displayed
 *  draw a triangle towards point of origin
 */
public class SpeechBubble : MonoBehaviour {

    public Actor speaker;
    private string fullText;
    private string displayedText;
    private Text textComponent;
    private float xBuffer = 4f;

    private float timeUntilFinished;
    private bool textFinished;
    private float timer = 10f;

	// Use this for initialization
	void Awake () {
        gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
        textComponent = gameObject.transform.Find("Text").GetComponent<Text>();
        textFinished = false;
	}

    void Update()
    {
        //Destroys the object an amount of time after the text is finished displaying
        if (textFinished == true)
        {
            if (timer <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        } else
        {
            timeUntilFinished -= Time.deltaTime;
            if (timeUntilFinished <= 0)
            {
                textFinished = true;
                speaker.SetTalking(false);
            }
        }

    }

    //Finds a good starting point near the speaker
    private void SetPosition()
    {
        float y = speaker.transform.position.y;
        float x;

        float speakerX = Camera.main.WorldToScreenPoint(speaker.transform.position).x;
        Debug.Log(speakerX);
        Debug.Log(Screen.width / 2);
        if (speakerX < Screen.width / 2)
        {
            x = speaker.transform.position.x + xBuffer;
        } else
        {
            x = speaker.transform.position.x - xBuffer;
        }
        gameObject.transform.position = new Vector3(x, y, transform.position.z);
        //TODO - Text appears letter at a time, don't set to finished until last letter shown
    }

    public void SetSpeaker(Actor speaker)
    {
        this.speaker = speaker;
        SetPosition();
        speaker.SetTalking(true);
    }

	public void ReceiveText(string text)
    {
        textComponent.text = (text);
        timeUntilFinished = text.Length * 0.06f; //TODO : Replace this with chars appearing a letter at a time
        Debug.Log(text + ": " + timeUntilFinished);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
