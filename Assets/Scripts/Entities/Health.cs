using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public interface IHealth
    {
        void TakeDamage(int damage);
        int CurrentHealth { get; }
        int MaxHealth { get; }
    }

    public class PlayerHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private int currentHealth;

        public int CurrentHealth => currentHealth;
        [SerializeField] public int MaxHealth { get; } = 100;

        private void Start()
        {
            currentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Max(0, currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Player Died");
            // 사망 처리 로직
        }
    }
}