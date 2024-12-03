using System.Collections;
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

        protected override void OnEnable()
        {
            base.OnEnable();
            StartCoroutine(ExplodeExecutor());
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(Explode));
        }

        // 폭탄 초기화
        public void Initialize(PlayerAttack playerAttack)
        {
            _playerAttack = playerAttack;
            explosionRadius = _playerAttack.bombRadius;
            damage = _playerAttack.damage;
        }

        // 폭발 처리 (자식 클래스에서 오버라이드 가능)
        protected abstract void BeforeExplode();
        protected abstract void Explode();
        protected abstract void AfterExplode(); // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator ExplodeExecutor()
        {
            BeforeExplode();
            yield return new WaitForSeconds(explosionDelay);
            _playerAttack.BombExplotion();
            Explode();
            yield return new WaitForSeconds(3f);
            AfterExplode();
            ExplodeComplete();
        }

        private void ExplodeComplete()
        {
            // 폭탄이 폭발 종료 후 플레이어의 BombExploded 메서드를 호출하여 폭탄을 풀로 반환
            _playerAttack.BombExploded(gameObject);
        }
    }
}