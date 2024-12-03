using Collections;
using Entities.Player;
using UnityEngine;

namespace Entities.Weapons
{
    /// <summary>
    ///     Bomb은 플레이어의 기본 공격 폼이다.
    /// </summary>
    public abstract class Bomb : BaseBehaviour
    {
        [Header("Bomb Settings")] public float explosionDelay = 3f; // 폭발 대기시간
        public float explosionRadius = 2f; // 폭발 범위
        public int damage = 50; // 폭탄 데미지
        public LayerMask damageableLayer; // 데미지를 받을 레이어
        private PlayerAttack _playerAttack;

        [Inject] protected Animator Animator;

        protected new virtual void OnEnable()
        {
            base.OnEnable();
            BeforeExplode();
            Invoke(nameof(Explode), explosionDelay);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(Explode));
        }

        // 폭탄 초기화
        public virtual void Initialize(PlayerAttack playerAttack)
        {
            _playerAttack = playerAttack;
            explosionRadius = _playerAttack.bombRadius;
            damage = _playerAttack.damage;
        }

        // 폭발 처리 (자식 클래스에서 오버라이드 가능)
        protected abstract void BeforeExplode();
        protected abstract void Explode();

        protected void ExplodeComplete()
        {
            // 폭탄이 폭발 종료 후 플레이어의 OnBombExploded 메서드를 호출하여 폭탄을 풀로 반환
            _playerAttack.OnBombExploded(gameObject);
        }
    }
}