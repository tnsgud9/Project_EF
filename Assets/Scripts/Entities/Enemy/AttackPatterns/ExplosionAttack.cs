using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Entities.Enemy.AttackPatterns
{
    [Serializable]
    [CreateAssetMenu(fileName = "ExplosionAttack", menuName = "AttackPattern/ExplosionAttack")]
    public class ExplosionAttack : BaseAttackPattern
    {
        public float radius = 1.5f; // ReSharper disable Unity.PerformanceAnalysis
        public float explosionDelay = 1.2f;
        public float exploedDelay = 1.2f;

        // ReSharper disable Unity.PerformanceAnalysis
        public GameObject indicatorPrefab;
        private GameObject _indicator;

        public override IEnumerator Execute(EnemyAttack enemyAttack, GameObject target = null)
        {
            // 인디게이터 생성
            if (_indicator.IsUnityNull())
                _indicator = Instantiate(indicatorPrefab, enemyAttack.gameObject.transform);
            Vector2 spriteSize = _indicator.GetComponent<SpriteRenderer>().sprite.bounds.size;
            var scaleFactor = radius * 2f / Mathf.Max(spriteSize.x, spriteSize.y);
            _indicator.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
            _indicator.SetActive(true);

            // 폭발 딜레이 
            yield return new WaitForSeconds(explosionDelay);

            // 폭탄 로직 실행
            _indicator.SetActive(false);

            // 데미지 적용
            var hitColliders = Physics2D.OverlapCircleAll(enemyAttack.transform.position, radius, damageableLayer);
            foreach (var hitCollider in hitColliders) hitCollider.GetComponent<IHealth>().TakeDamage();

            yield return new WaitForSeconds(exploedDelay);

            enemyAttack.StopCurrentPattern();
            yield return null;
        }

        public override void Update(EnemyAttack enemyAttack, GameObject target = null)
        {
        }
    }
}