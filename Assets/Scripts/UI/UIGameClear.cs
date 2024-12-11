using Collections;
using Commons;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIGameClear : BaseUI
    {
        public Button nextButton;

        protected void Start()
        {
            foreach (Transform child in transform) child.gameObject.SetActive(false);
            EventBus<Enums.Event>.Subscribe(Enums.Event.StageClear, () =>
            {
                foreach (Transform child in transform) child.gameObject.SetActive(true);
            });
            EventBus<Enums.Event>.Subscribe(Enums.Event.StageReady, () =>
            {
                foreach (Transform child in transform) child.gameObject.SetActive(false);
            });
            EventBus<Enums.Event>.Subscribe(Enums.Event.StageStart, () =>
            {
                foreach (Transform child in transform) child.gameObject.SetActive(false);
            });
            nextButton.onClick.AddListener(StageManager.Instance.NextStage);
        }

        protected override void AssignUiManage()
        {
            UiManager.Instance.AssignUI(this);
        }
    }
}