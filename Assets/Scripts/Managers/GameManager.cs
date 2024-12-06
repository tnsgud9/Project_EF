using Camera;
using Collections;

namespace Managers
{
    public class GameManager : Singleton<GameManager>, IManager
    {
        private void Awake()
        {
            // TODO: 추후에 카메라도 controller 의한 제어를 받아야한다.
            UnityEngine.Camera.main?.GetComponent<CameraFade>().FadeIn();
            UiManager.Instance.CloseAllUIs();
        }
    }
}