using Entities.Player;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace Entities.Weapons
{
    /// <summary>
    /// Bomb은 플레이어의 기본 공격 폼이다.
    /// </summary>
    public abstract class Bomb : MonoBehaviour
    {
        [Header("Bomb Settings")]
        public float explosionDelay = 3f; // 폭발 대기시간
        public float explosionRadius = 2f; // 폭발 범위
        public int damage = 50; // 폭탄 데미지
        public LayerMask damageableLayer; // 데미지를 받을 레이어

        protected PlayerAttack PlayerAttack;

        // 폭탄 초기화
        public virtual void Initialize(PlayerAttack playerAttack)
        {
            this.PlayerAttack = playerAttack;
        }

        private void OnEnable()
        {
            Invoke(nameof(Explode), explosionDelay);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(Explode));
        }

        // 폭발 처리 (자식 클래스에서 오버라이드 가능)
        protected abstract void Explode();

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
        
        protected void HandleExplosionComplete()
        {
            // 폭탄이 폭발 후 플레이어의 OnBombExploded 메서드를 호출하여 폭탄을 풀로 반환
            PlayerAttack.OnBombExploded(gameObject);

            // 이곳에서 추가적인 폭발 후 효과 (예: 애니메이션 종료 등) 추가 가능

            // 폭탄 비활성화하여 오브젝트 풀로 반환
            gameObject.SetActive(false); // 오브젝트를 비활성화하여 풀로 반환
        }
    }
}