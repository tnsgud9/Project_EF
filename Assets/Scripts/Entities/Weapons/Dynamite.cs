using UnityEngine;

namespace Entities.Weapons
{
    public class Dynamite : Bomb
    {
        protected override void Explode()
        {
            ApplyDamage(); // 폭발 후 데미지 적용

            HandleExplosionComplete();
        }
        
        // 폭발 범위 내 유닛에게 데미지
        protected void ApplyDamage()
        {
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayer);
            foreach (var obj in hitObjects)
            {
                IHealth health = obj.GetComponent<IHealth>();
                if (health is not null)
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }
}