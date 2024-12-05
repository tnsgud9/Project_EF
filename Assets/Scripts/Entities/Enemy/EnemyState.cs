using System;
using Collections;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyState
    {
    }

    public class IdleState : ScriptableObject, IState<EnemyController>
    {
        public void Start(EnemyController controller)
        {
            throw new NotImplementedException();
        }

        public void Update(EnemyController controller)
        {
            throw new NotImplementedException();
        }

        public void End(EnemyController controller)
        {
            throw new NotImplementedException();
        }
    }
}