using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenController : MonoBehaviour
{
    public Image black_bg;
    public float fadeSpeed = 0.8f;
    public bool sceneStarting = true;

    void Start()
    {

    }


    void Update()
    {
        if (sceneStarting) StartScene();
    }

    void FadeToClear()
    {
        black_bg.color = Color.Lerp(black_bg.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        black_bg.color = Color.Lerp(black_bg.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();
        if (black_bg.color.a < 0.05f)
        {
            black_bg.color = Color.clear;
            black_bg.enabled = false;
            sceneStarting = false;
        }
    }

    void EndScene()
    {
        black_bg.enabled = true;
        FadeToBlack();
        if (black_bg.color.a > 0.95f)
        {
            Application.Quit();
        }
    }

}
