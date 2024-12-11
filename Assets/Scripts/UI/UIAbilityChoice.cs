using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIAbilityChoice : BaseUI
    {
        public int showCardCount = 3;
        public RectTransform cardParentTransform;
        public GameObject cardPrefab;

        private void Start()
        {
            // TODO : 안티 코드 되는데로 짠거 바꿔야한다.
            gameObject.SetActive(false);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            var abilities = GameManager.Instance.abilities;
            //TODO :  참조 방식 수정 
            var selectedAbilities =
                Logic.GetUniqueRandomNumbers(0, abilities.Count, showCardCount);
            // RectTransform을 사용하는 경우, 자식들을 순회하여 삭제
            foreach (Transform child in cardParentTransform) Destroy(child.gameObject);
            foreach (var abilityIndex in selectedAbilities)
            {
                var cardObject = Instantiate(cardPrefab, cardParentTransform);
                cardObject.GetComponentInChildren<Text>().text = abilities[abilityIndex].description;
                cardObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    gameObject.SetActive(false);
                    GameManager.Instance.AddAbility(abilities[abilityIndex]);
                    abilities.RemoveAt(abilityIndex);

                    StageManager.Instance.NextStage();
                });
            }
        }

        protected override void AssignUiManage()
        {
            UiManager.Instance.AssignUI(this);
        }
    }
}