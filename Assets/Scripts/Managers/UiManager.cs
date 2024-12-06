using System.Collections.Generic;
using Collections;
using UnityEngine;

namespace Managers
{
    public class UiManager : Singleton<UiManager>
    {
        // Canvas의 부모 Transform
        [SerializeField] private Transform uiCanvas;

        // 생성된 UI 오브젝트를 관리 (고유 ID 기반)
        private readonly Dictionary<string, GameObject> _activeUIs = new();

        // UI 프리팹 로드 및 관리
        private readonly Dictionary<string, GameObject> _uiPrefabs = new();

        private void Awake()
        {
            // 프리팹 초기화
            LoadUIPrefabs();
        }

        private void LoadUIPrefabs()
        {
            var prefabs = Resources.LoadAll<GameObject>("UI");
            foreach (var prefab in prefabs) _uiPrefabs[prefab.name] = prefab;
        }

        // 특정 UI 표시
        public GameObject ShowUI(string uiName, string uiID)
        {
            if (!_uiPrefabs.ContainsKey(uiName))
            {
                Debug.LogError($"UI Prefab '{uiName}' not found.");
                return null;
            }

            if (_activeUIs.ContainsKey(uiID))
            {
                Debug.LogWarning($"UI ID '{uiID}' is already active.");
                return _activeUIs[uiID];
            }

            // UI 생성
            var newUI = Instantiate(_uiPrefabs[uiName], uiCanvas);
            _activeUIs[uiID] = newUI;

            return newUI;
        }

        // 특정 UI 비활성화
        public void DisableUI(string uiID)
        {
            if (_activeUIs.TryGetValue(uiID, out var ui))
                ui.SetActive(false);
            else
                Debug.LogWarning($"UI ID '{uiID}' not found.");
        }

        // 특정 UI 활성화
        public void EnableUI(string uiID)
        {
            if (_activeUIs.TryGetValue(uiID, out var ui))
                ui.SetActive(true);
            else
                Debug.LogWarning($"UI ID '{uiID}' not found.");
        }

        // 특정 UI 삭제
        public void CloseUI(string uiID)
        {
            if (_activeUIs.TryGetValue(uiID, out var ui))
            {
                Destroy(ui);
                _activeUIs.Remove(uiID);
            }
            else
            {
                Debug.LogWarning($"UI ID '{uiID}' not found.");
            }
        }

        // 모든 UI 비활성화
        public void DisableAllUIs()
        {
            foreach (var ui in _activeUIs.Values) ui.SetActive(false);
        }

        // 모든 UI 삭제
        public void CloseAllUIs()
        {
            foreach (var ui in _activeUIs.Values) Destroy(ui);
            _activeUIs.Clear();
        }
    }
}