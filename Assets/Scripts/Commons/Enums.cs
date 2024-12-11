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

        public enum EntityEvent
        {
            Damaged
        }

        public enum Event
        {
            GameOver,
            GameStart,
            Restart,
            MainMenu,
            StageReady,
            StageStart,
            StageClear
        }
    }
}