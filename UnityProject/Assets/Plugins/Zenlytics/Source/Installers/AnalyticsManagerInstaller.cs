using System;

using UnityEngine;

using Zenject;

using Zenlytics.Managers;

namespace Zenlytics.Installers
{

    [CreateAssetMenu(fileName = "AnalyticsManagerInstaller", menuName = "Analytics/Installer")]
    public class AnalyticsManagerInstaller : ScriptableObjectInstaller<AnalyticsManagerInstaller>
    {

        [SerializeField]
        private AnalyticsManager m_AnalyticsManager;

        public override void InstallBindings()
        {
            Container.Bind<AnalyticsManager>().FromInstance(m_AnalyticsManager).AsSingle();
            Container.Bind<ITickable>().FromInstance(m_AnalyticsManager).AsSingle();
            Container.Bind<IDisposable>().FromInstance(m_AnalyticsManager).AsSingle();
        }

    }

}