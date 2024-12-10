using System.Collections.Generic;
using System.Linq;
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
                });
            }
        }

        protected override void AssignUiManage()
        {
            UiManager.Instance.AssignUI(this);
        }


        private List<int> GetUniqueRandomNumbers(int min, int max, int count)
        {
            var random = new Random();

            // Range 내에서 값을 생성한 후 랜덤하게 섞고, 원하는 개수만큼 추출
            return Enumerable.Range(min, max - min + 1) // min부터 max까지의 숫자 생성
                .OrderBy(x => random.Next()) // 숫자 순서를 랜덤하게 섞기
                .Take(count) // count 개수만큼 선택
                .ToList(); // 리스트로 반환
        }
    }
}