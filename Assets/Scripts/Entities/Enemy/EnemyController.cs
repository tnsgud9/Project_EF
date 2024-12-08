using System;
using Collections;
using Commons;
using Entities.Abilities;
using UI;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyController : BaseBehaviour, IController
    {
        [Header("UI Settings")] public UIHealthBar uiHealthBar;

        [InjectAdd] private AudioSource _audioSource;
        private EnemyAliveState _enemyAliveState;
        [Inject] private EnemyAttack _enemyAttack;
        private EnemyDeathState _enemyDeathState;
        [Inject] private Health _health;
        private StateContext<EnemyController> _stateContext;

        private void Start()
        {
            // Exta Component Init
            AudioSystem = new AudioSystem(_audioSource);
            _stateContext = new StateContext<EnemyController>(this);
            _enemyAliveState = new EnemyAliveState();
            _enemyDeathState = new EnemyDeathState();

            uiHealthBar.AddHealthTracking(_health);

            // EventBinding
            Health.OnDie += () => { _stateContext.CurrentState = _enemyDeathState; };
            Health.OnHealthChanged += (currentHealth, damage) => { uiHealthBar.RefreshHealthBar(); };
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _stateContext.CurrentState = _enemyAliveState;
        }

        private void OnDestroy()
        {
            // TODO: HealthTracker에서 빼기
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