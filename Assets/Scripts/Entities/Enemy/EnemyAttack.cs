using System;
using System.Collections.Generic;
using Collections;
using Cysharp.Threading.Tasks;
using Entities.Abilities;
using Entities.Enemy.AttackPatterns;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Enemy
{
    // TODO: 유닛들과 상태 전이 등을 위해 인터페이스가 필요하나 아직 미구현 상태로 임시로 EnemyAttack은 인터페이스 없이 객체로 사용한다.
    public class EnemyAttack : BaseBehaviour, IAbility
    {
        // 여러 개의 스크립트를 리스트로 관리
        public List<BaseAttackPattern> attackPatterns; // 여러 패턴 목록
        public BaseAttackPattern currentPattern;
        [InjectChild] public Animator animator;
        private GameObject target; // 공격 대상 (필요시 할당)

        private void Update()
        {
            // 패턴 실행 중이거나 currentPattern이 없으면 실행
            if (currentPattern is not null) return;
            currentPattern = SelectRandomPattern();
            ExecuteCurrentPattern().Forget(); // UniTask 실행
        }

        public void AddEffect(AbilityData abilityData)
        {
            throw new NotImplementedException();
        }

        private async UniTask ExecuteCurrentPattern()
        {
            // 현재 패턴 실행
            await currentPattern.Execute(this, target);
            currentPattern = currentPattern.nextPattern;
        }

        private BaseAttackPattern SelectRandomPattern()
        {
            return attackPatterns[Random.Range(0, attackPatterns.Count)];
        }
    }
}