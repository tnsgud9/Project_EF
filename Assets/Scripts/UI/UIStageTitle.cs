using Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIStageTitle : BaseUI
    {
        public RectTransform rectTransform;
        [InjectChild] private Text _titleText;

        protected override void AssignUiManage()
        {
            UiManager.Instance.AssignUI(this);
            rectTransform = transform.GetChild(0) as RectTransform;
        }

        public void SetTitleText(string text)
        {
            _titleText.text = text;
        }
    }
}