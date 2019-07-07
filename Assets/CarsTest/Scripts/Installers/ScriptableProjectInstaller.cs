using CarsTest;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectInstaller", menuName = "CarsTest/ProjectInstaller")]
public class ScriptableProjectInstaller : ScriptableObjectInstaller<ScriptableProjectInstaller>
{
    [SerializeField] private GameInfo gameInfo;
    [SerializeField] private CarsInfo carsInfo;
    [SerializeField] private ScenesInfo scenesInfo;
    [SerializeField] private GameStateInstaller.Settings gameStates;
    
    public override void InstallBindings()
    {
        Container.BindInstance(gameInfo).IfNotBound();
        Container.BindInstance(carsInfo).IfNotBound();
        Container.BindInstance(scenesInfo).IfNotBound();
        Container.BindInstance(gameStates).IfNotBound();
    }
}