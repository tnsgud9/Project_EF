using System;
using Collections;
using Commons;
using Entities.Abilities;
using Managers;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : BaseBehaviour, IController
    {
        [Inject] public PlayerInputHandler inputHandler;
        [InjectChild] private Animator _animator;
        [InjectAdd] private AudioSource _audioSource;
        [Inject] private PlayerMovement _movement;

        private PlayerAliveState _playerAliveState;
        [Inject] private PlayerAttack _playerAttack;
        private PlayerDeathState _playerDeathState;
        private StateContext<PlayerController> _stateContext;

        [Inject] public PlayerController playerController;

        protected override void OnEnable()
        {
            base.OnEnable();
            _stateContext = new StateContext<PlayerController>(this);
            AudioSystem = new AudioSystem(_audioSource);
            // 기본 State 설정
            _playerAliveState = new PlayerAliveState();
            _playerDeathState = new PlayerDeathState();
            _stateContext.CurrentState = _playerAliveState;

            // 이벤트 구독
            inputHandler.OnMoveEnter += HandleMoveEnter;
            inputHandler.OnMoveStay += HandleMoveStay;
            inputHandler.OnMoveExit += HandleMoveExit;

            inputHandler.OnAttackEnter += HandleAttackEnter;
            inputHandler.OnAttackStay += HandleAttackStay;
            inputHandler.OnAttackExit += HandleAttackExit;

            var manager = GameManager.Instance;
            if (manager != null) manager.playerController = this;
            
            Health.OnDie += () => _stateContext.CurrentState = _playerDeathState;
        }
        
        private void OnDisable()
        {
            
            inputHandler.OnMoveEnter -= HandleMoveEnter;
            inputHandler.OnMoveStay -= HandleMoveStay;
            inputHandler.OnMoveExit -= HandleMoveExit;

            inputHandler.OnAttackEnter -= HandleAttackEnter;
            inputHandler.OnAttackStay -= HandleAttackStay;
            inputHandler.OnAttackExit -= HandleAttackExit;
        }

        [Inject] public IHealth Health { get; set; }

        public void KnockBack(float knockBackForce = 1f, float timeDelay = 0.6f)
        {
            _movement.KnockBack(knockBackForce, timeDelay);
            _playerAttack.enabled = false;
            StartCoroutine(Logic.WaitThenCallback(timeDelay, () => _playerAttack.enabled = true));
        }

        public void AddAbility(AbilityData abilityData,
            Enums.AbilityMethodType abilityMethodType = Enums.AbilityMethodType.Add)
        {
            foreach (var ability in GetComponentsInChildren<IAbility>()) ability.AddEffect(abilityData);
        }

        public IAudioSystem AudioSystem { get; set; }

        private void HandleMoveEnter()
        {
        }

        private void HandleMoveStay()
        {
            _movement.Move(inputHandler.movementInput);
        }

        private void HandleMoveExit()
        {
            _movement.Move(Vector2.zero);
        }

        private void HandleAttackEnter()
        {
            _playerAttack.Attack();
        }

        private void HandleAttackStay()
        {
        }

        private void HandleAttackExit()
        {
        }
    }
}