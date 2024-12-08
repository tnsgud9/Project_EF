using System;
using System.Collections.Generic;
using System.Linq;
using Collections;
using UI;

namespace Managers
{
    public class UiManager : Singleton<UiManager>
    {
        private readonly Dictionary<Type, List<BaseUI>> _activeUIs = new();

        public void AssignUI<T>(T ui) where T : BaseUI
        {
            var type = typeof(T);
            if (!_activeUIs.ContainsKey(type)) _activeUIs[type] = new List<BaseUI>();

            if (!_activeUIs[type].Contains(ui)) _activeUIs[type].Add(ui);
        }

        public T GetUI<T>() where T : BaseUI
        {
            var type = typeof(T);
            return _activeUIs.ContainsKey(type) && _activeUIs[type].Count > 0
                ? _activeUIs[type][0] as T
                : null;
        }

        public List<T> GetUIs<T>() where T : BaseUI
        {
            var type = typeof(T);
            return _activeUIs.ContainsKey(type)
                ? _activeUIs[type].Cast<T>().ToList()
                : new List<T>();
        }
        //
        // // 특정 UI 비활성화
        // public void DisableUI(string uiID)
        // {
        //     if (ActiveUIs.TryGetValue(uiID, out var ui))
        //         ui.SetActive(false);
        //     else
        //         Debug.LogWarning($"UI ID '{uiID}' not found.");
        // }
        //
        // // 특정 UI 활성화
        // public void EnableUI(string uiID)
        // {
        //     if (ActiveUIs.TryGetValue(uiID, out var ui))
        //         ui.SetActive(true);
        //     else
        //         Debug.LogWarning($"UI ID '{uiID}' not found.");
        // }
        //
        // // 특정 UI 삭제
        // public void CloseUI(string uiID)
        // {
        //     if (ActiveUIs.TryGetValue(uiID, out var ui))
        //     {
        //         Destroy(ui);
        //         ActiveUIs.Remove(uiID);
        //     }
        //     else
        //     {
        //         Debug.LogWarning($"UI ID '{uiID}' not found.");
        //     }
        // }
        //
        // // 모든 UI 비활성화
        // public void DisableAllUIs()
        // {
        //     foreach (var ui in ActiveUIs.Values) ui.SetActive(false);
        // }
        //
        // // 모든 UI 삭제
        // public void CloseAllUIs()
        // {
        //     foreach (var ui in ActiveUIs.Values) Destroy(ui);
        //     ActiveUIs.Clear();
        // }
    }
}