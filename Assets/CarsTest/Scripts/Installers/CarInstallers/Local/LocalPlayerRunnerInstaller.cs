using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class LocalPlayerRunnerInstaller: RunnerInstaller
    {        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CarBehaviour>().AsSingle();
            base.InstallBindings();
            PlayerControlledInstaller.Install(Container);
            
            BindChasers();
        }

        protected virtual void BindChasers()
        {
            Container.Bind<ChaserRegistry>().AsSingle();
            Container.BindInterfacesAndSelfTo<AIChaserSpawner>().AsSingle();
            Container.BindFactory<ChaserFacade, ChaserFacade.Factory>()
                .WithId("AIChaser")
                .FromPoolableMemoryPool<ChaserFacade, ChaserFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(4)
                    .FromSubContainerResolve()
                    .ByNewGameObjectInstaller<LocalAIChaserInstaller>()
                    .WithGameObjectName("AIChaser")
                    .UnderTransformGroup("Chasers"));
        }

        protected class ChaserFacadePool : MonoPoolableMemoryPool<IMemoryPool, ChaserFacade>
        {
        }
    }
}