using Collections;

namespace UI
{
    public abstract class BaseUI : BaseBehaviour
    {
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        public virtual void OnDisable()
        {
        }

        public virtual void Initialize()
        {
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}