using DG.Tweening;
using UnityEngine;

// DOTween 네임스페이스

namespace Camera
{
    public class CameraShake : MonoBehaviour
    {
        private Vector3 _originalPosition; // 카메라의 원래 위치
        private Tween _shakeTween; // DOTween의 Tween 객체

        private void Start()
        {
            // 원래 위치 저장
            _originalPosition = transform.localPosition;
        }

        // 외부에서 호출하여 떨림 시작
        public void TriggerExplosionShake(float duration = 0.5f, float strength = 0.75f, int vibrato = 20,
            float randomness = 90f)
        {
            // 기존의 떨림이 진행 중이라면 멈춤
            _shakeTween?.Kill();

            // DOShakePosition으로 폭발적인 떨림 효과 생성
            _shakeTween = transform.DOShakePosition(
                duration, // 지속 시간
                strength, // 강도
                vibrato, // 진동 횟수 (진동이 많을수록 폭발적)
                randomness // 시간에 따라 점점 약해짐
            ).SetEase(Ease.OutQuad); // 폭발 후 빠르게 잦아드는 느낌
        }
    }
}