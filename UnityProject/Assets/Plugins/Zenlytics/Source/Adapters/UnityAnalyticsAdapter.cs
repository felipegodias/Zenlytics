using System.Collections.Generic;

using UnityEngine;

using Zenlytics.Enums;

using UnityAnalytics = UnityEngine.Analytics.Analytics;

namespace Zenlytics.Adapters
{

    [CreateAssetMenu(fileName = "UnityAnalyticsAdapter", menuName = "Analytics/Adapter/Unity")]
    public class UnityAnalyticsAdapter : AnalyticsAdapter
    {

        public override void ReportBusinessEvent(
            string currency,
            int amount,
            string itemType,
            string itemId,
            string cartType,
            string receipt,
            string signature)
        {
            string productId = cartType + "." + itemType + "." + itemId;
            UnityAnalytics.Transaction(productId, amount, currency, receipt, signature);
        }

        public override void ReportResourceEvent(
            ResourceType type,
            string currency,
            float amount,
            string itemType,
            string itemId)
        {
            string eventNameFormat = "resource.{0}.{1}.{2}.{3}";
            string eventName = string.Format(eventNameFormat, type, currency, itemType, itemId);
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("amount", amount);
            UnityAnalytics.CustomEvent(eventName, dictionary);
        }

        public override void ReportProgressionEvent(
            ProgressionStatus status,
            string p01,
            string p02,
            string p03,
            int score)
        {
            string eventName = "progression." + status;
            if (!string.IsNullOrEmpty(p01))
            {
                eventName += "." + p01;
                if (!string.IsNullOrEmpty(p02))
                {
                    eventName += "." + p02;
                    if (!string.IsNullOrEmpty(p03))
                    {
                        eventName += "." + p03;
                    }
                }
            }

            var dictionary = new Dictionary<string, object>();
            dictionary.Add("score", score);
            UnityAnalytics.CustomEvent(eventName, dictionary);
        }

        public override void ReportErrorEvent(ErrorSeverity severity, string message)
        {
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("message", message);
            UnityAnalytics.CustomEvent("log." + severity, dictionary);
        }

        public override void ReportDesignEvent(string eventName, float eventValue)
        {
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("value", eventValue);
            UnityAnalytics.CustomEvent("design." + eventName, dictionary);
        }

    }

}