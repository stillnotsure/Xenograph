using Holoville.HOTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private static UIManager instance;
    private Image screenCover;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        screenCover = GameObject.Find("Screen Cover").GetComponent<Image>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void FadeOut(SpriteRenderer sprite, float time)
    {
        HOTween.To(sprite, time, new TweenParms().Prop("color", new Color(1.0f, 1.0f, 1.0f, 0.0f)));
    }

    public void FadeIn(SpriteRenderer sprite, float time)
    {
        HOTween.To(sprite, time, new TweenParms().Prop("color", new Color(1.0f, 1.0f, 1.0f, 1.0f)));
    }

    public void FadeIn(Image image, float time)
    {
        HOTween.To(image, time, new TweenParms().Prop("color", new Color(1.0f, 1.0f, 1.0f, 0.0f)));
    }

    public void FadeToBlack(Image image, float time)
    {
        HOTween.To(image, time, new TweenParms().Prop("color", new Color(0f, 0f, 0f, 1.0f)));
    }

    public void FadeOut(Anima2D.SpriteMeshInstance sprite, float time)
    {
        HOTween.To(sprite, time, new TweenParms().Prop("color", new Color(1.0f, 1.0f, 1.0f, 0.0f)));
    }

    public void FadeIn(Anima2D.SpriteMeshInstance sprite, float time)
    {
        HOTween.To(sprite, time, new TweenParms().Prop("color", new Color(1.0f, 1.0f, 1.0f, 1.0f)));
    }

    public void FadeScreenToBlack(float time)
    {
        FadeToBlack(screenCover, time);
    }

    public static UIManager GetInstance()
    {
        return instance;
    }
}
