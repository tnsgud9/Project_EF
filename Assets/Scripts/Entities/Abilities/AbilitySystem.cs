using System.Collections.Generic;

namespace Entities.Abilities
{
    public class AbilitySystem
    {
        public List<AbilityData> AbilityList;

        public AbilitySystem(ref List<AbilityData> abilityList)
        {
            AbilityList = abilityList;
        }
    }
}