using System.Collections;
using Entities.Projectiles;
using UnityEngine;
using UnityEngine.Pool;

namespace Entities.Enemy.AttackPatterns
{
    [CreateAssetMenu(fileName = "SpiralProjectileAttack", menuName = "AttackPattern/SpiralProjectileAttack")]
    public class SpiralProjectileAttack : BaseAttackPattern
    {
        public GameObject projectilePrefab;
        public int projectileCount = 20; // 총 발사체 수
        public float fireRate = 0.1f; // 발사 간격
        public float spiralSpeed = 10f; // 나선 회전 속도
        public float spiralRadiusIncrement = 0.1f; // 각 발사체마다 반지름 증가량

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

            var currentAngle = 0f;
            var currentRadius = 0f;

            for (var i = 0; i < projectileCount; i++)
            {
                // 나선형 좌표 계산
                var spawnPos = CalculateSpiralPosition(enemyAttack.transform.position, currentAngle, currentRadius);
                var direction = (spawnPos - enemyAttack.transform.position).normalized;

                var projectile = _projectilePool.Get();
                projectile.Initialize(spawnPos, direction, _projectilePool);

                // 각도와 반지름 업데이트
                currentAngle += spiralSpeed;
                currentRadius += spiralRadiusIncrement;

                yield return new WaitForSeconds(fireRate);
            }

            enemyAttack.StopCurrentPattern();
        }

        public override void AttackUpdate(EnemyAttack enemyAttack, GameObject target = null)
        {
            // 필요한 경우 추가 업데이트 로직 작성
        }

        private Vector3 CalculateSpiralPosition(Vector3 center, float angle, float radius)
        {
            var x = center.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            var y = center.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            return new Vector3(x, y, center.z);
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