using UnityEngine;
using UnityEngine.UI;
using System;

namespace Assets.Scripts.UI.Panel
{
    public class MentionPanel : PanelBase
    {
        private Button ok_btn;
        private Button close_btn;
        private Text head_txt;
        private Text main_txt;

        private void Awake()
        {
        }

        public override void Init(params object[] args)
        {
            base.Init(args);
            SkinPath = "MentionPanel";
            Layer = PanelLayer.Hints;
        }

        public override void OnShowing()
        {
            base.OnShowing();
            Transform skinTrans = Skin.transform;
            ok_btn = skinTrans.Find("info_panel/ok_btn").GetComponent<Button>();
            close_btn = skinTrans.Find("close_btn").GetComponent<Button>();
            head_txt = skinTrans.Find("title_img/head_txt").GetComponent<Text>();
            main_txt = skinTrans.Find("info_panel/main_txt").GetComponent<Text>();

            head_txt.text = Convert.ToString(Args[0]) ?? head_txt.text;
            main_txt.text = Convert.ToString(Args[1]);
        }

        public override void OnShowed()
        {
            base.OnShowed();
            ok_btn.onClick.AddListener(OnCloseClick);
            close_btn.onClick.AddListener(OnCloseClick);
        }

        private void OnCloseClick()
        {
            base.Close();
        }
    }
}
