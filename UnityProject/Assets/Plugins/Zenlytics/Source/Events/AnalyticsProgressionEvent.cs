using UnityEngine;

using Zenlytics.Enums;
using Zenlytics.Fields;

namespace Zenlytics.Events
{

    [CreateAssetMenu(fileName = "AnalyticsProgressionEvent", menuName = "Analytics/Events/Progression")]
    public class AnalyticsProgressionEvent : AnalyticsEvent
    {

        [SerializeField]
        private AnalyticsProgressionStatusField m_ProgressionStatus;

        [SerializeField]
        private AnalyticsStringField m_Progression01;

        [SerializeField]
        private AnalyticsStringField m_Progression02;

        [SerializeField]
        private AnalyticsStringField m_Progression03;

        [SerializeField]
        private AnalyticsIntField m_Value;

        protected override void HandleOnSignalFired(params object[] args)
        {
            ProgressionStatus progressionStatus = m_ProgressionStatus.GetValue(args);
            string progression01 = m_Progression01.GetValue(args);
            string progression02 = m_Progression02.GetValue(args);
            string progression03 = m_Progression03.GetValue(args);
            int value = m_Value.GetValue(args);
            AnalyticsManager.ReportProgressionEvent(
                                                    progressionStatus,
                                                    progression01,
                                                    progression02,
                                                    progression03,
                                                    value);
        }

    }

}