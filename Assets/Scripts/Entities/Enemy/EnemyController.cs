using System;
using Collections;
using Commons;
using Entities.Abilities;
using Managers;
using UI;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyController : BaseBehaviour, IController
    {
        [HideInInspector] [Inject] public EnemyAttack enemyAttack;
        [HideInInspector] [Inject] public Rigidbody2D rigidbody;
        [HideInInspector] [InjectChild] public SpriteRenderer spriteRenderer;
        [InjectAdd] private AudioSource _audioSource;
        private EnemyAliveState _enemyAliveState;
        private EnemyDeathState _enemyDeathState;
        private EnemyReadyState _enemyReadyState;
        [Inject] private Health _health;
        private StateContext<EnemyController> _stateContext;
        [Header("UI Settings")] private UIHealthBar _uiHealthBar;

        private void Start()
        {
            // Exta Component Init
            AudioSystem = new AudioSystem(_audioSource);
            // _uiHealthBar.AddHealthTracking(_health);
            _uiHealthBar = UiManager.Instance.GetUI<UIHealthBar>();
            _uiHealthBar.AddHealthTracking(_health);

            // EventBinding
            _health.OnDie += () => { _stateContext.CurrentState = _enemyDeathState; };
            // Health.OnHealthChanged += (currentHealth, damage) => { _uiHealthBar.RefreshHealthBar(); };
            EventBus<Enums.Event>.Subscribe(Enums.Event.StageReady,
                () =>
                {
                    _stateContext.CurrentState = _enemyReadyState;
                    GetComponent<Collider2D>().enabled = true;
                });
            EventBus<Enums.Event>.Subscribe(Enums.Event.StageStart,
                () => { _stateContext.CurrentState = _enemyAliveState; });
            EventBus<Enums.Event>.Subscribe(Enums.Event.GameOver,
                () => { GetComponent<Collider2D>().enabled = false; });
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _stateContext = new StateContext<EnemyController>(this);
            _enemyAliveState = new EnemyAliveState();
            _enemyDeathState = new EnemyDeathState();
            _enemyReadyState = new EnemyReadyState();
            _stateContext.CurrentState = _enemyReadyState;
        }

        private void OnDestroy()
        {
            if (_health != null) _uiHealthBar?.DestroyHealthTracking(_health);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.GetComponent<IController>()?.KnockBack(5f);
        }

        public IAudioSystem AudioSystem { get; set; }

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