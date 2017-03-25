using Anima2D;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Actor : MonoBehaviour {

    public GameObject head;
    private Animator animator;
    public string actorName;
    public string representation;
    public GameObject speechBubblePrefab;
    private SpeechBubble speechBubbleScript;

    //Graphics
    public List<SpriteMeshInstance> spriteMeshes;
    public List<SpriteRenderer> sprites;


    void Start()
    {   
        if (representation == null)
        {
            representation = actorName;
        }
        animator = this.GetComponent<Animator>();
        //PopulateSpriteLists();
    }

    private void PopulateSpriteLists()
    {
        var spriteArray = gameObject.transform.Cast<Transform>().Where(c => c.gameObject.tag == "Sprite").ToArray();
        for (int i = 0; i < spriteArray.Length; i++)
        {
            if (spriteArray[i].GetComponent<SpriteRenderer>() != null)
            {
                Debug.Log(spriteArray[i]);
                sprites.Add(spriteArray[i].GetComponent<SpriteRenderer>());
            }
            else if (spriteArray[i].GetComponent<SpriteMeshInstance>() != null)
            {
                spriteMeshes.Add(spriteArray[i].GetComponent<SpriteMeshInstance>());
            }
        }
        sprites.Add(transform.Find("Head").GetComponent<SpriteRenderer>());
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
    }

    public IEnumerator Teleport(Vector3 targetPosition, bool flipX)
    {
        float duration = 3f;
        fadeAllLimbs(false, duration/2);
        yield return new WaitForSeconds(duration / 2);
        Debug.Log(targetPosition);
        transform.localPosition = targetPosition;
        Debug.Log(transform.localPosition);
        if (flipX)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        fadeAllLimbs(true, duration / 2);
    }

    private void fadeAllLimbs(bool isIn, float fadeDuration)
    {
        UIManager uiManager = UIManager.GetInstance();
        for (int i = 0; i < sprites.Count(); i++)
        {
            if (isIn)
                uiManager.FadeIn(sprites[i], fadeDuration);
            else
                uiManager.FadeOut(sprites[i], fadeDuration);
        }
        for (int i = 0; i < spriteMeshes.Count(); i++)
        {
            if (isIn)
                uiManager.FadeIn(spriteMeshes[i], fadeDuration);
            else
                uiManager.FadeOut(spriteMeshes[i], fadeDuration);
        }
    }

}
