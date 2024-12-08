using Collections;

namespace Entities.Enemy
{
    public class EnemyAliveState : IState<EnemyController>
    {
        public void StateStart(EnemyController controller)
        {
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
        }

        public void StateUpdate(EnemyController controller)
        {
        }

        public void StateEnd(EnemyController controller)
        {
        }
    }
}