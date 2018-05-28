using System;

using UnityEngine;

namespace Zenlytics.Fields
{

    [Serializable]
    public class AnalyticsFloatField : AnalyticsField
    {

        [SerializeField]
        private float m_Value;

        public static Type[] FieldTypes
        {
            get
            {
                return new[]
                {
                    typeof(float),
                    typeof(byte),
                    typeof(short),
                    typeof(int)
                };
            }
        }

        public float GetValue(object[] args)
        {
            if (UseConstant)
            {
                return m_Value;
            }

            object valueObj = GetObjectValue(args);
            float value = float.Parse(valueObj.ToString());
            return value;
        }

    }

}