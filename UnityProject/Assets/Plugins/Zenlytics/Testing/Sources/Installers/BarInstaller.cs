using UnityEngine;

using Zenject;

[CreateAssetMenu(fileName = "BarInstaller", menuName = "Installers/BarInstaller")]
public class BarInstaller : ScriptableObjectInstaller<BarInstaller>
{

    public override void InstallBindings()
    {
        Container.DeclareSignal<BarSignal>();
    }

}