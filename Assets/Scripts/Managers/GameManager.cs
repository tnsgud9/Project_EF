using System.Collections.Generic;
using Camera;
using Collections;
using Entities.Abilities;
using Entities.Player;

namespace Managers
{
    public class GameManager : Singleton<GameManager>, IManager
    {
        public PlayerController playerController;

        public List<AbilityData> abilities;

        // public List<AbilityData> playerAbilities;


        protected override void Awake()
        {
            base.Awake();
            // TODO: 추후에 카메라도 controller 의한 제어를 받아야한다.
            UnityEngine.Camera.main?.GetComponent<CameraFade>().FadeIn();
            // UiManager.Instance.CloseAllUIs();
        }

        public void AddAbility(AbilityData ability)
        {
            playerController.AddAbility(ability);
        }
    }
}