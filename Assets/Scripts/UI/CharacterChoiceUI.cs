using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace UI
{
    public class CharacterChoiceUI : BaseUI
    {
        [SerializeField] private List<GameObject> characterPrefabs;

        protected override void AssignUiManage()
        {
            UiManager.Instance.AssignUI(this);
        }
    }
}