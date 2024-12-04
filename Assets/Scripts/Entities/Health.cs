using System;
using UnityEngine;

namespace Entities
{
    public interface IHealth
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }
        event Action OnDie;
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
        public int MaxHealth { get; set; } = 100;

        // Die 이벤트 구현
        public event Action OnDie;

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Max(0, currentHealth);

            if (currentHealth <= 0)
                // Die 이벤트를 호출
                OnDie?.Invoke();
        }
    }
}