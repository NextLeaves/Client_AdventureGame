using System;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.UI.Panel
{
    public class LoginPanel : PanelBase
    {
        private Button login_btn;
        private Button register_btn;
        private Button findout_btn;
        private Button checkProto_btn;

        private InputField acc_field;
        private InputField pw_field;

        private MessageDistribution _msgDistri = MessageDistribution.GetInstance();

        private void Awake()
        {
            _msgDistri.AddOnceListenner(NamesOfProtocol.Login, OnLoginBack);
        }

        public override void Init(params object[] args)
        {
            base.Init(args);
            SkinPath = "LoginPanel";
            Layer = PanelLayer.Panels;
        }

        public override void OnShowing()
        {
            base.OnShowing();
            Transform skinTrans = Skin.transform;
            login_btn = skinTrans.Find("log_image/underBox_image/login_btn").GetComponent<Button>();
            register_btn = skinTrans.Find("log_image/signup_btn").GetComponent<Button>();
            findout_btn = skinTrans.Find("log_image/lostpassword_btn").GetComponent<Button>();
            checkProto_btn = skinTrans.Find("log_image/underBox_image/protocol/protocol_btn").GetComponent<Button>();
            acc_field = skinTrans.Find("log_image/account/account_inputField").GetComponent<InputField>();
            pw_field = skinTrans.Find("log_image/password/password_inputField").GetComponent<InputField>();
        }

        public override void OnShowed()
        {
            base.OnShowed();
            login_btn.onClick.AddListener(OnLoginClick);
            register_btn.onClick.AddListener(OnRegisterClick);
            findout_btn.onClick.AddListener(OnFindoutClick);
            checkProto_btn.onClick.AddListener(OnOpenProtocolClick);            
        }


        private void OnLoginClick()
        {
            if (string.IsNullOrEmpty(acc_field.text) || string.IsNullOrEmpty(pw_field.text))
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
            proto.AddInfo<string>(acc_field.text);
            proto.AddInfo<string>(pw_field.text);
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

        private void OnRegisterClick()
        {
            Debug.Log("OnRegisterClick");
        }

        private void OnFindoutClick()
        {
            Debug.Log("OnFindoutClick");
        }

        private void OnOpenProtocolClick()
        {
            Debug.Log("OnOpenProtocolClick");
        }

    }
}
