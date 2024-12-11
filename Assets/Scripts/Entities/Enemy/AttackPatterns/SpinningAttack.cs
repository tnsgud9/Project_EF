using System.Collections;
using Entities.Projectiles;
using UnityEngine;
using UnityEngine.Pool;

namespace Entities.Enemy.AttackPatterns
{
    [CreateAssetMenu(fileName = "SpinningAttack", menuName = "AttackPattern/SpinningAttack")]
    public class SpinningAttack : BaseAttackPattern
    {
        public GameObject projectilePrefab;
        public int projectileCount = 8;
        public float rotationSpeed = 90f;
        public float attackDuration = 3f;
        public float projectileSpawnInterval = 0.5f; // 발사체 생성 간격 (초)

        private ObjectPool<Projectile> _projectilePool;

        public override IEnumerator AttackStart(EnemyAttack enemyAttack, GameObject target = null)
        {
            if (_projectilePool == null)
                _projectilePool = new ObjectPool<Projectile>(
                    CreateProjectile,
                    OnTakeFromPool,
                    OnReturnToPool,
                    OnDestroyProjectile,
                    false,
                    10,
                    20
                );

            var elapsedTime = 0f;
            var angleStep = 360f / projectileCount;
            var currentAngle = 0f;
            var lastSpawnTime = -projectileSpawnInterval; // 마지막 발사체 생성 시간 초기화

            while (elapsedTime < attackDuration)
            {
                // 발사체 생성 간격 체크
                if (elapsedTime - lastSpawnTime >= projectileSpawnInterval)
                {
                    for (var i = 0; i < projectileCount; i++)
                    {
                        var projectileAngle = currentAngle + i * angleStep;
                        var direction = CalculateProjectileDirection(projectileAngle);

                        var projectile = _projectilePool.Get();
                        projectile.Initialize(enemyAttack.transform.position, direction, _projectilePool);
                    }

                    lastSpawnTime = elapsedTime; // 마지막 생성 시간 업데이트
                }

                currentAngle += rotationSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            enemyAttack.StopCurrentPattern();
        }

        public override void AttackUpdate(EnemyAttack enemyAttack, GameObject target = null)
        {
            // 필요한 경우 업데이트 로직 추가
        }

        private Vector3 CalculateProjectileDirection(float angle)
        {
            var x = Mathf.Cos(angle * Mathf.Deg2Rad);
            var y = Mathf.Sin(angle * Mathf.Deg2Rad);
            return new Vector3(x, y, 0).normalized;
        }

        private Projectile CreateProjectile()
        {
            return Instantiate(projectilePrefab).GetComponent<Projectile>();
        }

        private void OnTakeFromPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }

        private void OnReturnToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void OnDestroyProjectile(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }
    }
}