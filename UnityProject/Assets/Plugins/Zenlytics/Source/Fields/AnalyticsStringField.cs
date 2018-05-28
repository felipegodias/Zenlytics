using System;

using UnityEngine;

namespace Zenlytics.Fields
{

    [Serializable]
    public class AnalyticsStringField : AnalyticsField
    {

        [SerializeField]
        private string m_Value;

        public static Type[] FieldTypes
        {
            get
            {
                return new[]
                {
                    typeof(object)
                };
            }
        }

        public string GetValue(object argument)
        {
            if (UseConstant)
            {
                return m_Value;
            }

            return null;
        }

    }

}