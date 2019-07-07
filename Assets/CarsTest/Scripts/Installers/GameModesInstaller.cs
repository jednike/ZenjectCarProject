using Zenject;

namespace CarsTest
{
    public class GameModesRunnerInstaller : Installer<GameInfo, GameModesRunnerInstaller>
    {
        private readonly GameInfo _gameInfo;
        public GameModesRunnerInstaller(GameInfo gameInfo)
        {
            _gameInfo = gameInfo;
        }

        public override void InstallBindings()
        {
            switch (_gameInfo.currentGameMode)
            {
                case GameMode.Checkpoint:
                    Container.Bind<DefaultModeBehaviour>().To<DefaultModeRunnerBehaviour>().AsSingle();
                    Container.Bind(typeof(IGameModeBehaviour), typeof(CheckpointsModeBehaviour)).To(typeof(CheckpointsModeRunnerBehaviour)).AsSingle();
                    break;
                case GameMode.Survive:
                    Container.Bind<DefaultModeBehaviour>().To<DefaultModeRunnerBehaviour>().AsSingle();
                    Container.Bind(typeof(IGameModeBehaviour), typeof(SurviveModeBehaviour)).To(typeof(SurviveModeRunnerBehaviour)).AsSingle();
                    break;
                case GameMode.Destroy:
                    Container.Bind<DefaultModeBehaviour>().To<DefaultModeRunnerBehaviour>().AsSingle();
                    Container.Bind(typeof(IGameModeBehaviour), typeof(DestroyModeBehaviour)).To(typeof(DestroyModeRunnerBehaviour)).AsSingle();
                    break;
                default:
                    break;
            }
        }
    }

    public class GameModesChaserInstaller : Installer<GameInfo, GameModesChaserInstaller>
    {
        private readonly GameInfo _gameInfo;
        public GameModesChaserInstaller(GameInfo gameInfo)
        {
            _gameInfo = gameInfo;
        }

        public override void InstallBindings()
        {
            switch (_gameInfo.currentGameMode)
            {
                case GameMode.Checkpoint:
                    Container.Bind<DefaultModeBehaviour>().AsSingle();
                    Container.Bind(typeof(IGameModeBehaviour), typeof(CheckpointsModeBehaviour))
                        .To(typeof(CheckpointsModeBehaviour)).AsSingle();
                    break;
                case GameMode.Survive:
                    Container.Bind<DefaultModeBehaviour>().AsSingle();
                    Container.Bind(typeof(IGameModeBehaviour), typeof(SurviveModeBehaviour))
                        .To(typeof(SurviveModeBehaviour)).AsSingle();
                    break;
                case GameMode.Destroy:
                    Container.Bind<DefaultModeBehaviour>().AsSingle();
                    Container.Bind(typeof(IGameModeBehaviour), typeof(DestroyModeBehaviour))
                        .To(typeof(DestroyModeBehaviour)).AsSingle();
                    break;
                default:
                    break;
            }
        }
    }
}