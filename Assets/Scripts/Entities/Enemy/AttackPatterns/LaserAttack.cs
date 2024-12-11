using System.Collections;
using UnityEngine;

namespace Entities.Enemy.AttackPatterns
{
    [CreateAssetMenu(fileName = "LaserAttack", menuName = "AttackPattern/LaserAttack")]
    public class LaserAttack : BaseAttackPattern
    {
        public float laserWidth = 0.1f;
        public float maxLength = 50f;
        public float damagePerSecond = 10f;
        public float attackDuration = 3f;
        public Color laserColor = Color.red;
        private LayerMask layersToHit;

        private LineRenderer lineRenderer;

        public override IEnumerator AttackStart(EnemyAttack enemyAttack, GameObject target = null)
        {
            InitializeLaser(enemyAttack.gameObject);

            var elapsedTime = 0f;

            while (elapsedTime < attackDuration)
            {
                UpdateLaser(enemyAttack, target);
                yield return null;
                elapsedTime += Time.deltaTime;
            }

            DisableLaser();
            enemyAttack.StopCurrentPattern();
        }

        private void InitializeLaser(GameObject enemyObject)
        {
            lineRenderer = enemyObject.GetComponent<LineRenderer>();
            if (lineRenderer == null) lineRenderer = enemyObject.AddComponent<LineRenderer>();

            lineRenderer.startWidth = laserWidth;
            lineRenderer.endWidth = laserWidth;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = laserColor;
            lineRenderer.endColor = laserColor;

            layersToHit = LayerMask.GetMask("Default", "Player");
        }

        private void UpdateLaser(EnemyAttack enemyAttack, GameObject target)
        {
            var startPosition = enemyAttack.transform.position;
            var direction = target != null
                ? (target.transform.position - startPosition).normalized
                : enemyAttack.transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(startPosition, direction, out hit, maxLength, layersToHit))
            {
                lineRenderer.SetPosition(0, startPosition);
                lineRenderer.SetPosition(1, hit.point);

                // 레이저에 맞은 대상에게 데미지 적용
                var damageable = hit.collider.GetComponent<Health>();
                if (damageable != null) damageable.TakeDamage();
            }
            else
            {
                lineRenderer.SetPosition(0, startPosition);
                lineRenderer.SetPosition(1, startPosition + direction * maxLength);
            }
        }

        private void DisableLaser()
        {
            if (lineRenderer != null) lineRenderer.enabled = false;
        }

        public override void AttackUpdate(EnemyAttack enemyAttack, GameObject target = null)
        {
            // 필요한 경우 추가 업데이트 로직
        }
    }
}