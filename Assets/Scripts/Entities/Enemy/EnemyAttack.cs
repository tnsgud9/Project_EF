using System;
using Collections;

namespace Entities.Enemy
{
    public interface IEnemyAttack
    {
        void Execute(); // 공격 패턴 실행
    }

    public class EnemyAttack : BaseBehaviour, IEnemyAttack
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}