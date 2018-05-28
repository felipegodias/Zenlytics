using UnityEngine;

using Zenlytics.Enums;
using Zenlytics.Fields;

namespace Zenlytics.Events
{

    [CreateAssetMenu(fileName = "AnalyticsErrorEvent", menuName = "Analytics/Events/Error")]
    public class AnalyticsErrorEvent : AnalyticsEvent
    {

        [SerializeField]
        private AnalyticsErrorSeverityField m_Severity;

        [SerializeField]
        private AnalyticsStringField m_Message;

        protected override void HandleOnSignalFired(params object[] args)
        {
            ErrorSeverity severity = m_Severity.GetValue(args);
            string message = m_Message.GetValue(args);

            AnalyticsManager.ReportErrorEvent(severity, message);
        }

    }

}