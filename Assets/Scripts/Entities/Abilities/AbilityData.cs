using UnityEngine;

namespace Entities.Abilities
{
    [CreateAssetMenu(fileName = "Augment", menuName = "TFT/Augment")]
    public class AbilityData : ScriptableObject
    {
        [Header("General Info")] public string abilityName; // 증강체 이름

        public string description; // 증강체 설명
        // public Sprite icon;

        [Header("Player Modifiers")] public int health; // 최대 체력 증가
        public float attackPower; // 추가 공격력
        public float criticalChance; // 치명타 확률 증가 (퍼센트)
        public float moveSpeed; // 이동 속도 증가
        public int refreshCount; // 선택지 새로고침 횟수 증가
        public float bombSetupTime; // 폭탄 설치 시간 감소
        public int bombPlant; // 폭탄 설치 횟수
        public int bombRadius; // 폭탄 폭발 범위
        public int bombDamage; // 폭탄 폭발 범위

        [Header("Enemy Modifiers")] public float enemyHealth; // 적 체력 감소

        public float enemySpeed; // 적 이동 속도 감소

        [Header("Weapon Modification")] public GameObject weaponPrefab; // 변경할 무기 프리팹 (선택 사항)
    }
}