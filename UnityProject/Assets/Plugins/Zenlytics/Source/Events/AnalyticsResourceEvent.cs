using UnityEngine;

using Zenlytics.Enums;
using Zenlytics.Fields;

namespace Zenlytics.Events
{

    [CreateAssetMenu(fileName = "AnalyticsResourceEvent", menuName = "Analytics/Events/Resource")]
    public class AnalyticsResourceEvent : AnalyticsEvent
    {

        [SerializeField]
        private AnalyticsResourceTypeField m_FlowType;

        [SerializeField]
        private AnalyticsStringField m_ResourceCurrency;

        [SerializeField]
        private AnalyticsFloatField m_Amount;

        [SerializeField]
        private AnalyticsStringField m_ItemType;

        [SerializeField]
        private AnalyticsStringField m_ItemId;

        protected override void HandleOnSignalFired(params object[] args)
        {
            ResourceType flowType = m_FlowType.GetValue(args);
            string resourceCurrency = m_ResourceCurrency.GetValue(args);
            float amount = m_Amount.GetValue(args);
            string itemType = m_ItemType.GetValue(args);
            string itemId = m_ItemId.GetValue(args);

            AnalyticsManager.ReportResourceEvent(flowType, resourceCurrency, amount, itemType, itemId);
        }

    }

}