using UnityEngine;
using UnityEngine.Pool;

namespace Entities.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 5f;
        public float lifetime = 5f;
        public int damage = 10;
        public LayerMask damageableLayer;

        private Vector2 _direction;
        private ObjectPool<Projectile> _projectilePool;
        private float _spawnTime;

        private void Update()
        {
            transform.Translate(_direction * (speed * Time.deltaTime));

            if (Time.time - _spawnTime > lifetime) _projectilePool.Release(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (((1 << collision.gameObject.layer) & damageableLayer) != 0)
            {
                var health = collision.GetComponent<Health>();
                if (health != null) health.TakeDamage(damage);
                _projectilePool.Release(this);
            }
        }

        public void Initialize(Vector2 position, Vector2 direction, ObjectPool<Projectile> projectilePool,
            GameObject target = null)
        {
            transform.position = position;
            _direction = direction.normalized;
            _spawnTime = Time.time;
            _projectilePool = projectilePool;
        }
    }
}