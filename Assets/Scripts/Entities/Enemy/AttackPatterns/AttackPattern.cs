using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Entities.Enemy.AttackPatterns
{
    public abstract class BaseAttackPattern : ScriptableObject
    {
        public LayerMask damageableLayer; // 데미지를 받을 레이어 
        [CanBeNull] public BaseAttackPattern nextPattern;
        public abstract IEnumerator AttackStart(EnemyAttack enemyAttack, GameObject target = null);
        public abstract void AttackUpdate(EnemyAttack enemyAttack, GameObject target = null);
    }
}