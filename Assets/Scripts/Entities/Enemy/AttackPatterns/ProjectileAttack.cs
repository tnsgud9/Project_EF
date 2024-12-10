using System.Collections;
using Entities.Projectiles;
using UnityEngine;
using UnityEngine.Pool;

// Import the Pool namespace

namespace Entities.Enemy.AttackPatterns
{
    [CreateAssetMenu(fileName = "ProjectileAttack", menuName = "AttackPattern/ProjectileAttack")]
    public class ProjectileAttack : BaseAttackPattern
    {
        public GameObject projectilePrefab; // 발사체 프리팹
        public int projectileCount = 1;
        public float fireDelay = 0.5f;

        // Use ObjectPool<Projectile> from UnityEngine.Pool
        private ObjectPool<Projectile> _projectilePool;

        public override IEnumerator Execute(EnemyAttack enemyAttack, GameObject target = null)
        {
            // Initialize the ObjectPool for projectiles if it's not already initialized
            if (_projectilePool == null)
                // Create a pool for the projectiles
                _projectilePool = new ObjectPool<Projectile>(
                    CreateProjectile,
                    OnTakeFromPool,
                    OnReturnToPool,
                    OnDestroyProjectile,
                    false, // Whether to allow growth of the pool
                    10, // Initial pool size (can adjust as needed)
                    20 // Maximum pool size (can adjust as needed)
                );

            // Fire projectiles
            for (var i = 0; i < projectileCount; i++)
            {
                // Get a projectile from the pool
                var projectile = _projectilePool.Get();
                var direction = (target.transform.position - enemyAttack.transform.position).normalized;

                // Initialize the projectile with position, direction, and optional target
                projectile.Initialize(enemyAttack.transform.position, direction, _projectilePool);

                yield return new WaitForSeconds(fireDelay);
            }

            enemyAttack.StopCurrentPattern();
        }

        public override void Update(EnemyAttack enemyAttack, GameObject target = null)
        {
            // Add any updates for the attack pattern (if needed)
        }

        // Methods for managing the pool
        private Projectile CreateProjectile()
        {
            // Instantiate a new projectile from the prefab
            var projectileInstance = Instantiate(projectilePrefab).GetComponent<Projectile>();
            return projectileInstance;
        }

        private void OnTakeFromPool(Projectile projectile)
        {
            // Optionally, reset the projectile when it's taken from the pool
            projectile.gameObject.SetActive(true);
        }

        public void OnReturnToPool(Projectile projectile)
        {
            // Optionally, reset the projectile state when returned to the pool
            projectile.gameObject.SetActive(false);
        }

        private void OnDestroyProjectile(Projectile projectile)
        {
            // Destroy the projectile if necessary (only if it can’t be reused)
            Destroy(projectile.gameObject);
        }
    }
}