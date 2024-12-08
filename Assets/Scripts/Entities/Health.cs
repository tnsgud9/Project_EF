using System;
using Entities.Abilities;
using UnityEngine;

namespace Entities
{
    public interface IHealth
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }
        event Action OnDie;
        event Action<int, int> OnHealthChanged;
        void TakeDamage(int damage = 1);
    }

    public class Health : MonoBehaviour, IHealth, IAbility
    {
        [SerializeField] private int currentHealth;

        private void Start()
        {
            currentHealth = MaxHealth;
        }

        public void AddEffect(AbilityData abilityData)
        {
            MaxHealth += abilityData.health;
        }

        public int CurrentHealth => currentHealth;
        [field: SerializeField] public int MaxHealth { get; set; } = 100;
        public event Action OnDie;
        public event Action<int, int> OnHealthChanged;

        public void TakeDamage(int damage = 1)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Max(0, currentHealth);
            OnHealthChanged?.Invoke(currentHealth, damage);
            if (currentHealth <= 0)
                // Die 이벤트를 호출
                OnDie?.Invoke();
        }
    }
}