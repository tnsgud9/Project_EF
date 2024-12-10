using System.Collections.Generic;
using Entities.Abilities;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace UI
{
    public class UIAbilityChoice : BaseUI
    {
        public int showCardCount = 3;
        public RectTransform cardParentTransform;
        public GameObject cardPrefab;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            //TODO :  참조 방식 수정 
            var selectedAbilities = PickRandomAbilities(GameManager.Instance.abilities, showCardCount);
            // RectTransform을 사용하는 경우, 자식들을 순회하여 삭제
            foreach (Transform child in cardParentTransform) Destroy(child.gameObject);
            foreach (var ability in selectedAbilities)
            {
                var cardObject = Instantiate(cardPrefab, cardParentTransform);
                cardObject.GetComponentInChildren<Text>().text = ability.description;
                cardObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    gameObject.SetActive(false);
                    GameManager.Instance.AddAbility(ability);
                    RemoveAbilitiesFromList(ref selectedAbilities, ability);
                });
            }
        }

        protected override void AssignUiManage()
        {
            UiManager.Instance.AssignUI(this);
        }

        // 랜덤으로 값을 뽑는 함수
        public List<AbilityData> PickRandomAbilities(List<AbilityData> list, int count)
        {
            var pickedValues = new List<AbilityData>();
            var copyList = new List<AbilityData>(list); // 원본 리스트를 복사

            var random = new Random();
            for (var i = 0; i < count; i++)
            {
                var randomIndex = random.Next(copyList.Count);
                pickedValues.Add(copyList[randomIndex]);
                copyList.RemoveAt(randomIndex); // 뽑은 값 삭제
            }

            return pickedValues;
        }

        // 리스트에서 특정 값들을 삭제하는 함수
        private void RemoveAbilitiesFromList(ref List<AbilityData> list, List<AbilityData> deleteAbilities)
        {
            foreach (var value in deleteAbilities) RemoveAbilitiesFromList(ref list, value);
        }

        private void RemoveAbilitiesFromList(ref List<AbilityData> list, AbilityData deselectedAbility)
        {
            list.Remove(deselectedAbility);
        }
    }
}