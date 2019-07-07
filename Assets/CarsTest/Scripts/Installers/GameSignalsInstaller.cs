using CarsTest.Photon;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class GameSignalsInstaller : Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {            
            Container.DeclareSignal<AIChaserKilledSignal>();
            Container.BindSignal<AIChaserKilledSignal>().ToMethod(signal =>
            {
                Debug.Log("AI chaser Died");
            });
            
            Container.DeclareSignal<PlayerChaserKilledSignal>();
            Container.BindSignal<PlayerChaserKilledSignal>().ToMethod(signal =>
            {
                Debug.Log("Player chaser Died");
            });
            
            Container.DeclareSignal<RunnerTouchCheckpointSignal>();
            Container.BindSignal<RunnerTouchCheckpointSignal>().ToMethod(signal =>
            {
                Debug.Log("Runner Touch Checkpoint");
            });
            
            Container.DeclareSignal<RunnerDiedSignal>();
            Container.BindSignal<RunnerDiedSignal>().ToMethod(signal =>
            {
                Debug.Log("Runner Died");
            });
            
            Container.DeclareSignal<GameModeEndSignal>();
            Container.BindSignal<GameModeEndSignal>().ToMethod(signal =>
            {
                Debug.Log("Game Mode End " + signal.Success);
            });
            
            Container.DeclareSignal<CarInstantiateSignal>();
            Container.BindSignal<CarInstantiateSignal>().ToMethod(signal =>
            {
                Debug.Log("I want to instantiate " + signal.CarType);
            });
            Container.DeclareSignal<InputInstantiateSignal>();
            Container.BindSignal<InputInstantiateSignal>().ToMethod(signal =>
            {
                Debug.Log("I want to instantiate input");
            });
            Container.DeclareSignal<ManagerInstantiateSignal>();
            Container.BindSignal<ManagerInstantiateSignal>().ToMethod(signal =>
            {
                Debug.Log("I want to instantiate manager");
            });
        }
    }

}
