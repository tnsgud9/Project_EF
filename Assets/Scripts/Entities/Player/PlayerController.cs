using System;
using Collections;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : BaseBehaviour
    {
        [InjectComponent] private IPlayerMovement _playerMovement;
        [InjectComponent] private IPlayerAttack _playerAttack;
        [InjectComponent] private PlayerInputHandler _inputHandler;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            // 이벤트 구독
            _inputHandler.OnMoveEnter += HandleMoveEnter;
            _inputHandler.OnMoveStay += HandleMoveStay;
            _inputHandler.OnMoveExit += HandleMoveExit;

            _inputHandler.OnAttackEnter += HandleAttackEnter;
            _inputHandler.OnAttackStay += HandleAttackStay;
            _inputHandler.OnAttackExit += HandleAttackExit;
            
        }
        
        private void HandleMoveEnter()
        {
            Debug.Log("Move Started");
        }

        private void HandleMoveStay()
        {
            Debug.Log("Moving...");
            _playerMovement.Move(_inputHandler.movementInput); // 필요시 구현
        }

        private void HandleMoveExit()
        {
            Debug.Log("Move Stopped");
            _playerMovement.Move(Vector2.zero);
        }

        private void HandleAttackEnter()
        {
            Debug.Log("Attack Started");
            _playerAttack.Attack();
        }

        private void HandleAttackStay()
        {
            Debug.Log("Charging Attack...");
        }

        private void HandleAttackExit()
        {
            Debug.Log("Attack Released");
        }
    }
}
