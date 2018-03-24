using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainInterface : MonoBehaviour
{
    public Button exitGame_btn;
    public Button makeClub_btn;
    public Button startMovie_btn;
    public Button homeIndex_btn;

    void Start()
    {
        exitGame_btn.onClick.AddListener(OnExitGame);
        makeClub_btn.onClick.AddListener(OnMakeClub);
        startMovie_btn.onClick.AddListener(OnStartMovie);
        homeIndex_btn.onClick.AddListener(OnHomeIndex);
    }

    private void OnExitGame()
    {
        Application.Quit();
    }

    private void OnMakeClub()
    {

    }

    private void OnStartMovie()
    {

    }

    private void OnHomeIndex()
    {
        Application.OpenURL("https://nextleaves.github.io/");
    }
}
