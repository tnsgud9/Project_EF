using DG.Tweening;
using Managers;
using UnityEngine;

namespace Entities.Weapons
{
    public class Dynamite : Bomb
    {
        private static readonly int ExplosionImminentAnimTrigger = Animator.StringToHash("Imminent");
        private static readonly int ExplodeAnimTrigger = Animator.StringToHash("Explode");
        public float explosionImminentRate = 2f; // 폭발 임박 계수

        [Header("Animation Settings")] public float startIdleAnimSpeed = 0.1f; // 애니메이션의 시작 속도
        public float targetIdleAnimSpeed = 0.5f; // 애니메이션의 목표 속도

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }

        protected override void BeforeExplode()
        {
            DOTween.To(() => startIdleAnimSpeed, x => Animator.speed = x, targetIdleAnimSpeed, explosionImminentRate)
                .SetEase(Ease.Linear)
                .OnComplete(() => Animator.SetTrigger(ExplosionImminentAnimTrigger));
        }

        protected override void Explode()
        {
            CameraManager.Instance.CameraExplosionShake();
            Animator.SetTrigger(ExplodeAnimTrigger);
            ApplyDamage(); // 폭발 후 데미지 적용
        }

        protected override void AfterExplode()
        {
        }

        // 폭발 범위 내 유닛에게 데미지
        protected void ApplyDamage()
        {
            var hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayer);
            foreach (var obj in hitObjects)
            {
                var health = obj.GetComponent<IHealth>();
                if (health is not null) health.TakeDamage(damage);
            }
        }
    }
}