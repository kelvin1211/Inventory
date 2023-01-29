using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jason;
using UnityEngine.UI;
public class FadeInOut : MonoBehaviour
{
    Image panel;
    float time = 0f;
    float fadeTime = 0.3f;

    Button LeftArrow;
    Button RightArrow;
    Button DownArrow;

    private void Awake()
    {
        Initailized();
        ClickArrowEvent();
    }

    void Initailized()
    {
        Transform parent = AssetAssist.FindObject("UICanvas");
        panel = AssetAssist.FindComponent<Image>("FadeInOut",parent);
        LeftArrow = AssetAssist.FindComponent<Button>("LeftArrow", parent);
        RightArrow = AssetAssist.FindComponent<Button>("RightArrow", parent);
        DownArrow = AssetAssist.FindComponent<Button>("DownArrow", parent);

    }

    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = panel.color;
        while(alpha.a < 1f) 
        {
            time+= Time.deltaTime/fadeTime;
            alpha.a = Mathf.Lerp(0,1,time);
            panel.color= alpha;
            yield return null;
        }

        time = 0f;
        yield return new WaitForSeconds(fadeTime);

        while(alpha.a >0f)
        {
            time+= Time.deltaTime/fadeTime;
            alpha.a = Mathf.Lerp(1,0,time);
            panel.color= alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }

    void ClickArrowEvent()
    {
        LeftArrow.onClick.AddListener(() =>
        {
            Fade();
        });

        RightArrow.onClick.AddListener(() =>
        {
            Fade();
        });

        DownArrow.onClick.AddListener(() =>
        {
            Fade();
        });
    }
}
