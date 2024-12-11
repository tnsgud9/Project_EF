using Collections;
using Commons;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIGameOver : BaseUI
    {
        public Button restartButton;
        public Button exitButton;

        protected void Start()
        {
            foreach (Transform child in transform) child.gameObject.SetActive(false);
            EventBus<Enums.Event>.Subscribe(Enums.Event.GameOver, () =>
            {
                foreach (Transform child in transform) child.gameObject.SetActive(true);
            });
            EventBus<Enums.Event>.Subscribe(Enums.Event.StageClear, () =>
            {
                foreach (Transform child in transform) child.gameObject.SetActive(false);
            });
            EventBus<Enums.Event>.Subscribe(Enums.Event.StageReady, () =>
            {
                foreach (Transform child in transform) child.gameObject.SetActive(false);
            });
            EventBus<Enums.Event>.Subscribe(Enums.Event.StageStart, () =>
            {
                foreach (Transform child in transform) child.gameObject.SetActive(false);
            });

            restartButton.onClick.AddListener(() =>
            {
                StageManager.Instance.StageStart();
                // TODO: Scene Restart Init;
            });
        }

        protected override void AssignUiManage()
        {
            UiManager.Instance.AssignUI(this);
        }
    }
}