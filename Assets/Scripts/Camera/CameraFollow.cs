using UnityEngine;

namespace Camera
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target; // 카메라가 따라갈 대상 (캐릭터)
        public SpriteRenderer spriteRenderer; // 스프라이트 렌더러
        public float smoothSpeed = 0.125f; // 부드럽게 이동할 속도

        private UnityEngine.Camera cam;

        void Start()
        {
            cam = UnityEngine.Camera.main; // 카메라를 가져옵니다
        }

        void Update()
        {
            // 카메라의 뷰포트 크기 계산 (화면 너비와 높이)
            float screenWidth = 2f * cam.orthographicSize * cam.aspect;
            float screenHeight = 2f * cam.orthographicSize;

            // 스프라이트의 실제 크기 계산
            float spriteWidth = spriteRenderer.bounds.size.x;
            float spriteHeight = spriteRenderer.bounds.size.y;

            // 카메라의 최소/최대 X, Y 좌표 계산
            float minX = spriteRenderer.transform.position.x - spriteWidth / 2 + screenWidth / 2;
            float maxX = spriteRenderer.transform.position.x + spriteWidth / 2 - screenWidth / 2;
            float minY = spriteRenderer.transform.position.y - spriteHeight / 2 + screenHeight / 2;
            float maxY = spriteRenderer.transform.position.y + spriteHeight / 2 - screenHeight / 2;

            // 캐릭터 위치에 따라 카메라의 X, Y 위치 제한
            float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(target.position.y, minY, maxY);

            // 타겟의 위치를 부드럽게 따라가도록 카메라 위치 업데이트
            Vector3 desiredPosition = new Vector3(clampedX, clampedY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}