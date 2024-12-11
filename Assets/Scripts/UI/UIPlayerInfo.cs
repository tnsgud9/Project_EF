using Managers;
using UnityEngine;

namespace UI
{
    public class UIPlayerInfo : BaseUI
    {
        public RectTransform playerBombRect;
        public RectTransform playerHealthRect;

        public GameObject bombIconPrefab;
        public GameObject healthIconPrefab;

        protected override void OnEnable()
        {
            base.OnEnable();
            foreach (Transform child in playerHealthRect) Destroy(child.gameObject);
            foreach (Transform child in playerBombRect) Destroy(child.gameObject);
        }

        protected override void AssignUiManage()
        {
            UiManager.Instance.AssignUI(this);
        }

        public void SetPlayerHealth(int count)
        {
            foreach (Transform child in playerHealthRect) child.gameObject.SetActive(false);
            for (var i = 0; i < count; i++)
            {
                var healthIconObject =
                    playerHealthRect.childCount < i ? playerHealthRect.GetChild(i).gameObject : null;
                if (healthIconObject == null)
                    healthIconObject = Instantiate(healthIconPrefab, playerHealthRect.transform);
                healthIconObject.SetActive(true);
            }
        }

        public void SetPlayerBomb(int count)
        {
            foreach (Transform child in playerBombRect) child.gameObject.SetActive(false);
            for (var i = 0; i < count; i++)
            {
                var bombIconObject = playerBombRect.childCount < i ? playerBombRect.GetChild(i).gameObject : null;
                if (bombIconObject == null)
                    bombIconObject = Instantiate(bombIconPrefab, playerBombRect.transform);
                bombIconObject.SetActive(true);
            }
        }
    }
}