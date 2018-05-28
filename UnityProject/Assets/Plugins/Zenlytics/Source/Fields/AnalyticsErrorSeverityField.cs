using System;

using UnityEngine;

using Zenlytics.Enums;

namespace Zenlytics.Fields
{

    [Serializable]
    public class AnalyticsErrorSeverityField : AnalyticsField
    {

        [SerializeField]
        private ErrorSeverity m_Value;

        public static Type[] FieldTypes
        {
            get
            {
                return new[]
                {
                    typeof(ErrorSeverity)
                };
            }
        }

        public ErrorSeverity GetValue(object[] args)
        {
            if (UseConstant)
            {
                return m_Value;
            }

            object valueObj = GetObjectValue(args);
            var value = (ErrorSeverity) int.Parse(valueObj.ToString());
            return value;
        }

    }

}