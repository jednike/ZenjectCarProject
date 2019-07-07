using System;
using Zenject;

namespace CarsTest
{
    public interface IGameModeBehaviour
    {
        void Initialize();
        void Update();
        void Dispose();
    }

    public class GameModeManager: IInitializable, ITickable, IDisposable
    {
        private readonly IGameModeBehaviour _currentGameModeBehaviour;
        private readonly GameStateManager _gameStateManager;
        private readonly SignalBus _signalBus;
        
        private bool _destroyed;
        
        public GameModeManager(IGameModeBehaviour gameModeBehaviour, SignalBus signalBus, GameStateManager gameStateManager)
        {
            _signalBus = signalBus;
            _gameStateManager = gameStateManager;
            _currentGameModeBehaviour = gameModeBehaviour;
        }

        public virtual void Initialize()
        {
            _currentGameModeBehaviour.Initialize();
            _signalBus.Subscribe<GameModeEndSignal>(OnGameModeEnd);
            _gameStateManager.ChangeState(GameState.GamePlay);
        }

        private void OnGameModeEnd(GameModeEndSignal gameModeEnd)
        {
            _gameStateManager.ChangeState(GameState.EndGame);
            _currentGameModeBehaviour.Dispose();
            Dispose();
        }

        public void Tick()
        {
            if(_destroyed)
                return;
            _currentGameModeBehaviour.Update();
        }

        public void Dispose()
        {
            _destroyed = true;
        }
    }
    public class GameModeRunnerManager : GameModeManager
    {
        private readonly RunnerFacade.Factory _factory;
        public GameModeRunnerManager(IGameModeBehaviour gameModeBehaviour, SignalBus signalBus, GameStateManager gameStateManager, RunnerFacade.Factory factory) : base(gameModeBehaviour, signalBus, gameStateManager)
        {
            _factory = factory;
        }

        public override void Initialize()
        {
            _factory.Create();
            base.Initialize();
        }
    }
    public class GameModeChaserManager : GameModeManager
    {
        private readonly NetworkCarFacade.Factory _factory;
        public GameModeChaserManager(IGameModeBehaviour gameModeBehaviour, SignalBus signalBus, GameStateManager gameStateManager, [Inject(Id = "PlayerChaser")] NetworkCarFacade.Factory factory) : base(gameModeBehaviour, signalBus, gameStateManager)
        {
            _factory = factory;
        }

        public override void Initialize()
        {
            _factory.Create();
            base.Initialize();
        }
    }
}