using Collections;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : BaseBehaviour
    {
        [Inject] public PlayerInputHandler inputHandler;
        [Inject] private IHealth _health;

        private PlayerAliveState _playerAliveState;
        [Inject] private IPlayerAttack _playerAttack;
        private PlayerDeathState _playerDeathState;
        [Inject] private IPlayerMovement _playerMovement;

        private StateContext<PlayerController> _stateContext;

        private void Start()
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _stateContext = new StateContext<PlayerController>(this);

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

            _health.OnDie += () => { _stateContext.CurrentState = _playerDeathState; };
        }

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