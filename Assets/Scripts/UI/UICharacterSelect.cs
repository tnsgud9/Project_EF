using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UICharacterSelect : BaseUI
    {
        [SerializeField] private List<GameObject> characterPrefabs;
        public RectTransform buttonParent;

        protected override void OnEnable()
        {
            base.OnEnable();
            foreach (Transform button in buttonParent) Destroy(button.gameObject);
            foreach (var buttonPrefab in characterPrefabs)
            {
                var buttonObj = Instantiate(buttonPrefab, buttonParent);
                buttonObj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    // TODO:튜플로 생성할 수있게 수정 필요.
                });
            }
        }

        protected override void AssignUiManage()
        {
            UiManager.Instance.AssignUI(this);
        }
    }
}