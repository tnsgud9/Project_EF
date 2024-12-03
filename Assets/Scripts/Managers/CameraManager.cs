using Camera;
using Collections;

namespace Managers
{
    public class CameraManager : Singleton<CameraManager>, IManager
    {
        private UnityEngine.Camera _camera;

        private void Start()
        {
            _camera = UnityEngine.Camera.main;
        }

        public void CameraExplosionShake()
        {
            _camera.GetComponent<CameraShake>()?.TriggerExplosionShake();
        }
    }
}