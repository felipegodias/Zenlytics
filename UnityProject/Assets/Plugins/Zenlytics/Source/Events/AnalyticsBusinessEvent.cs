using UnityEngine;

using Zenlytics.Fields;

namespace Zenlytics.Events
{

    [CreateAssetMenu(fileName = "AnalyticsBusinessEvent", menuName = "Analytics/Events/Business")]
    public class AnalyticsBusinessEvent : AnalyticsEvent
    {

        [SerializeField]
        private AnalyticsStringField m_CartType;

        [SerializeField]
        private AnalyticsStringField m_ItemType;

        [SerializeField]
        private AnalyticsStringField m_ItemId;

        [SerializeField]
        private AnalyticsIntField m_Amount;

        [SerializeField]
        private AnalyticsStringField m_Currency;

        [SerializeField]
        private AnalyticsStringField m_Receipt;

        [SerializeField]
        private AnalyticsStringField m_Signature;

        protected override void HandleOnSignalFired(params object[] args)
        {
            string cartType = m_CartType.GetValue(args);
            string itemType = m_ItemType.GetValue(args);
            string itemId = m_ItemId.GetValue(args);
            int amount = m_Amount.GetValue(args);
            string currency = m_Currency.GetValue(args);
            string receipt = m_Receipt.GetValue(args);
            string signature = m_Signature.GetValue(args);

            AnalyticsManager.ReportBusinessEvent(currency, amount, itemType, itemId, cartType, receipt, signature);
        }

    }

}