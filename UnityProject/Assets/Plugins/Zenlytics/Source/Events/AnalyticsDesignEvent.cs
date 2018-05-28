using UnityEngine;

using Zenlytics.Fields;

namespace Zenlytics.Events
{

    [CreateAssetMenu(fileName = "AnalyticsDesignEvent", menuName = "Analytics/Events/Design")]
    public class AnalyticsDesignEvent : AnalyticsEvent
    {

        [SerializeField]
        private AnalyticsStringField m_EventName;

        [SerializeField]
        private AnalyticsFloatField m_Value;

        protected override void HandleOnSignalFired(params object[] args)
        {
            string name = m_EventName.GetValue(args);
            float value = m_Value.GetValue(args);
            AnalyticsManager.ReportDesignEvent(name, value);
        }

    }

}