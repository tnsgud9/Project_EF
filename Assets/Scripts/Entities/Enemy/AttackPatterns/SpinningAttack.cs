using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemy.AttackPatterns
{
    [CreateAssetMenu(fileName = "SpinningAttack", menuName = "AttackPattern/SpinningAttack")]
    public class SpinningAttack : BaseAttackPattern
    {
        public GameObject projectilePrefab;
        public int projectileCount = 8;
        public float rotationSpeed = 30f;
        public float attackDuration = 3f;

        public override IEnumerator AttackStart(EnemyAttack enemyAttack, GameObject target = null)
        {
            var elapsedTime = 0f;
            var projectiles = new List<GameObject>();

            // 발사체 생성 및 원형 배치
            for (var i = 0; i < projectileCount; i++)
            {
                var angle = i * Mathf.PI * 2 / projectileCount;
                var positionOffset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 1.5f;
                var projectile = Instantiate(projectilePrefab, enemyAttack.transform.position + positionOffset,
                    Quaternion.identity);
                projectiles.Add(projectile);
            }

            while (elapsedTime < attackDuration)
            {
                elapsedTime += Time.deltaTime;

                // 발사체 회전
                for (var i = 0; i < projectiles.Count; i++)
                {
                    var angle = i * Mathf.PI * 2 / projectileCount + rotationSpeed * elapsedTime;
                    var positionOffset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 1.5f;
                    if (projectiles[i] != null)
                        projectiles[i].transform.position = enemyAttack.transform.position + positionOffset;
                }

                yield return null;
            }

            // 발사체 제거
            foreach (var projectile in projectiles)
                if (projectile != null)
                    Destroy(projectile);

            enemyAttack.StopCurrentPattern();
        }

        public override void AttackUpdate(EnemyAttack enemyAttack, GameObject target = null)
        {
        }
    }
}