using System;
using Collections;
using Commons;
using Entities.Abilities;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyController : BaseBehaviour, IController
    {
        [InjectAdd] private AudioSource _audioSource;
        [Inject] private EnemyAttack _enemyAttack;
        private StateContext<EnemyController> _stateContext;

        protected override void OnEnable()
        {
            base.OnEnable();
            AudioSystem = new AudioSystem(_audioSource);
            Health.OnDie += () => { Debug.Log($"Enemy Die : {gameObject.name}"); };
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.GetComponent<IController>()?.KnockBack(5f);
        }

        public IAudioSystem AudioSystem { get; set; }
        [Inject] public IHealth Health { get; set; }

        public void AddAbility(AbilityData abilityData,
            Enums.AbilityMethodType abilityMethodType = Enums.AbilityMethodType.Add)
        {
            throw new NotImplementedException();
        }

        public void KnockBack(float knockBackForce = 1, float timeDelay = 0.6f)
        {
        }
    }
}