using UnityEngine;

namespace CarsTest
{
    public class ChaserStateFollow : IChaserState
    {
        private readonly CarView _view;
        private readonly AIInfo _aiInfo;
        private readonly CarInput _input;
        private readonly RunnerFacade _runner;
        private readonly ChaserStateManager _stateManager;

        private Vector3 _targetPosition;

        public ChaserStateFollow(
            RunnerFacade runner,
            CarView view,
            AIInfo aiInfo,
            ChaserStateManager stateManager,
            CarInput input)
        {
            _stateManager = stateManager;
            _view = view;
            _aiInfo = aiInfo;
            _runner = runner;
            _input = input;
        }

        public void EnterState()
        {
            _input.SteeringInput = 0;
            _input.ThrottleInput = 0;
            
            _targetPosition = _runner.Position;
        }
        public void ExitState()
        {
        }
        
        public void Update()
        {
            var forward = _aiInfo.sensorPosition.forward;
            var right = _aiInfo.sensorPosition.right;
            
            var frontCollision = DetectCollisionChance(forward, _aiInfo.frontOffset, _aiInfo.frontDistanceReaction);
            var backCollision = DetectCollisionChance(-forward, _aiInfo.backOffset, _aiInfo.backDistanceReaction);
            var leftCollision = DetectCollisionChance(-right, _aiInfo.sideOffset, _aiInfo.sideDistanceReaction);
            var rightCollision = DetectCollisionChance(right, _aiInfo.sideOffset, _aiInfo.sideDistanceReaction);

            var playerCollision = DetectCollisionChance((_runner.Position - _view.Position).normalized, 0, Mathf.Infinity);
            _targetPosition = _runner.Position;
            
            var steerInput = TurnInput(AngleBetweenMeAndTarget);
            var throttleInput = ThrottleInput(DistanceToTarget);

            _input.ThrottleInput = throttleInput;
            _input.SteeringInput = steerInput;
        }

        public void FixedUpdate()
        {
        }
            
        private float DistanceToTarget => (_view.Position - _targetPosition).magnitude;
        private float AngleBetweenMeAndTarget
        {
            get
            {
                var targetDir = _targetPosition - _aiInfo.sensorPosition.position;
                var angle = Vector3.SignedAngle(targetDir, _aiInfo.sensorPosition.forward, Vector3.up);
                return angle;
            }
        }
        private static float TurnInput(float angle)
        {
            return Mathf.Clamp(Mathf.Abs(angle) < 2f? 0: -angle, -1f, 1f);
        }
        private static float ThrottleInput(float distance)
        {
            return Mathf.Clamp01(distance / .4f);
        }
        
        private CollisionChance DetectCollisionChance(Vector3 direction, float offset, float distanceReaction)
        {
            var collisionChance = new CollisionChance
            {
                CollisionType = CollisionType.Nothing,
            };

            if (!Physics.Raycast(_aiInfo.sensorPosition.position + direction * offset, direction, out var hit, distanceReaction))
                return collisionChance;

            switch (hit.collider.tag)
            {
                case "Runner":
                    collisionChance.CollisionType = CollisionType.Runner;
                    break;
                case "Chaser":
                    collisionChance.CollisionType = CollisionType.Chaser;
                    break;
                case "Bonus":
                    collisionChance.CollisionType = CollisionType.Bonus;
                    break;
                default:
                    collisionChance.CollisionType = CollisionType.Obstacle;
                    break;
            }

            collisionChance.Distance = hit.distance;

            return collisionChance;
        }
        
        private enum CollisionType
        {
            Runner,
            Chaser,
            Obstacle,
            Bonus,
            Nothing
        }
        private class CollisionChance
        {
            public CollisionType CollisionType;
            public float Distance;

            public override string ToString()
            {
                return $"{CollisionType}:{Distance}";
            }
        }
    }
}

