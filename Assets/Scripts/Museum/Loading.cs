using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Loading : MonoBehaviour
{

    float FadeTime = 1.5f; // Fade효과 재생시간
    public Image BackgroundImage;
    public TextMeshProUGUI[] texts;
    float AlphaStart, AlphaEnd;
    float time = 0f;
    bool isPlaying = false;

    void Start()
    {
        OutStartFadeAnim();
    }

    public void OutStartFadeAnim()
    {
        if (isPlaying) return;
        AlphaStart = 1f;
        AlphaEnd = 0f;
        StartCoroutine("fadeoutplay");    //코루틴 실행
    }

    IEnumerator fadeoutplay()
    {
        yield return new WaitForSeconds(5f);
        isPlaying = true;

        Color fadecolor = BackgroundImage.color;
        Color textcolor = texts[0].color;
        time = 0f;
        while (fadecolor.a > 0f)
        {
            time += Time.deltaTime / FadeTime;
            fadecolor.a = Mathf.Lerp(AlphaStart, AlphaEnd, time);
            textcolor.a = Mathf.Lerp(AlphaStart, AlphaEnd, time);
            BackgroundImage.color = fadecolor;
            for (int i = 0; i < 6; ++i) texts[i].color = textcolor;
            yield return null;
        }
        isPlaying = false;
        gameObject.SetActive(false);
    }
}
