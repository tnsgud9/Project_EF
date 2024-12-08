using Collections;
using Commons;

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
        public void StateStart(PlayerController controller)
        {
            // 제어권 통제
            controller.inputHandler.enabled = false;
            // 죽음 애니메이션

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