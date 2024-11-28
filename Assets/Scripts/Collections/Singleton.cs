using UnityEngine;

namespace Collections
{
    public class DestoryableSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        var go = new GameObject();
                        var component = go.AddComponent<T>();

                        go.name = typeof(T).ToString();

                        _instance = component;
                    }
                }

                return _instance;
            }
        }
    }

    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        var go = new GameObject();
                        var component = go.AddComponent<T>();
                        go.name = typeof(T).ToString();
                        DontDestroyOnLoad(go);
                        _instance = component;
                    }
                }

                return _instance;
            }
        }
    }
}