using UnityEngine;

namespace Entities
{
    public class Movement : MonoBehaviour
    {
        public float moveSpeed = 5f; // 이동 속도
        private Rigidbody2D _rb; // Rigidbody2D 컴포넌트
        private Vector2 _movementInput; // 이동 입력값
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        }

        private void Update()
        {
            // 플레이어의 이동 입력을 받는다.
            float moveX = Input.GetAxisRaw("Horizontal"); // A, D 또는 좌우 화살표
            float moveY = Input.GetAxisRaw("Vertical"); // W, S 또는 상하 화살표

            _movementInput = new Vector2(moveX, moveY).normalized; // 이동 방향을 정규화 (대각선 이동 속도 보정)
        }

        private void FixedUpdate()
        {
            // Rigidbody2D를 사용해 물리적으로 이동시킨다.
            _rb.velocity = _movementInput * moveSpeed; // 이동 방향에 속도 적용
        }
    }
}
