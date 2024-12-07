using System.Collections.Generic;
using Camera;
using Collections;
using Entities.Abilities;
using Entities.Player;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : Singleton<GameManager>, IManager
    {
        public PlayerController playerController;

        public List<IAbility> PlayerAbilities;

        public int totalEnemyMaxHealth = 0;
        
        private void Awake()
        {
            // TODO: 추후에 카메라도 controller 의한 제어를 받아야한다.
            UnityEngine.Camera.main?.GetComponent<CameraFade>().FadeIn();
            UiManager.Instance.CloseAllUIs();
        }
    }
}