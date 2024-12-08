using Collections;
using Managers;

namespace UI
{
    public abstract class BaseUI : BaseBehaviour
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            UiManager.Instance.AddUI(gameObject.name, gameObject);
        }
    }
}