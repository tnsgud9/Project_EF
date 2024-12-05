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
            InjectGetChildrenComponent();
            InjectGetAddComponent();
        }

        private void InjectGetComponent()
        {
            var fields = GetFieldsWithAttribute(typeof(Inject));
            foreach (var field in fields)
            {
                var type = field.FieldType;
                var component = GetComponent(type);
                if (component == null)
                {
                    Debug.LogWarning("GetComponent typeof(" + type.Name + ") in game object '" + gameObject.name +
                                     "' is null");
                    // component = gameObject.AddComponent(type);
                    continue;
                }

                field.SetValue(this, component);
            }

            // 프로퍼티 처리
            var properties = GetPropertiesWithAttribute(typeof(Inject));
            foreach (var property in properties)
            {
                var type = property.PropertyType;
                var component = GetComponent(type);
                if (component == null)
                {
                    Debug.LogWarning($"GetComponent typeof({type.Name}) in game object '{gameObject.name}' is null");
                    continue;
                }

                property.SetValue(this, component);
            }
        }

        private void InjectGetAddComponent()
        {
            var fields = GetFieldsWithAttribute(typeof(InjectAdd));
            foreach (var field in fields)
            {
                var type = field.FieldType;
                var component = GetComponent(type);
                if (component == null)
                {
                    Debug.LogWarning("GetComponent typeof(" + type.Name + ") in game object '" + gameObject.name +
                                     "' is null");
                    component = gameObject.AddComponent(type);
                    // continue;
                }

                field.SetValue(this, component);
            }

            // 프로퍼티 처리
            var properties = GetPropertiesWithAttribute(typeof(Inject));
            foreach (var property in properties)
            {
                var type = property.PropertyType;
                var component = GetComponent(type);
                if (component == null)
                {
                    Debug.LogWarning($"GetComponent typeof({type.Name}) in game object '{gameObject.name}' is null");
                    continue;
                }

                property.SetValue(this, component);
            }
        }

        private void InjectGetChildrenComponent()
        {
            var fields = GetFieldsWithAttribute(typeof(InjectChild));
            foreach (var field in fields)
            {
                var type = field.FieldType;
                var component = GetComponentInChildren(type);
                if (component == null)
                {
                    Debug.LogWarning("GetComponent typeof(" + type.Name + ") in game object '" + gameObject.name +
                                     "' is null");
                    component = gameObject.AddComponent(type);
                    // continue;
                }

                field.SetValue(this, component);
            }

            // 프로퍼티 처리
            var properties = GetPropertiesWithAttribute(typeof(Inject));
            foreach (var property in properties)
            {
                var type = property.PropertyType;
                var component = GetComponent(type);
                if (component == null)
                {
                    Debug.LogWarning($"GetComponent typeof({type.Name}) in game object '{gameObject.name}' is null");
                    continue;
                }

                property.SetValue(this, component);
            }
        }

        protected IEnumerable<FieldInfo> GetFieldsWithAttribute(Type attributeType)
        {
            var currentType = GetType();
            var fields = new List<FieldInfo>();

            while (currentType != null && currentType != typeof(MonoBehaviour))
            {
                fields.AddRange(
                    currentType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                          BindingFlags.DeclaredOnly)
                        .Where(field => field.GetCustomAttributes(attributeType, true).FirstOrDefault() != null)
                );
                currentType = currentType.BaseType;
            }

            return fields;
        }

        protected IEnumerable<PropertyInfo> GetPropertiesWithAttribute(Type attributeType)
        {
            var currentType = GetType();
            var properties = new List<PropertyInfo>();

            while (currentType != null && currentType != typeof(MonoBehaviour))
            {
                properties.AddRange(
                    currentType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                              BindingFlags.DeclaredOnly)
                        .Where(prop => prop.GetCustomAttributes(attributeType, true).FirstOrDefault() != null &&
                                       prop.CanWrite) // set 가능한 프로퍼티만 선택
                );
                currentType = currentType.BaseType;
            }

            return properties;
        }
    }
}