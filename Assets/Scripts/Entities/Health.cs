using UnityEngine;

namespace Entities
{
    public interface IHealth
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }
        void TakeDamage(int damage);
    }

    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private int currentHealth;

        private void Start()
        {
            currentHealth = MaxHealth;
        }

        public int CurrentHealth => currentHealth;
        public int MaxHealth { get; } = 100;

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Max(0, currentHealth);

            if (currentHealth <= 0) Die();
        }

        private void Die()
        {
            Debug.Log("Died");
            // 사망 처리 로직
        }
    }
}