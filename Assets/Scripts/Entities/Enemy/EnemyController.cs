using Collections;
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

        public IAudioSystem AudioSystem { get; set; }
        public IHealth Health { get; set; }

        public void AddAbility(AbilityData abilityData)
        {
        }
    }
}