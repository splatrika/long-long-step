using System;

namespace Splatrika.LongLongStep.Model.TutorialStates
{
    public class StatesContext
    {
        public float TargetPlayerRotation => _getTargetPlayerRotation.Invoke();

        private Func<float> _getTargetPlayerRotation;
        private Action<Type> _switchState;
        private Action _raiseStartedWaitingForRotate;
        private Action _raiseStopedWaitingForRotate;
        private Action _raiseAccentedGoal;
        private Action _raiseAccentedPlayer;
        private Action _unlockDefaultLevel;


        public StatesContext(
            Func<float> getTargetPlayerRotation,
            Action<Type> switchState,
            Action raiseStartedWaitingForRotate,
            Action raiseStopedWaitingForRotate,
            Action raiseAccentedGoal,
            Action raiseAccentedPlayer,
            Action unlockDefaultLevel)
        {
            _getTargetPlayerRotation = getTargetPlayerRotation;
            _switchState = switchState;
            _raiseStartedWaitingForRotate = raiseStartedWaitingForRotate;
            _raiseStopedWaitingForRotate = raiseStopedWaitingForRotate;
            _raiseAccentedGoal = raiseAccentedGoal;
            _raiseAccentedPlayer = raiseAccentedPlayer;
            _unlockDefaultLevel = unlockDefaultLevel;
        }


        public void SwitchState<T>() where T : State
        {
            _switchState.Invoke(typeof(T));
        }


        public void RaiseStartedWaitingForRotate()
        {
            _raiseStartedWaitingForRotate.Invoke();
        }


        public void RaiseStoppedWaitingForRotate()
        {
            _raiseStopedWaitingForRotate.Invoke();
        }


        public void RaiseAccentedGoal()
        {
            _raiseAccentedGoal.Invoke();
        }


        public void RaiseAccentedPlayer()
        {
            _raiseAccentedPlayer.Invoke();
        }


        public void UnlockDefaultLevel()
        {
            _unlockDefaultLevel.Invoke();
        }
    }
}
