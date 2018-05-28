using UnityEngine;

using Zenlytics.Enums;

namespace Zenlytics.Adapters
{

    [CreateAssetMenu(fileName = "DebugAnalyticsAdapter", menuName = "Analytics/Adapter/Debug")]
    public class DebugAnalyticsAdapter : AnalyticsAdapter
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
            Log(
                "DebugAnalyticsAdapter.ReportBusinessEvent(currency:{0}, amount:{1}, itemType:{2}, itemId:{3}, cartType:{4}, receipt:{4}, signature:{4})",
                currency,
                amount,
                itemType,
                itemId,
                cartType,
                receipt,
                signature);
        }

        public override void ReportResourceEvent(
            ResourceType type,
            string currency,
            float amount,
            string itemType,
            string itemId)
        {
            Log(
                "DebugAnalyticsAdapter.ReportResourceEvent(type:{0}, currency:{1}, amount:{2}, itemType:{3}, itemId:{4})",
                type,
                currency,
                amount,
                itemType,
                itemId);
        }

        public override void ReportProgressionEvent(
            ProgressionStatus status,
            string p01,
            string p02,
            string p03,
            int score)
        {
            Log(
                "DebugAnalyticsAdapter.ReportProgressionEvent(status:{0}, p01:{1}, p02:{2}, p03:{3}, score:{4})",
                status,
                p01,
                p02,
                p03,
                score);
        }

        public override void ReportErrorEvent(ErrorSeverity severity, string message)
        {
            Log("DebugAnalyticsAdapter.ReportErrorEvent(severity:{0}, message:{1})", severity, message);
        }

        public override void ReportDesignEvent(string eventName, float eventValue)
        {
            Log("DebugAnalyticsAdapter.ReportDesignEvent(eventName:{0}, eventValue:{1})", eventName, eventValue);
        }

        private static void Log(string format, params object[] args)
        {
            if (Debug.isDebugBuild || Application.isEditor)
            {
                Debug.LogFormat(format, args);
            }
        }

    }

}