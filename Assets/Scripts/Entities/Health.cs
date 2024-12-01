using Collections;
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

    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private int currentHealth;

        public int CurrentHealth => currentHealth;
        public int MaxHealth { get; } = 100;

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
            Debug.Log("Died");
            // 사망 처리 로직
        }
    }
}