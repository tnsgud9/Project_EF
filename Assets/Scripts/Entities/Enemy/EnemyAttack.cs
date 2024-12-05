using System;
using System.Collections.Generic;
using Collections;
using Entities.Abilities;
using Entities.Enemy.AttackPatterns;

namespace Entities.Enemy
{
    // TODO: 유닛들과 상태 전이 등을 위해 인터페이스가 필요하나 아직 미구현 상태로 임시로 객체로 사용한다.
    public class EnemyAttack : BaseBehaviour, IAbility
    {
        // 여러 개의 스크립트를 리스트로 관리
        public List<AttackPattern> attackPatterns;
        private StateContext<EnemyAttack> _attackStateContext;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        public void ApplyEffect(AbilityData abilityData)
        {
            throw new NotImplementedException();
        }
    }
}