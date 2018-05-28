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
            string eventNameFormat = "resource.{0}";
            string eventName = string.Format(eventNameFormat, type);
            var dictionary = new Dictionary<string, object>
            {
                {
                    "currency", currency
                },
                {
                    "amount", amount
                },
                {
                    "itemType", itemType
                },
                {
                    "itemId", itemId
                }
            };
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

            var dictionary = new Dictionary<string, object>
            {
                {
                    "progression01", p01
                },
                {
                    "score", score
                }
            };

            if (!string.IsNullOrEmpty(p02))
            {
                dictionary.Add("progression02", p02);
                if (!string.IsNullOrEmpty(p03))
                {
                    dictionary.Add("progression03", p03);
                }
            }

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