using UnityEngine;

namespace Splatrika.LongLongStep.Model.TutorialStates
{
    public class FirstStepState : State
    {
        private const float _rotationTargetDelta = 0.05f;
        private bool _rotatedEnough;
        private IPlayerCharacter _playerCharacter;


        public FirstStepState(
            StatesContext context,
            IPlayerCharacter playerCharacter) : base(context)
        {
            _playerCharacter = playerCharacter;
        }


        public override void Update(float deltaTime)
        {
            if (!_rotatedEnough && RotatedEnough(_playerCharacter))
            {
                _rotatedEnough = true;
                _playerCharacter.RestrictRotation();
            }
        }


        public override void OnPlayerStoppedRotation()
        {
            if (!_rotatedEnough)
            {
                Context.SwitchState<WaitForRotationState>();
            }
        }


        public override void OnPlayerWait()
        {
            _playerCharacter.AllowRotation();
            Context.SwitchState<ShowGoalState>();
        }


        private bool RotatedEnough(IPlayerCharacter character)
        {
            return Mathf.Abs(
                (float)character.StepTargetRadians - Context.TargetPlayerRotation)
                < _rotationTargetDelta;
        }
    }
}
