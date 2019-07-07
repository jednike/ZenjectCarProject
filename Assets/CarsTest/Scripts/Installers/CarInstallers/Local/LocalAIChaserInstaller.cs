using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CarsTest
{
    public class LocalAIChaserInstaller: ChaserInstaller
    {        
        public override void InstallBindings()
        {            
            Container.Bind<CarDeathHandler>().To<AIChaserDeathHandler>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CarBehaviour>().AsSingle();
            base.InstallBindings();
            
            Container.BindInterfacesAndSelfTo<ChaserStateManager>().AsSingle();
            Container.Bind<ChaserStateIdle>().AsSingle();
            Container.Bind<ChaserStateFollow>().AsSingle();
            
            Container.Bind<AIInfo>().FromComponentOn(CarObject).AsSingle();
        }
    }
}