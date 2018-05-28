using System;

using UnityEngine;

namespace Zenlytics.Fields
{

    [Serializable]
    public class AnalyticsIntField : AnalyticsField
    {

        [SerializeField]
        private int m_Value;

        public static Type[] FieldTypes
        {
            get
            {
                return new[]
                {
                    typeof(byte),
                    typeof(short),
                    typeof(int)
                };
            }
        }

        public int GetValue(object[] args)
        {
            if (UseConstant)

            {
                return m_Value;
            }

            object valueObj = GetObjectValue(args);
            int value = int.Parse(valueObj.ToString());
            return value;
        }

    }

}