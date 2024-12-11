using System;
using System.Collections.Generic;
using Collections;
using UI;

namespace Managers
{
    public class UiManager : Singleton<UiManager>
    {
        private readonly Dictionary<Type, BaseUI> _assignUIs = new();

        public void AssignUI<T>(T ui) where T : BaseUI
        {
            var type = typeof(T);
            _assignUIs[type] = ui;
        }

        public T GetUI<T>() where T : BaseUI
        {
            var type = typeof(T);
            return _assignUIs.GetValueOrDefault(type) as T;
        }

        // public List<T> GetUIs<T>() where T : BaseUI
        // {
        //     var type = typeof(T);
        //     return _assignUIs.ContainsKey(type)
        //         ? _assignUIs[type].Cast<T>().ToList()
        //         : new List<T>();
        // }
        //
        // 특정 UI 비활성화
        public void DisableUI(Type type)
        {
            _assignUIs[type]?.gameObject.SetActive(false);
        }

        // 특정 UI 활성화
        public void EnableUI(Type type)
        {
            _assignUIs[type]?.gameObject.SetActive(true);
        }

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