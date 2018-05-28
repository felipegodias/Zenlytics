using UnityEngine;

using Zenject;

[CreateAssetMenu(fileName = "QuxInstaller", menuName = "Installers/QuxInstaller")]
public class QuxInstaller : ScriptableObjectInstaller<QuxInstaller>
{

    public override void InstallBindings()
    {
        Container.DeclareSignal<QuxSignal>();
    }

}