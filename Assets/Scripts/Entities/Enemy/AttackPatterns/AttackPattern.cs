using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Entities.Enemy.AttackPatterns
{
    public abstract class BaseAttackPattern : ScriptableObject
    {
        public LayerMask damageableLayer = LayerMask.NameToLayer("Player"); // 데미지를 받을 레이어 
        [CanBeNull] public BaseAttackPattern nextPattern;
        public abstract UniTask Execute(EnemyAttack enemyAttack, GameObject target = null);
    }
}