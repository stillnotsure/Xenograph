using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using TMPro;
using Holoville.HOTween;

public class RecapScript : MonoBehaviour
{
    public TextMeshProUGUI titleText, successText, accuracyText, percentageText, byeText;
    GameManager gameManager;
    public AudioSource audioSource;

    void Awake()
    {
        successText.gameObject.SetActive(false);
        accuracyText.gameObject.SetActive(false);
        percentageText.gameObject.SetActive(false);
        byeText.gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {
        gameManager = GameManager.GetInstance();
        StartCoroutine(RevealEvent());
    }

    IEnumerator RevealEvent()
    {
        yield return new WaitForSeconds(2f);
        successText.gameObject.SetActive(true);
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        accuracyText.gameObject.SetActive(true);
        percentageText.SetText(GameManager.GetInstance().score.ToString("0") + "%");
        percentageText.gameObject.SetActive(true);
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        byeText.gameObject.SetActive(true);
        audioSource.Play();
    }
}