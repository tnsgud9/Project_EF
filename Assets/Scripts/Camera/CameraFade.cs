using Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Camera
{
    public class CameraFade : BaseBehaviour
    {
        [InjectChild] public Image fadeImage; // 전체 화면을 덮을 UI Image

        // FadeIn 함수: 전체 화면을 덮는 투명 이미지의 alpha 값을 1로 변경
        public void FadeIn(float startDelay = 0f, float fadeDuration = 1f)
        {
            fadeImage.enabled = true; // 이미지 활성화
            StartCoroutine(Logic.WaitThenCallback(startDelay, () =>
            {
                fadeImage.color =
                    new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1); // 초기 alpha 값을 1로 설정
                fadeImage.DOFade(0f, fadeDuration).OnKill(() =>
                {
                    fadeImage.enabled = false; // FadeOut이 끝난 후 비활성화
                });
            }));
        }

        // FadeOut 함수: 전체 화면을 덮는 투명 이미지의 alpha 값을 0으로 변경
        public void FadeOut(float startDelay = 0f, float fadeDuration = 1f)
        {
            fadeImage.enabled = true; // 이미지 활성화
            StartCoroutine(Logic.WaitThenCallback(startDelay, () =>
            {
                fadeImage.color =
                    new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0); // 초기 alpha 값을 0으로 설정
                fadeImage.DOFade(1f, fadeDuration); // alpha 값을 1로 변경
            }));
        }
    }
}