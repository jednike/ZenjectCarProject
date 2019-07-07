using CarsTest.Photon;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class SurviveModeBehaviour: IGameModeBehaviour
    {
        protected readonly DefaultModeBehaviour ModeBehaviour;
        protected readonly SurviveModeInfo SurviveModeInfo;
        protected readonly SignalBus SignalBus;
        protected internal float CurrentTime;

        public SurviveModeBehaviour(DefaultModeBehaviour modeBehaviour, SurviveModeInfo modeInfo, SignalBus signalBus)
        {
            ModeBehaviour = modeBehaviour;
            SignalBus = signalBus;
            SurviveModeInfo = modeInfo;
        }

        public float GetSecondsToEnd()
        {
            return SurviveModeInfo.SurviveTime - CurrentTime;
        }

        public virtual void Initialize()
        {
            ModeBehaviour.Initialize();
        }

        public virtual void Update()
        {
            ModeBehaviour.Update();
        }

        public void Dispose()
        {
            ModeBehaviour.Dispose();
        }
    }
    public class SurviveModeRunnerBehaviour: SurviveModeBehaviour
    {
        public SurviveModeRunnerBehaviour(DefaultModeBehaviour modeBehaviour, SurviveModeInfo modeInfo, SignalBus signalBus) : base(modeBehaviour, modeInfo, signalBus)
        {
        }
        
        public override void Initialize()
        {
            base.Initialize();
            CurrentTime = 0;
        }

        public override void Update()
        {
            base.Update();
            CurrentTime += Time.deltaTime;
            CheckOnEnd();
        }

        private bool CheckOnEnd()
        {
            if (CurrentTime < SurviveModeInfo.SurviveTime)
                return false;
            
            SignalBus.Fire(new GameModeEndSignal( true));
            return true;
        }
    }
    public class SurviveModeNetworkBehaviour: NetworkGameModeObserver
    {
        private DefaultModeBehaviour _modeBehaviour;
        private SurviveModeBehaviour _surviveModeBehaviour;

        [Inject] public void Construct(DefaultModeBehaviour modeBehaviour, SurviveModeBehaviour surviveModeBehaviour)
        {
            _modeBehaviour = modeBehaviour;
            _surviveModeBehaviour = surviveModeBehaviour;
        }

        public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsReading)
            {
                _modeBehaviour.RunnerLives = (int) stream.ReceiveNext();
                _surviveModeBehaviour.CurrentTime = (float) stream.ReceiveNext();
            } else if (stream.IsWriting)
            {
                stream.SendNext(_modeBehaviour.RunnerLives);
                stream.SendNext(_surviveModeBehaviour.CurrentTime);
            }
        }
    }
}