using System;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class ProjectInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            #if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
            gameInfo.mobileUi = true;            
            #endif
            Container.BindInterfacesAndSelfTo<SceneBehaviour>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<NetworkConnector>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<NetworkBehaviour>().AsSingle().NonLazy();
        }
    }
}