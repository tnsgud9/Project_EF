using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Collections
{
    public class BaseBehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            InjectGetComponent();
        }

        protected void InjectGetComponent()
        {
            var fields = GetFieldsWithAttribute(typeof(InjectComponent));
            foreach (var field in fields)
            {
                var type = field.FieldType;
                var component = GetComponent(type);
                if (component == null)
                {
                    Debug.LogWarning("GetComponent typeof(" + type.Name + ") in game object '" + gameObject.name + "' is null");
                    component = this.gameObject.AddComponent(type);
                    // continue;
                }
                field.SetValue(this, component);
            }
        }

        protected IEnumerable<FieldInfo> GetFieldsWithAttribute(Type attributeType)
        {
            var fields = GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => field.GetCustomAttributes(attributeType, true).FirstOrDefault() != null);

            return fields;
        }
    }
}