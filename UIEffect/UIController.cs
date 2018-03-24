using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button changeServer_btn;
    public GameObject server_scrollView;

    public Button server1_btn;
    public Button server2_btn;
    public Button server3_btn;
    public Button server4_btn;
    public Text serverName_txt;

    void Start()
    {
        changeServer_btn.onClick.AddListener(OpenServerScrollView);        
    }

    void Update()
    {

    }

    void OpenServerScrollView()
    {
        server_scrollView.SetActive(!server_scrollView.activeSelf);
    }    

    public void ChangeServerName(Button button)
    {
        serverName_txt.text = button.GetComponentInChildren<Text>().text;
        OpenServerScrollView();
    }


}
