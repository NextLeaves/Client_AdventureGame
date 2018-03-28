using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Panel
{
    public class TaskPanel : PanelBase
    {
        private Button ok_btn;
        private Button close_btn;
        private Text head_txt;
        private Text task_txt;
        private Text reword_txt;
        private Image image_img;

        public override void Init(params object[] args)
        {
            base.Init(args);
            SkinPath = "TaskPanel";
            Layer = PanelLayer.Panels;
        }        

        public override void OnShowing()
        {
            base.OnShowing();
            Transform skinTrans = Skin.transform;
            ok_btn = skinTrans.Find("mainInfo_panel/Scroll View/Viewport/Content/solidLayout/obj_btn/Button").GetComponent<Button>();
            close_btn = skinTrans.Find("close_btn").GetComponent<Button>();
            head_txt = skinTrans.Find("headinfo_img/head_txt").GetComponent<Text>();
            task_txt = skinTrans.Find("mainInfo_panel/Scroll View/Viewport/Content/expression_info_txt").GetComponent<Text>();
            reword_txt = skinTrans.Find("mainInfo_panel/Scroll View/Viewport/Content/reword_info_txt").GetComponent<Text>();
            image_img = skinTrans.Find("mainPicture").GetComponent<Image>();
        }

        public override void OnShowed()
        {
            base.OnShowed();
            ok_btn.onClick.AddListener(OnStartClick);
            close_btn.onClick.AddListener(OnCloseClick);
            head_txt.text = Convert.ToString(Args[0]);
            task_txt.text = Convert.ToString(Args[1]);
            reword_txt.text = Convert.ToString(Args[2]);
            image_img.sprite = Args[3] as Sprite;

        }

        private void OnStartClick()
        {
            base.Close();
        }

        private void OnCloseClick()
        {
            base.Close();
        }
    }
}
