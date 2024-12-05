using UnityEngine;

namespace Commons
{
    public class Enums : MonoBehaviour
    {
        public enum AbilityMethodType
        {
            Add,
            Set
        }

        public enum Event
        {
            GameOver,
            GameStart,
            Restart,
            MainMenu
        }
    }
}