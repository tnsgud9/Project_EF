using System.Collections.Generic;
using Collections;
using UnityEngine;

namespace Managers
{
    public class UiManager : Singleton<UiManager>
    {
        // 생성된 UI 오브젝트를 관리 (고유 ID 기반)
        public readonly Dictionary<string, GameObject> ActiveUIs = new();

        public void AddUI(string uiName, GameObject uiObject)
        {
            ActiveUIs.Add(uiName, uiObject);
        }

        // 특정 UI 비활성화
        public void DisableUI(string uiID)
        {
            if (ActiveUIs.TryGetValue(uiID, out var ui))
                ui.SetActive(false);
            else
                Debug.LogWarning($"UI ID '{uiID}' not found.");
        }

        // 특정 UI 활성화
        public void EnableUI(string uiID)
        {
            if (ActiveUIs.TryGetValue(uiID, out var ui))
                ui.SetActive(true);
            else
                Debug.LogWarning($"UI ID '{uiID}' not found.");
        }

        // 특정 UI 삭제
        public void CloseUI(string uiID)
        {
            if (ActiveUIs.TryGetValue(uiID, out var ui))
            {
                Destroy(ui);
                ActiveUIs.Remove(uiID);
            }
            else
            {
                Debug.LogWarning($"UI ID '{uiID}' not found.");
            }
        }

        // 모든 UI 비활성화
        public void DisableAllUIs()
        {
            foreach (var ui in ActiveUIs.Values) ui.SetActive(false);
        }

        // 모든 UI 삭제
        public void CloseAllUIs()
        {
            foreach (var ui in ActiveUIs.Values) Destroy(ui);
            ActiveUIs.Clear();
        }
    }
}