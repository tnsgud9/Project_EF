using Collections;
using Commons;
using Entities.Abilities;

namespace Entities
{
    public interface IController
    {
        public IAudioSystem AudioSystem { get; set; }

        public void KnockBack(float knockBackForce = 1f, float timeDelay = 0.6f);

        void AddAbility(AbilityData abilityData,
            Enums.AbilityMethodType abilityMethodType = Enums.AbilityMethodType.Add);
    }
}