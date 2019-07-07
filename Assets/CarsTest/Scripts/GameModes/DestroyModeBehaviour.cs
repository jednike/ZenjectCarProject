using CarsTest.Photon;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    
    public class DestroyModeBehaviour: IGameModeBehaviour
    {
        protected readonly DefaultModeBehaviour DefaultModeBehaviour;
        protected readonly DestroyModeInfo DestroyModeInfo;
        protected readonly SignalBus SignalBus;

        protected internal int DestroyedChasers;

        public DestroyModeBehaviour(DefaultModeBehaviour defaultModeBehaviour, DestroyModeInfo destroyModeInfo, SignalBus signalBus)
        {
            DefaultModeBehaviour = defaultModeBehaviour;
            SignalBus = signalBus;
            DestroyModeInfo = destroyModeInfo;
        }

        public virtual void Initialize()
        {
            DefaultModeBehaviour.Initialize();
        }

        public virtual void Update()
        {
            DefaultModeBehaviour.Update();
        }

        public void Dispose()
        {
            DefaultModeBehaviour.Dispose();
        }
    }
    public class DestroyModeRunnerBehaviour: DestroyModeBehaviour
    {
        public DestroyModeRunnerBehaviour(DefaultModeBehaviour defaultModeBehaviour, DestroyModeInfo destroyModeInfo, SignalBus signalBus) : base(defaultModeBehaviour, destroyModeInfo, signalBus)
        {
        }
        
        public override void Initialize()
        {
            base.Initialize();
            DestroyedChasers = 0;
            SignalBus.Subscribe<AIChaserKilledSignal>(ChaserKilled);
            SignalBus.Subscribe<PlayerChaserKilledSignal>(ChaserKilled);
        }

        private void ChaserKilled()
        {
            DestroyedChasers++;
            CheckOnEnd();
        }

        private bool CheckOnEnd()
        {
            if (DestroyedChasers < DestroyModeInfo.ChaserLives)
                return false;
            
            SignalBus.Fire(new GameModeEndSignal( true));
            return true;
        }
    }
    public class DestroyModeNetworkBehaviour: NetworkGameModeObserver
    {
        private DefaultModeBehaviour _defaultModeBehaviour;
        private DestroyModeBehaviour _destroyModeBehaviour;

        [Inject] public void Construct(DefaultModeBehaviour modeBehaviour, DestroyModeBehaviour destroyModeBehaviour)
        {
            _defaultModeBehaviour = modeBehaviour;
            _destroyModeBehaviour = destroyModeBehaviour;
        }

        public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsReading)
            {
                _defaultModeBehaviour.RunnerLives = (int) stream.ReceiveNext();
                _destroyModeBehaviour.DestroyedChasers = (int) stream.ReceiveNext();
            } else if (stream.IsWriting)
            {
                stream.SendNext(_defaultModeBehaviour.RunnerLives);
                stream.SendNext(_destroyModeBehaviour.DestroyedChasers);
            }
        }
    }
}