using System;
using System.Collections;
using Collections;
using Commons;
using Entities.Abilities;
using UnityEngine;

namespace Entities.Player
{
    public interface IMovement
    {
        public void Move(Vector2 direction);
        public IEnumerator DelayMovement(float time, Action callback = null);
    }

    public class PlayerMovement : BaseBehaviour, IMovement, IAbility
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        public float moveSpeed = Const.DefaultPlayerSpeed; // 이동 속도
        [InjectChild] private Animator _animator;
        private bool _isKnockedBack;
        private Vector2 _movementInput; // 이동 입력값
        private float _moveSpeed; // 이동 속도
        [Inject] private Rigidbody2D _rigid; // Rigidbody2D 컴포넌트
        [InjectChild] private SpriteRenderer _spriteRenderer;

        protected override void OnEnable()
        {
            base.OnEnable();
            _moveSpeed = moveSpeed;
        }

        public void AddEffect(AbilityData abilityData)
        {
            moveSpeed += abilityData.moveSpeed;
            _moveSpeed = moveSpeed;
        }


        public void Move(Vector2 direction)
        {
            if (_isKnockedBack) return; // TODO: 안티패턴 개선 필요
            // Rigidbody2D를 사용해 물리적으로 이동시킨다.
            _rigid.velocity = direction.normalized * _moveSpeed; // 이동 방향에 속도 적용 , drag 영향을 받지 않음
            _spriteRenderer.sortingOrder = Mathf.FloorToInt(-transform.position.y * 10);
            _animator.SetFloat(Horizontal, direction.y);
            _animator.SetFloat(Vertical, direction.x);
        }

        public IEnumerator DelayMovement(float time, Action callback = null)
        {
            _moveSpeed = 0f;
            yield return new WaitForSeconds(time);
            _moveSpeed = moveSpeed;
            callback?.Invoke();
        }

        public void KnockBack(float knockBackForce = 1f, float timeDelay = 0.6f)
        {
            _animator.SetFloat(Horizontal, 0);
            _animator.SetFloat(Vertical, 0);
            _isKnockedBack = true;
            _rigid.AddForce(-_rigid.velocity.normalized * knockBackForce, ForceMode2D.Impulse);
            StartCoroutine(DelayMovement(timeDelay, () => { _isKnockedBack = false; }));
            // 반대 방향으로 튕겨나가게 힘을 적용
        }
    }
}