using Camera;
using Collections;
using UnityEngine;

namespace Managers
{
    public class CameraManager : Singleton<CameraManager>, IManager
    {
        private UnityEngine.Camera _camera;

        private void Start()
        {
            _camera = UnityEngine.Camera.main;
            _camera.backgroundColor = Color.black;
        }

        public void CameraExplosionShake()
        {
            _camera.GetComponent<CameraShake>()?.TriggerExplosionShake();
        }
    }
}