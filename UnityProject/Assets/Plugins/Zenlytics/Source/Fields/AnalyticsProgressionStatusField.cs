using System;

using UnityEngine;

using Zenlytics.Enums;

namespace Zenlytics.Fields
{

    [Serializable]
    public class AnalyticsProgressionStatusField : AnalyticsField
    {

        [SerializeField]
        private ProgressionStatus m_Value;

        public static Type[] FieldTypes
        {
            get
            {
                return new[]
                {
                    typeof(ProgressionStatus)
                };
            }
        }

        public ProgressionStatus GetValue(object[] args)
        {
            if (UseConstant)
            {
                return m_Value;
            }

            object valueObj = GetObjectValue(args);
            var value = (ProgressionStatus) int.Parse(valueObj.ToString());
            return value;
        }

    }

}