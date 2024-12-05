using System;
using System.Collections;
using Collections;
using Commons;
using Entities.Abilities;
using UnityEngine;

namespace Entities.Player
{
    public interface IPlayerMovement
    {
        public void Move(Vector2 direction);
        public IEnumerator DelayMovement(float time, Action callback = null);
    }

    public class PlayerMovement : BaseBehaviour, IPlayerMovement, IAbility
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        public float moveSpeed = Const.DefaultPlayerSpeed; // 이동 속도
        [InjectChild] private Animator _animator;
        private Vector2 _movementInput; // 이동 입력값
        [Inject] private Rigidbody2D _rigid; // Rigidbody2D 컴포넌트
        [InjectChild] private SpriteRenderer _spriteRenderer;

        public void AddEffect(AbilityData abilityData)
        {
            moveSpeed += abilityData.moveSpeed;
        }


        public void Move(Vector2 direction)
        {
            // Rigidbody2D를 사용해 물리적으로 이동시킨다.
            _rigid.velocity = direction * moveSpeed; // 이동 방향에 속도 적용
            _spriteRenderer.sortingOrder = Mathf.FloorToInt(-transform.position.y * 10);
            _animator.SetFloat(Horizontal, direction.y);
            _animator.SetFloat(Vertical, direction.x);
        }

        public IEnumerator DelayMovement(float time, Action callback = null)
        {
            var previousMoveSpeed = moveSpeed;
            moveSpeed = 0f;
            yield return new WaitForSeconds(time);
            moveSpeed = previousMoveSpeed;
            callback?.Invoke();
        }
    }
}