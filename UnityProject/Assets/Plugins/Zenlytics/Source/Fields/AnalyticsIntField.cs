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

        public int GetValue(object argument)
        {
            if (UseConstant)

            {
                return m_Value;
            }

            return 0;
        }

    }

}