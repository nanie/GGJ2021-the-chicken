using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private Image imgIntro;
    [SerializeField] private TextMeshProUGUI txtCaption;
    [SerializeField] private List<IntroPage> pages;
    void Start()
    {
        StartCoroutine(ShowIntro());
    }

    IEnumerator ShowIntro()
    {
        foreach (var page in pages)
        {
            imgIntro.sprite = page.image;
            txtCaption.text = page.GetText();
            yield return new WaitForSeconds(page.waitTime);
        }

        SceneManager.LoadScene(1);
    }
}

[Serializable]
public class IntroPage
{
    public float waitTime;
    public Sprite image;
    public string text;
    public string GetText()
    {
        return text;
    }
}