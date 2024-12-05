using Collections;
using Commons;
using Entities.Abilities;

namespace Entities
{
    public interface IController
    {
        public IAudioSystem AudioSystem { get; set; }
        public IHealth Health { get; set; }

        void AddAbility(AbilityData abilityData,
            Enums.AbilityMethodType abilityMethodType = Enums.AbilityMethodType.Add);
    }
}