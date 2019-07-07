using CarsTest.Photon;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class CheckpointsModeBehaviour: IGameModeBehaviour
    {
        private readonly DefaultModeBehaviour _defaultModeBehaviour;
        protected readonly SignalBus SignalBus;
        protected readonly LevelInfo LevelInfo;

        public int CheckpointsCount => LevelInfo.checkpoints.Length;
        public int CurrentCheckpoint { get; protected internal set; }

        public CheckpointsModeBehaviour(DefaultModeBehaviour defaultModeBehaviour, CheckPointsModeInfo modeInfo, LevelInfo levelInfo, SignalBus signalBus)
        {
            _defaultModeBehaviour = defaultModeBehaviour;
            LevelInfo = levelInfo;            
            SignalBus = signalBus;
        }

        public virtual void Initialize()
        {
            _defaultModeBehaviour.Initialize();
            
            CurrentCheckpoint = 0;
            foreach (var checkpoint in LevelInfo.checkpoints)
            {
                checkpoint.gameObject.SetActive(false);
            }
            LevelInfo.checkpoints[0].gameObject.SetActive(true);
        }

        public void Update()
        {
            
        }

        public bool GetCurrentCheckpoint(out Transform checkpointTransform, int direction = 0)
        {
            if (CurrentCheckpoint + direction < CheckpointsCount && CurrentCheckpoint + direction > 0)
            {
                checkpointTransform = LevelInfo.checkpoints[CurrentCheckpoint + direction];
                return true;
            }
            checkpointTransform = null;
            return false;
        }
        public bool GetNextCheckpoint(out Transform checkpointTransform)
        {
            return GetCurrentCheckpoint(out checkpointTransform, 1);
        }
        public bool GetPrevCheckpoint(out Transform checkpointTransform)
        {
            return GetCurrentCheckpoint(out checkpointTransform, -1);
        }
        public virtual void Dispose()
        {
            _defaultModeBehaviour.Dispose();
        }
    }
    public class CheckpointsModeRunnerBehaviour: CheckpointsModeBehaviour
    {
        public CheckpointsModeRunnerBehaviour(DefaultModeBehaviour defaultModeBehaviour, CheckPointsModeInfo modeInfo, LevelInfo levelInfo, SignalBus signalBus) : base(defaultModeBehaviour, modeInfo, levelInfo, signalBus)
        {
        }
        
        public override void Initialize()
        {
            base.Initialize();
            SignalBus.Subscribe<RunnerTouchCheckpointSignal>(OnRunnerTouchCheckpoint);
        }
        private void OnRunnerTouchCheckpoint()
        {
            CurrentCheckpoint++;
            if(CheckOnEnd())
                return;
            LevelInfo.checkpoints[CurrentCheckpoint-1].gameObject.SetActive(false);
            LevelInfo.checkpoints[CurrentCheckpoint].gameObject.SetActive(true);
        }
        private bool CheckOnEnd()
        {
            if (CurrentCheckpoint < CheckpointsCount)
                return false;
            SignalBus.Fire(new GameModeEndSignal( true));
            SignalBus.Unsubscribe<RunnerTouchCheckpointSignal>(OnRunnerTouchCheckpoint);
            return true;
        }

        public override void Dispose()
        {
        }
    }
    public class CheckpointsModeNetworkBehaviour: NetworkGameModeObserver
    {
        private DefaultModeBehaviour _modeBehaviour;
        private CheckpointsModeBehaviour _checkpointsModeBehaviour;

        [Inject] public void Construct(DefaultModeBehaviour modeBehaviour, CheckpointsModeBehaviour checkpointsModeBehaviour)
        {
            _modeBehaviour = modeBehaviour;
            _checkpointsModeBehaviour = checkpointsModeBehaviour;
        }

        public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsReading)
            {
                _modeBehaviour.RunnerLives = (int) stream.ReceiveNext();
                _checkpointsModeBehaviour.CurrentCheckpoint = (int) stream.ReceiveNext();
            } else if (stream.IsWriting)
            {
                stream.SendNext(_modeBehaviour.RunnerLives);
                stream.SendNext(_checkpointsModeBehaviour.CurrentCheckpoint);
            }
        }
    }
}