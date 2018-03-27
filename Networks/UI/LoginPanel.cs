using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public InputField account_input;
    public InputField password_input;
    public Button log_btn;

    private MessageDistribution _msgDistri = MessageDistribution.GetInstance();

    void Start()
    {
        log_btn.onClick.AddListener(OnLoginClick);
        _msgDistri.AddOnceListenner(NamesOfProtocol.Login, OnLoginBack);
    }

    void Update()
    {

    }

    private void OnLoginClick()
    {
        if (string.IsNullOrEmpty(account_input.text) || string.IsNullOrEmpty(password_input.text))
        {
            Debug.Log("[Warn] input nothing.");
            return;
        }

        if (NetworkManager.ConnClient.status != NetworkStatus.Connected)
        {
            string host = "127.0.0.1";
            int port = 1234;
            NetworkManager.ConnClient.Connect(host, port);
        }
        ProtocolByte proto = new ProtocolByte();
        proto.AddInfo<string>(NamesOfProtocol.Login);
        proto.AddInfo<string>(account_input.text);
        proto.AddInfo<string>(password_input.text);
        NetworkManager.ConnClient.Send(proto);
    }

    private void OnLoginBack(ProtocolBase protocol)
    {
        ProtocolByte proto = protocol as ProtocolByte;
        string num = proto.GetString(1);
        if (num == "1")
        {
            Debug.Log("登录成功--game start");                
        }
        else
        {
            Debug.Log("登录失败--password is error");
        }
    }
}
