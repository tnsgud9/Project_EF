using Managers;

namespace UI
{
    public class UIAbilityChoice : BaseUI
    {
        protected override void AssignUiManage()
        {
            UiManager.Instance.AssignUI(this);
        }
    }
}