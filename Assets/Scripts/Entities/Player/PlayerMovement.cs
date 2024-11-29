using System;
using System.Collections;
using Collections;
using Commons;
using UnityEngine;

namespace Entities.Player
{
    public interface IPlayerMovement
    {
        public void Move(Vector2 direction);
        public IEnumerator DelayMovement(float time, Action callback = null);
    }

    public class PlayerMovement : BaseBehaviour , IPlayerMovement
    {
        public float moveSpeed = Const.DefaultPlayerSpeed; // 이동 속도
        [InjectComponent] private Rigidbody2D _rigid; // Rigidbody2D 컴포넌트
        private Vector2 _movementInput; // 이동 입력값
        [InjectChildrenComponent] private SpriteRenderer _spriteRenderer;
        
        public void Move(Vector2 direction)
        {
            // Rigidbody2D를 사용해 물리적으로 이동시킨다.
            _rigid.velocity = direction * moveSpeed; // 이동 방향에 속도 적용
            _spriteRenderer.sortingOrder = (int)transform.position.y;
        }

        public IEnumerator DelayMovement(float time, Action callback = null)
        {
            float previousMoveSpeed = moveSpeed;
            moveSpeed = 0f;
            yield return new WaitForSeconds(time);
            moveSpeed = previousMoveSpeed;
            callback?.Invoke();
        }
    }
}
