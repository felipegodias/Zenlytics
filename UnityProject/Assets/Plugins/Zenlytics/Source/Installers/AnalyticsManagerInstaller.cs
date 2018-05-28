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
            Container.BindInterfacesAndSelfTo<AnalyticsManager>().FromInstance(m_AnalyticsManager).AsSingle();
        }

    }

}