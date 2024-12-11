using Collections;
using Commons;
using Managers;
using UI;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerReadyState : IState<PlayerController>
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");

        public void StateStart(PlayerController controller)
        {
            controller.inputHandler.enabled = false;
            controller.movement.enabled = false;
            controller.playerAttack.enabled = false;
            controller.health.RegenHealth();
            controller.animator.SetFloat(Horizontal, 0f);
            controller.animator.SetFloat(Vertical, 0f);
            controller.GetComponent<Collider2D>().enabled = true;
            controller.gameObject.transform.position = Vector3.zero;

            UiManager.Instance.GetUI<UIPlayerInfo>()?.SetPlayerHealth(controller.health.CurrentHealth);
            UiManager.Instance.GetUI<UIPlayerInfo>()?.SetPlayerBomb(controller.playerAttack.maxBombs);
        }

        public void StateUpdate(PlayerController controller)
        {
        }

        public void StateEnd(PlayerController controller)
        {
        }
    }

    public class PlayerAliveState : IState<PlayerController>
    {
        public void StateStart(PlayerController controller)
        {
            controller.inputHandler.enabled = true;
            controller.movement.enabled = true;
            controller.playerAttack.enabled = true;
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
            controller.GetComponent<Collider2D>().enabled = false;
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