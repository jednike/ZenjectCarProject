using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class RunnerInstaller: CarInstaller
    {        
        public override void InstallBindings()
        {
            CarId = 1;
            
            base.InstallBindings();

            Container.BindInterfacesTo<RespawnController>().AsSingle().NonLazy();
            Container.Bind<RunnerDeathHandler>().AsSingle();
            Container.Bind<RunnerCheckpointHandler>().AsSingle();
            Container.Bind<RunnerFacade>().FromNewComponentOnRoot().AsSingle().NonLazy();
            Container.InstantiateComponent<RunnerCollisionHandler>(CarObject);
        }
    }
}