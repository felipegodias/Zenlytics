using UnityEngine;

using Zenject;

[CreateAssetMenu(fileName = "FooInstaller", menuName = "Installers/FooInstaller")]
public class FooInstaller : ScriptableObjectInstaller<FooInstaller>
{

    public override void InstallBindings()
    {
        Container.DeclareSignal<FooSignal>();
    }

}