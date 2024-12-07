using UnityEngine;

namespace Entities.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public delegate void InputEventHandler();

        public Vector2 movementInput;
        private Vector2 _lastMovementInput = Vector2.zero; // 이전 프레임의 입력 상태

        private void Update()
        {
            HandleMovementInput();
            HandleAttackInput();
        }

        public event InputEventHandler OnMoveEnter;
        public event InputEventHandler OnMoveStay;
        public event InputEventHandler OnMoveExit;

        public event InputEventHandler OnAttackEnter;
        public event InputEventHandler OnAttackStay;
        public event InputEventHandler OnAttackExit;

        private void HandleMovementInput()
        {
            // 방향 입력 처리
            var horizontal = Input.GetAxisRaw("Horizontal"); // A(-1) / D(+1) / None(0)
            var vertical = Input.GetAxisRaw("Vertical"); // W(+1) / S(-1) / None(0)

            movementInput = new Vector2(horizontal, vertical);

            // Enter: 처음 움직임 시작
            if (movementInput != Vector2.zero && _lastMovementInput == Vector2.zero) OnMoveEnter?.Invoke();

            // Stay: 움직이는 동안 지속
            if (movementInput != Vector2.zero) OnMoveStay?.Invoke();

            // Exit: 움직임 멈춤
            if (movementInput == Vector2.zero && _lastMovementInput != Vector2.zero) OnMoveExit?.Invoke();

            // 이전 상태 업데이트
            _lastMovementInput = movementInput;
        }

        private void HandleAttackInput()
        {
            if (Input.GetKeyDown(KeyCode.Space)) OnAttackEnter?.Invoke();

            if (Input.GetKey(KeyCode.Space)) OnAttackStay?.Invoke();

            if (Input.GetKeyUp(KeyCode.Space)) OnAttackExit?.Invoke();
        }
    }
}