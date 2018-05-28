using System;
using System.Reflection;

using UnityEngine;

namespace Zenlytics.Fields
{

    [Serializable]
    public abstract class AnalyticsField
    {

        [SerializeField]
        private bool m_UseConstant;

        [SerializeField]
        private int m_ArgumentIndex;

        [SerializeField]
        private string m_ArgumentProperty;

        public bool UseConstant
        {
            get
            {
                return m_UseConstant;
            }
        }

        public int ArgumentIndex
        {
            get
            {
                return m_ArgumentIndex;
            }
        }

        public string ArgumentProperty
        {
            get
            {
                return m_ArgumentProperty;
            }
        }

        protected object GetObjectValue(object[] args)
        {
            object arg = args[ArgumentIndex];
            Type type = arg.GetType();
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            PropertyInfo propertyInfo = type.GetProperty(ArgumentProperty, bindingFlags);
            object valueObj = propertyInfo.GetValue(arg, null);
            return valueObj;
        }

    }

}