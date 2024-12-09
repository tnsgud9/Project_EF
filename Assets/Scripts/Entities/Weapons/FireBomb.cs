using System;
using System.Collections;
using UnityEngine;

namespace Entities.Weapons
{
    public class FireBomb : Bomb
    {
        public int dotDamageCount = 3;
        public float dotDamageDelay = 0.3f;
        [Header("Animation Settings")] public float explosionImminentRate = 2f; // 폭발 임박 계수
        public float startIdleAnimSpeed = 0.1f; // 애니메이션의 시작 속도
        public float targetIdleAnimSpeed = 0.5f; // 애니메이션의 목표 속도

        [Header("Audio Settings")] public AudioPreset explodeSound;
        public AudioPreset explodeHitSound;
        public AudioPreset explodeFuseSound;
        public AudioPreset plantSound;


        protected override void BeforeExplode()
        {
            throw new NotImplementedException();
        }

        protected override IEnumerator Explode()
        {
            for (var i = 0; i < dotDamageCount; i++)
            {
                ApplyDamage();
                yield return new WaitForSeconds(dotDamageDelay);
            }

            yield return null;
        }

        protected override void AfterExplode()
        {
            throw new NotImplementedException();
        }


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