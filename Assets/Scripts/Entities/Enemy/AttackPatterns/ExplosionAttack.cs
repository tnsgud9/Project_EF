using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Entities.Enemy.AttackPatterns
{
    [Serializable]
    [CreateAssetMenu(fileName = "ExplosionAttack", menuName = "AttackPattern/Explosion")]
    public class ExplosionAttack : BaseAttackPattern
    {
        public float radius = 1.5f; // ReSharper disable Unity.PerformanceAnalysis

        public override async UniTask Execute(EnemyAttack enemyAttack, GameObject target = null)
        {
            Debug.Log("Execute ExplosionAttack");
            // TODO: 공격 범위 인디케이터
            await UniTask.Delay(1000);
            var hitColliders = Physics.OverlapSphere(enemyAttack.transform.position, radius, damageableLayer);
            foreach (var hitCollider in hitColliders) hitCollider.GetComponent<IHealth>().TakeDamage(1);
        }
    }
}