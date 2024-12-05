using System;
using UnityEngine;

namespace Entities.Enemy.AttackPatterns
{
    [Serializable]
    public class CircleAttack : AttackPattern
    {
        public CircleAttack()
        {
            patternName = "Melee Attack";
            damage = 20f;
            attackSpeed = 1.5f;
            attackRange = 2f; // 근접 공격 범위
        }

        public override void ExecuteAttack()
        {
            // 근접 공격 실행 (범위 내의 적을 타겟으로 하여 공격)
            Debug.Log(
                $"{patternName} executed with {damage} damage at {attackSpeed} speed within {attackRange}m range.");
            // 근접 공격 로직을 여기에 구현 (예: 근처의 적을 찾아서 데미지를 입힘)
        }
    }
}