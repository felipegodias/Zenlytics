using System;

using UnityEngine;
using UnityEngine.SceneManagement;

using Zenject;

using Zenlytics.Adapters;
using Zenlytics.Enums;
using Zenlytics.Events;

namespace Zenlytics.Managers
{

    [CreateAssetMenu(fileName = "AnalyticsManager", menuName = "Analytics/Manager")]
    public class AnalyticsManager : ScriptableObject, IInitializable, ITickable, IDisposable, IAnalyticsManager
    {

        [SerializeField]
        private AnalyticsEvent[] m_AnalyticsEvents;

        [SerializeField]
        private AnalyticsAdapter[] m_AnalyticsAdapters;

        private bool m_isInjected;

        public void Initialize()
        {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            foreach (AnalyticsEvent analyticsEvent in m_AnalyticsEvents)
            {
                analyticsEvent.Initialize(this);
            }
        }

        public void Tick()
        {
            if (m_isInjected)
            {
                return;
            }

            m_isInjected = true;

            ProjectContext projectContext = ProjectContext.Instance;
            var sceneContext = FindObjectOfType<SceneContext>();

            DiContainer diContainer = sceneContext != null ? sceneContext.Container : projectContext.Container;

            foreach (AnalyticsEvent analyticsEvent in m_AnalyticsEvents)
            {
                analyticsEvent.Unlisten();
                analyticsEvent.Inject(diContainer);
                analyticsEvent.Listen();
            }
        }

        public void Dispose()
        {
            m_isInjected = false;
            foreach (AnalyticsEvent analyticsEvent in m_AnalyticsEvents)
            {
                analyticsEvent.Unlisten();
            }
        }

        public void ReportBusinessEvent(
            string currency,
            int amount,
            string itemType,
            string itemId,
            string cartType,
            string receipt,
            string signature)
        {
            foreach (AnalyticsAdapter analyticsAdapter in m_AnalyticsAdapters)
            {
                analyticsAdapter.ReportBusinessEvent(currency, amount, itemType, itemId, cartType, receipt, signature);
            }
        }

        public void ReportResourceEvent(
            ResourceType type,
            string currency,
            float amount,
            string itemType,
            string itemId)
        {
            foreach (AnalyticsAdapter analyticsAdapter in m_AnalyticsAdapters)
            {
                analyticsAdapter.ReportResourceEvent(type, currency, amount, itemType, itemId);
            }
        }

        public void ReportProgressionEvent(ProgressionStatus status, string p01, string p02, string p03, int score)
        {
            foreach (AnalyticsAdapter analyticsAdapter in m_AnalyticsAdapters)
            {
                analyticsAdapter.ReportProgressionEvent(status, p01, p02, p03, score);
            }
        }

        public void ReportErrorEvent(ErrorSeverity severity, string message)
        {
            foreach (AnalyticsAdapter analyticsAdapter in m_AnalyticsAdapters)
            {
                analyticsAdapter.ReportErrorEvent(severity, message);
            }
        }

        public void ReportDesignEvent(string eventName, float eventValue)
        {
            foreach (AnalyticsAdapter analyticsAdapter in m_AnalyticsAdapters)
            {
                analyticsAdapter.ReportDesignEvent(eventName, eventValue);
            }
        }

        private void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            m_isInjected = false;
        }

    }

}