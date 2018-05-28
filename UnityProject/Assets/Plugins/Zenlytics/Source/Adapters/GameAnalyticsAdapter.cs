//using GameAnalyticsSDK;

using UnityEngine;

using Zenlytics.Enums;

namespace Zenlytics.Adapters
{

    [CreateAssetMenu(fileName = "GameAnalyticsAdapter", menuName = "Analytics/Adapter/GameAnalytics")]
    public class GameAnalyticsAdapter : AnalyticsAdapter
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
            /*
            GameAnalytics.NewBusinessEvent(currency, amount, itemType, itemId, cartType);
        */
        }

        public override void ReportResourceEvent(
            ResourceType type,
            string currency,
            float amount,
            string itemType,
            string itemId)
        {
            /*
            var flowType = (GAResourceFlowType) type;
            GameAnalytics.NewResourceEvent(flowType, currency, amount, itemType, itemId);
            */
        }

        public override void ReportProgressionEvent(
            ProgressionStatus status,
            string p01,
            string p02,
            string p03,
            int score)
        {
            /*
            if (string.IsNullOrEmpty(p01))
            {
                return;
            }

            var progressionStatus = (GAProgressionStatus) status;

            if (!string.IsNullOrEmpty(p02))
            {
                if (!string.IsNullOrEmpty(p03))
                {
                    GameAnalytics.NewProgressionEvent(progressionStatus, p01, p02, p03, score);
                }
                else
                {
                    GameAnalytics.NewProgressionEvent(progressionStatus, p01, p02, score);
                }
            }
            else
            {
                GameAnalytics.NewProgressionEvent(progressionStatus, p01, score);
            }
            */
        }

        public override void ReportErrorEvent(ErrorSeverity severity, string message)
        {
            /*
            var errorSeverity = (GAErrorSeverity) severity;
            GameAnalytics.NewErrorEvent(errorSeverity, message);
            */
        }

        public override void ReportDesignEvent(string eventName, float eventValue)
        {
            /*
            GameAnalytics.NewDesignEvent(eventName, eventValue);
            */
        }

    }

}