using System;
using Collections;
using Commons;
using Entities.Abilities;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyController : BaseBehaviour, IController
    {
        [InjectAdd] private AudioSource _audioSource;
        private EnemyAliveState _enemyAliveState;
        [Inject] private EnemyAttack _enemyAttack;
        private EnemyDeathState _enemyDeathState;
        [Inject] private Health _health;
        private StateContext<EnemyController> _stateContext;

        protected override void OnEnable()
        {
            base.OnEnable();
            AudioSystem = new AudioSystem(_audioSource);
            _stateContext = new StateContext<EnemyController>(this);
            _enemyAliveState = new EnemyAliveState();
            _enemyDeathState = new EnemyDeathState();
            _stateContext.CurrentState = _enemyAliveState;
            Health.OnDie += () => { _stateContext.CurrentState = _enemyDeathState; };
            // Health.DamageCallback += (int health) => { _enemyHealthGauge.SetHealth(health);  };
        }

        private void OnDisable()
        {
            // TODO: 컨트롤러들이 직접 GameManager를 참조하여 호출하는 방식은 안티 패턴 개선 필요
            // GameManager.Instance.totalEnemyMaxHealth -= _health.MaxHealth;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.GetComponent<IController>()?.KnockBack(5f);
        }

        public IAudioSystem AudioSystem { get; set; }
        [Inject] public IHealth Health { get; set; }

        public void AddAbility(AbilityData abilityData,
            Enums.AbilityMethodType abilityMethodType = Enums.AbilityMethodType.Add)
        {
            throw new NotImplementedException();
        }

        public void KnockBack(float knockBackForce = 1, float timeDelay = 0.6f)
        {
        }
    }
}