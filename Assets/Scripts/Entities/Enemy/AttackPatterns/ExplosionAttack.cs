﻿using System;
using System.Collections;
using UnityEngine;

namespace Entities.Enemy.AttackPatterns
{
    [Serializable]
    [CreateAssetMenu(fileName = "ExplosionAttack", menuName = "AttackPattern/ExplosionAttack")]
    public class ExplosionAttack : BaseAttackPattern
    {
        public float radius = 1.5f; // ReSharper disable Unity.PerformanceAnalysis
        public float radiusOffset = 0.3f;
        public float explosionDelay = 1.2f;
        public float exploedDelay = 1.2f;

        // ReSharper disable Unity.PerformanceAnalysis
        public GameObject indicatorPrefab;

        public override IEnumerator AttackStart(EnemyAttack enemyAttack, GameObject target = null)
        {
            // 인디게이터 생성
            if (enemyAttack.indicator == null)
            {
                enemyAttack.indicator = Instantiate(indicatorPrefab, enemyAttack.gameObject.transform);
                enemyAttack.indicatorRenderer = enemyAttack.indicator.GetComponent<SpriteRenderer>();
            }

            Vector2 spriteSize = enemyAttack.indicatorRenderer.sprite.bounds.size;
            // TODO: spriteRender를 제어하는 함수 혹은 기능 필요
            enemyAttack.indicatorRenderer.color = new Color(1f, 1f, 0f, 0.2f);
            var scaleFactor = radius * 2f / Mathf.Max(spriteSize.x, spriteSize.y);
            enemyAttack.indicator.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
            enemyAttack.indicator.SetActive(true);

            // 폭발 딜레이 
            yield return new WaitForSeconds(explosionDelay);

            // 폭탄 로직 실행

            enemyAttack.indicatorRenderer.color = new Color(1f, 0f, 0f, 0.2f);
            // 데미지 적용
            var hitColliders =
                Physics2D.OverlapCircleAll(enemyAttack.transform.position, radius - radiusOffset, damageableLayer);
            foreach (var hitCollider in hitColliders) hitCollider.GetComponent<Health>().TakeDamage();

            yield return new WaitForSeconds(0.1f);
            enemyAttack.indicator.SetActive(false);

            yield return new WaitForSeconds(exploedDelay);

            enemyAttack.StopCurrentPattern();
            yield return null;
        }

        public override void AttackUpdate(EnemyAttack enemyAttack, GameObject target = null)
        {
        }
    }
}