using System;
using System.Collections.Generic;
using Collections;
using Entities.Abilities;
using Entities.Enemy.AttackPatterns;
using Managers;
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

        [SerializeField] private bool isCurrentPatternRunning;

        [HideInInspector] public GameObject indicator;
        [HideInInspector] public SpriteRenderer indicatorRenderer;
        private GameObject _target; // 공격 대상 (필요시 할당)

        private void Update()
        {
            // 패턴 실행 중이거나 currentPattern이 없으면 실행
            // TODO: ScriptableObject 상에서 사용가능한 IState로 리펙토링
            if (currentPattern is not null && isCurrentPatternRunning)
            {
                currentPattern.AttackUpdate(this, _target);
                return;
            }

            currentPattern = SelectPattern();
            StartCoroutine(currentPattern.AttackStart(this, _target));
            isCurrentPatternRunning = true;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _target = GameManager.Instance.playerController.gameObject;
            if (currentPattern is not null)
            {
                isCurrentPatternRunning = true;
                StartCoroutine(currentPattern.AttackStart(this, _target));
            }
        }

        private void OnDisable()
        {
            StopCurrentPattern();
            currentPattern = null;
        }

        public void AddEffect(AbilityData abilityData)
        {
            throw new NotImplementedException();
        }

        public void StopCurrentPattern()
        {
            isCurrentPatternRunning = false;
        }

        private BaseAttackPattern SelectPattern()
        {
            if (currentPattern?.nextPattern is not null)
                return currentPattern.nextPattern;
            return attackPatterns[Random.Range(0, attackPatterns.Count)];
        }
    }
}