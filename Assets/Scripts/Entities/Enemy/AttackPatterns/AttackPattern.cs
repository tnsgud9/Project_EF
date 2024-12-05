using System;
using UnityEngine;

namespace Entities.Enemy.AttackPatterns
{
    [Serializable]
    public class AttackPattern
    {
        public string patternName; // 공격 패턴 이름
        public float damage; // 공격 데미지
        public float attackSpeed; // 공격 속도 (초당 공격 횟수)
        public float attackRange; // 공격 범위 (근접, 원거리, 장거리 공격에 맞게 사용)

        // 공격 패턴을 실행하는 기본 메소드
        public virtual void ExecuteAttack()
        {
            Debug.Log($"{patternName} attack executed with {damage} damage at {attackSpeed} speed.");
        }
    }
}