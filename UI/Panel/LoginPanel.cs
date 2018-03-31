#define NETWORK

using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
#if NETWORK
            if (string.IsNullOrEmpty(acc_field.text) || string.IsNullOrEmpty(pw_field.text))
            {
                Debug.Log("[Warn] input nothing.");
                return;
            }

            if (NetworkManager.ConnClient.status != NetworkStatus.Connected)
            {                
                string host = Root.IP;
                int port = 1234;
                NetworkManager.ConnClient.Connect(host, port);
            }
            ProtocolByte proto = new ProtocolByte();
            proto.AddInfo<string>(NamesOfProtocol.Login);
            proto.AddInfo<string>(acc_field.text);
            proto.AddInfo<string>(pw_field.text);
            NetworkManager.ConnClient.Send(proto);
#elif DEBUG
            Debug.Log("登录成功--game start");
            base.Close();
            object[] objs = new object[1];
            objs[0] = 1;
            PanelManager._instance.OpenPanel<LoadingPanel>("", objs);
#endif
        }

        private void OnLoginBack(ProtocolBase protocol)
        {
            ProtocolByte proto = protocol as ProtocolByte;
            string num = proto.GetString(1);
            if (num == "1")
            {
                Debug.Log("登录成功--game start");
                Root.Account = acc_field.text;
                base.Close();
                object[] objs = new object[1];
                objs[0] = 1;
                PanelManager._instance.OpenPanel<LoadingPanel>("", objs);
            }
            else
            {
                Debug.Log("登录失败--password is error");
                object[] obj = new object[2];
                obj[0] = "错误提示";
                obj[1] = "账号或密码错误，请检查信息后，再进行操作。";
                PanelManager._instance.OpenPanel<MentionPanel>("", obj);
            }
        }

        private void OnRegisterClick()
        {
            Debug.Log("OnRegisterClick");

            base.Close();
            PanelManager._instance.OpenPanel<RegisterPanel>("", null);

        }

        private void OnFindoutClick()
        {
            Debug.Log("OnFindoutClick");
            base.Close();
            PanelManager._instance.OpenPanel<FindoutPanel>("", null);
        }

        private void OnOpenProtocolClick()
        {
            Debug.Log("OnOpenProtocolClick");
            object[] obj = new object[2];
            obj[0] = "游戏协议";
            obj[1] = "在适用法律允许的最大范围内       INUStudio保留对本协议的最终解释权  用户如对本协议有任何疑问   请登陆INUStudio或官方网站获取信息";
            PanelManager._instance.OpenPanel<MentionPanel>("", obj);
        }

    }
}
