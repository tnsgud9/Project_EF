using System;
using Collections;
using Commons;
using Entities.Abilities;
using Managers;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyController : BaseBehaviour, IController
    {
        [InjectAdd] private AudioSource _audioSource;
        [Inject] private EnemyAttack _enemyAttack;
        private StateContext<EnemyController> _stateContext;
        private EnemyAliveState _enemyAliveState;
        private EnemyDeathState _enemyDeathState;

        [Inject] private IHealth _health;

        protected override void OnEnable()
        {
            base.OnEnable();
            AudioSystem = new AudioSystem(_audioSource);
            _enemyAliveState = new EnemyAliveState();
            _enemyDeathState = new EnemyDeathState();
            _stateContext.CurrentState = _enemyAliveState;
            Health.OnDie += () =>
            {
                _stateContext.CurrentState = _enemyDeathState;
            };
            // Health.DamageCallback += (int health) => { _enemyHealthGauge.SetHealth(health);  };
            GameManager.Instance.totalEnemyMaxHealth += _health.MaxHealth;
        }

        private void OnDisable()
        {
            // TODO: 컨트롤러들이 직접 GameManager를 참조하여 호출하는 방식은 안티 패턴 개선 필요
            GameManager.Instance.totalEnemyMaxHealth -= _health.MaxHealth;
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