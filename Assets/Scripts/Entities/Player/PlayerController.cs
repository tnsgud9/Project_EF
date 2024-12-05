using Collections;
using Entities.Abilities;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : BaseBehaviour, IController
    {
        [Inject] public PlayerInputHandler inputHandler;
        [InjectAdd] private AudioSource _audioSource;

        private PlayerAliveState _playerAliveState;
        [Inject] private IPlayerAttack _playerAttack;
        private PlayerDeathState _playerDeathState;
        [Inject] private IPlayerMovement _playerMovement;
        private StateContext<PlayerController> _stateContext;

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

            Health.OnDie += () => { _stateContext.CurrentState = _playerDeathState; };
        }

        [Inject] public IHealth Health { get; set; }

        public void AddAbility(AbilityData abilityData)
        {
            foreach (var ability in GetComponentsInChildren<IAbility>()) ability.ApplyEffect(abilityData);
        }

        public IAudioSystem AudioSystem { get; set; }

        private void HandleMoveEnter()
        {
        }

        private void HandleMoveStay()
        {
            _playerMovement.Move(inputHandler.movementInput);
        }

        private void HandleMoveExit()
        {
            _playerMovement.Move(Vector2.zero);
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