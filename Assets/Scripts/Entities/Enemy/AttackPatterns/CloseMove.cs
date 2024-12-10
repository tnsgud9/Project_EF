using System;
using System.Collections;
using UnityEngine;

namespace Entities.Enemy.AttackPatterns
{
    [Serializable]
    [CreateAssetMenu(fileName = "CloseMove", menuName = "AttackPattern/CloseMove")]
    public class CloseMove : BaseAttackPattern
    {
        public float moveSpeed = 1f; // 이동 속도
        public float stopDistance = 0.3f; // 타겟과의 최소 거리 (이 거리에 도달하면 멈춤)
        public float timeLimit = 3f; // 제한 시간 (초)
        private float _timeElapsed; // 경과 시간

        public override IEnumerator Execute(EnemyAttack enemyAttack, GameObject target = null)
        {
            _timeElapsed = 0f;
            yield return null;
        }

        public override void Update(EnemyAttack enemyAttack, GameObject target = null)
        {
            if (target is null) Debug.LogWarning("AttackPattern CloseMove target is null : pattern skip");
            if (_timeElapsed >= timeLimit)
            {
                enemyAttack.StopCurrentPattern();
                return; // 제한 시간을 초과했으므로, 이동을 멈춤
            }

            // 타겟과의 거리 계산
            var distance = Vector3.Distance(enemyAttack.transform.position, target!.transform.position);

            // 타겟과의 거리가 stopDistance보다 크면 따라가기
            if (distance > stopDistance)
            {
                // 타겟 방향으로 이동
                var direction = (target.transform.position - enemyAttack.transform.position).normalized;
                enemyAttack.transform.position += direction * (moveSpeed * Time.deltaTime);
            }
            else
            {
                enemyAttack.StopCurrentPattern();
                return;
            }

            // 경과 시간 업데이트
            _timeElapsed += Time.deltaTime;
        }

        // 경과 시간을 리셋하고 다시 시작하려면 이 메서드를 호출할 수 있습니다
        public void ResetTimer()
        {
            _timeElapsed = 0f;
        }
    }
}