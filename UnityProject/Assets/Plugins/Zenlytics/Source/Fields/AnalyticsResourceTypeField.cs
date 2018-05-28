using System;

using UnityEngine;

using Zenlytics.Enums;

namespace Zenlytics.Fields
{

    [Serializable]
    public class AnalyticsResourceTypeField : AnalyticsField
    {

        [SerializeField]
        private ResourceType m_Value;

        public static Type[] FieldTypes
        {
            get
            {
                return new[]
                {
                    typeof(ResourceType)
                };
            }
        }

        public ResourceType GetValue(object[] args)
        {
            if (UseConstant)
            {
                return m_Value;
            }

            object valueObj = GetObjectValue(args);
            var value = (ResourceType) int.Parse(valueObj.ToString());
            return value;
        }

    }

}