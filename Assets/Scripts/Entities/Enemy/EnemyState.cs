using Collections;
using DG.Tweening;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyReadyState : IState<EnemyController>
    {
        public void StateStart(EnemyController controller)
        {
            controller.enemyAttack.enabled = false;
        }

        public void StateUpdate(EnemyController controller)
        {
        }

        public void StateEnd(EnemyController controller)
        {
        }
    }

    public class EnemyAliveState : IState<EnemyController>
    {
        public void StateStart(EnemyController controller)
        {
            controller.enemyAttack.enabled = true;
            var currentColor = controller.spriteRenderer.color;
            currentColor.a = 1f;
            controller.spriteRenderer.color = currentColor;
        }

        public void StateUpdate(EnemyController controller)
        {
        }

        public void StateEnd(EnemyController controller)
        {
        }
    }

    public class EnemyDeathState : IState<EnemyController>
    {
        public void StateStart(EnemyController controller)
        {
            controller.enemyAttack.enabled = false;
            controller.GetComponent<Collider2D>().enabled = false;
            // 알파 값 변경 (0으로 서서히 변경)
            controller.spriteRenderer.DOFade(0f, 1f);
        }

        public void StateUpdate(EnemyController controller)
        {
        }

        public void StateEnd(EnemyController controller)
        {
        }
    }
}