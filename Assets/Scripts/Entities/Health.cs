using System;
using Collections;
using Commons;
using Entities.Abilities;
using UnityEngine;

namespace Entities
{
    public class Health : MonoBehaviour, IAbility
    {
        [SerializeField] private int currentHealth;
        [field: SerializeField] public int MaxHealth { get; set; } = 100;

        public int CurrentHealth => currentHealth;

        private void Start()
        {
            EventBus<Enums.Event>.Subscribe(Enums.Event.StageReady, () => { currentHealth = MaxHealth; });
            currentHealth = MaxHealth;
        }

        public void AddEffect(AbilityData abilityData)
        {
            MaxHealth += abilityData.health;
        }

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

        public void RegenHealth()
        {
            currentHealth = MaxHealth;
            OnHealthChanged?.Invoke(currentHealth, 0);
        }
    }
}