using Collections;
using Commons;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerAliveState : IState<PlayerController>
    {
        public void StateStart(PlayerController controller)
        {
            // TODO: 기본적인 플레이어 세팅 설정
            controller.inputHandler.enabled = true;
        }

        public void StateUpdate(PlayerController controller)
        {
        }

        public void StateEnd(PlayerController controller)
        {
        }
    }

    public class PlayerDeathState : IState<PlayerController>
    {
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private readonly AudioPreset _deathAudio;

        public PlayerDeathState(AudioPreset deathAudio)
        {
            _deathAudio = deathAudio;
        }

        public void StateStart(PlayerController controller)
        {
            // 제어권 통제
            controller.inputHandler.enabled = false;
            // 죽음 애니메이션
            controller.animator.SetTrigger(Death);
            // 죽음 애니메이션
            controller.animator.SetFloat(Horizontal, 0f);
            controller.animator.SetFloat(Vertical, 0f);
            controller.AudioSystem.Play(_deathAudio);
            // BroadCast
            EventBus<Enums.Event>.Publish(Enums.Event.GameOver);
        }

        public void StateUpdate(PlayerController controller)
        {
        }

        public void StateEnd(PlayerController controller)
        {
        }
    }
}