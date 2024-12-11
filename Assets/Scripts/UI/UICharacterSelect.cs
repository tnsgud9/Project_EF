using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class UICharacterSet
    {
        public Sprite icon;
        public GameObject weaponPrefab;
        public string name;
    }

    public class UICharacterSelect : BaseUI
    {
        public RectTransform buttonParent;
        public GameObject buttonPrefab;
        public List<UICharacterSet> characterSets;

        protected override void OnEnable()
        {
            base.OnEnable();
            foreach (Transform button in buttonParent) Destroy(button.gameObject);
            foreach (var set in characterSets)
            {
                var buttonObj = Instantiate(buttonPrefab, buttonParent);
                buttonObj.GetComponentsInChildren<Image>()[1].sprite = set.icon;
                buttonObj.GetComponentInChildren<Text>().text = set.name;
                buttonObj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameManager.Instance.playerController.playerAttack.bombPrefab = set.weaponPrefab;
                    gameObject.SetActive(false);
                    StageManager.Instance.StageStart();
                });
            }
        }

        protected override void AssignUiManage()
        {
            UiManager.Instance.AssignUI(this);
        }
    }
}