using Collections;

namespace UI
{
    public abstract class BaseUI : BaseBehaviour
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            // 자식 클래스에서 AssignUI를 호출하도록 유도
            AssignUiManage();
        }

        // 자식 클래스가 반드시 이 메서드를 호출하게끔 유도
        protected abstract void AssignUiManage();
    }
}