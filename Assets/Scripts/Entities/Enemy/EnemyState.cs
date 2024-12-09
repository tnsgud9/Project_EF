using Collections;

namespace Entities.Enemy
{
    public class EnemyAliveState : IState<EnemyController>
    {
        public void StateStart(EnemyController controller)
        {
            controller.enemyAttack.enabled = true;
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
        }

        public void StateUpdate(EnemyController controller)
        {
        }

        public void StateEnd(EnemyController controller)
        {
        }
    }
}