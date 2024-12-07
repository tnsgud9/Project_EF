using System;
using Collections;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyAliveState : IState<EnemyController>
    {
        public void Start(EnemyController controller)
        {
        }

        public void Update(EnemyController controller)
        {
        }

        public void End(EnemyController controller)
        {
        }
    }

    public class EnemyDeathState : IState<EnemyController>
    {
        public void Start(EnemyController controller)
        {
        }

        public void Update(EnemyController controller)
        {
        }

        public void End(EnemyController controller)
        {
        }
    }
}