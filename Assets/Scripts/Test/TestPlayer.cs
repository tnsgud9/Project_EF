using System.Collections;
using Collections;
using UnityEngine;

namespace Test
{
    public class TestPlayer : BaseBehaviour
    {
        private void Start()
        {
            // 처음에 컴포넌트를 비활성화
            this.enabled = false;

            // 3초 뒤에 컴포넌트 활성화 (Coroutine 사용)
            StartCoroutine(EnableAfterDelayCoroutine());
        }

        // 3초 뒤에 enabled를 true로 설정하는 코루틴
        private IEnumerator EnableAfterDelayCoroutine()
        {
            // 3초 동안 대기
            yield return new WaitForSeconds(3f);

            // 3초 후에 컴포넌트 활성화
            this.enabled = true;
            Debug.Log("Component has been re-enabled after 3 seconds.");
        }

        // Update 메서드로 계속 실행될 작업
        private void Update()
        {
            // 업데이트 메서드에 실행할 코드 추가
        }
    }
}