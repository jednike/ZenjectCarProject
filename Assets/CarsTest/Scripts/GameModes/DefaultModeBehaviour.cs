using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class DefaultModeBehaviour: IGameModeBehaviour
    {
        protected readonly DefaultModeInfo ModeInfo;
        protected readonly SignalBus SignalBus;

        public int RunnerLives { get; set; }

        protected DefaultModeBehaviour(DefaultModeInfo modeInfo, SignalBus signalBus)
        {
            ModeInfo = modeInfo;
            SignalBus = signalBus;
        }

        public virtual void Initialize()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Dispose()
        {
        }
    }
    public class DefaultModeRunnerBehaviour: DefaultModeBehaviour
    {        
        public DefaultModeRunnerBehaviour(DefaultModeInfo modeInfo, SignalBus signalBus): base(modeInfo, signalBus)
        {
        }

        public override void Initialize()
        {
            RunnerLives = ModeInfo.RunnerLives;
            SignalBus.Subscribe<RunnerDiedSignal>(OnRunnerDied);
        }
        private void OnRunnerDied()
        {
            if(ModeInfo.Immortal)
                return;
            RunnerLives--;
            CheckOnEnd();
        }
        private bool CheckOnEnd()
        {
            if (RunnerLives > 0)
                return false;
            
            SignalBus.Fire(new GameModeEndSignal( false));
            SignalBus.Unsubscribe<RunnerDiedSignal>(OnRunnerDied);
            return true;
        }
    }
}