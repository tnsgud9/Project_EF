using DG.Tweening;
using Managers;
using UnityEngine;

namespace Entities.Weapons
{
    public class Dynamite : Bomb
    {
        private static readonly int ExplosionImminentAnimTrigger = Animator.StringToHash("Imminent");
        private static readonly int ExplodeAnimTrigger = Animator.StringToHash("Explode");

        [Header("Animation Settings")] public float explosionImminentRate = 2f; // 폭발 임박 계수
        public float startIdleAnimSpeed = 0.1f; // 애니메이션의 시작 속도
        public float targetIdleAnimSpeed = 0.5f; // 애니메이션의 목표 속도

        [Header("Audio Settings")] public AudioPreset explodeSound;
        public AudioPreset explodeHitSound;
        public AudioPreset explodeFuseSound;
        public AudioPreset plantSound;

        private Vector3 _originalScale;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }

        protected override void BeforeExplode()
        {
            AudioSystem.PlayOneShot(plantSound);
            AudioSystem.Play(explodeFuseSound);
            DOTween.To(() => startIdleAnimSpeed, x => Animator.speed = x, targetIdleAnimSpeed, explosionImminentRate)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Animator.SetTrigger(ExplosionImminentAnimTrigger);
                    AudioSystem.Stop();
                });
            _originalScale = SpriteRenderer.transform.localScale;
        }

        protected override void Explode()
        {
            AudioSystem.Play(explodeSound);
            // Sprite의 원래 크기 (픽셀 단위)
            Vector2 spriteSize = SpriteRenderer.sprite.bounds.size;
            SpriteRenderer.sortingOrder = 1000;
            // 원의 반지름에 맞게 크기를 설정
            // radius에 맞추기 위해서 Scale 비율을 계산합니다.
            var scaleFactor = explosionRadius * 2f / Mathf.Max(spriteSize.x, spriteSize.y);
            // 크기 조정
            SpriteRenderer.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);


            CameraManager.Instance.CameraExplosionShake();
            Animator.SetTrigger(ExplodeAnimTrigger);
            ApplyDamage(); // 폭발 후 데미지 적용
        }

        protected override void AfterExplode()
        {
            SpriteRenderer.transform.localScale = _originalScale;
        }

        // 폭발 범위 내 유닛에게 데미지
        protected void ApplyDamage()
        {
            var hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayer);
            foreach (var obj in hitObjects)
            {
                var controller = obj.GetComponent<IController>();
                if (controller == null)
                {
                    Debug.LogWarning($"{gameObject.name} : Controller is not exist or null");
                    continue;
                }

                controller.Health.TakeDamage(damage);
                controller.AudioSystem.PlayOneShot(explodeHitSound);
            }
        }
    }
}