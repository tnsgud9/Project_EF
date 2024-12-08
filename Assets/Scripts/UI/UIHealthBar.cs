using System.Collections.Generic;
using System.Linq;
using Collections;
using Entities;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHealthBar : BaseUI
    {
        [Inject] public Slider slider;
        public LayerMask availableLayer;

        private readonly List<IHealth> _trackingHealths = new();
        private int _allMaxHealth;

        protected override void OnEnable()
        {
            base.OnEnable();
            RefreshHealthBar();
        }

        protected override void AssignUiManage()
        {
            UiManager.Instance.AssignUI(this);
        }

        public void AddHealthTracking(IHealth health)
        {
            _trackingHealths.Add(health);
            _allMaxHealth = GetAllMaxHealth();
            RefreshHealthBar();
        }

        public void AddHealthTracking(GameObject healthObject)
        {
            var health = healthObject.GetComponent<IHealth>();
            if (health is null)
            {
                Debug.LogError("Cannot add health tracking: health object is null");
                return;
            }

            if (healthObject.layer != availableLayer)
            {
                Debug.LogWarning("Cannot add health tracking: layer is not " + availableLayer);
                return;
            }

            AddHealthTracking(health);
        }

        public void ResetHealthBar()
        {
            _trackingHealths.Clear();
            _allMaxHealth = 0;
        }

        public void DestroyHealthTracking(IHealth health)
        {
            _allMaxHealth = GetAllMaxHealth();
            _trackingHealths.Remove(health);
        }

        public void RefreshHealthBar()
        {
            if (_trackingHealths.Count > 0)
            {
                var currentHealthSum = _trackingHealths.Sum(h => h.CurrentHealth);
                slider.value = currentHealthSum / (float)_allMaxHealth;
            }
        }

        private int GetAllMaxHealth()
        {
            return _trackingHealths.Sum(it => it.MaxHealth);
        }
    }
}