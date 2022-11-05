using System;
using UnityEngine;

namespace Splatrika.LongLongStep.Model.PlayerCharacterStates
{
    public class StatesContext
    {
        public delegate void DamageDelegate(out bool died);

        public float? Progress
        {
            get => _getProgress.Invoke();
            set => _setProgess.Invoke(value);
        }
        public Vector3 Position
        {
            get => _getPosition.Invoke();
            set => _setPosition.Invoke(value);
        }
        public Vector3? StepTarget
        {
            get => _getStepTarget.Invoke();
            set => _setStepTarget.Invoke(value);
        }
        public int Lifes
        {
            get => _getLifes.Invoke();
        }
        public IGround Ground
        {
            get => _getGround.Invoke();
            set => _setGround.Invoke(value);
        }
        public float? StepTargetRadians
        {
            set => _setStepTargetRadians.Invoke(value);
        }
        public bool RotationAllowed
        {
            get => _getRotationAllowed.Invoke();
        }

        private Action<Type> _switchState;
        private Action<float?> _setProgess;
        private Action<Vector3> _setPosition;
        private Action<Vector3?> _setStepTarget;
        private Action<IGround> _setGround;
        private Action<float?> _setStepTargetRadians;
        private DamageDelegate _damage;
        private Func<float?> _getProgress;
        private Func<Vector3> _getPosition;
        private Func<Vector3?> _getStepTarget;
        private Func<int> _getLifes;
        private Func<IGround> _getGround;
        private Func<bool> _getRotationAllowed;
        private Action _raiseStartStep;
        private Action _raiseWait;
        private Action _raiseFalled;
        private Action _raiseHappy;
        private Action<int> _raiseStartedRotation;
        private Action _raiseStoppedRotation;


        public StatesContext(
            Action<Type> switchState,
            Action<float?> setProgess,
            Action<Vector3> setPosition,
            Action<Vector3?> setStepTarget,
            Action<IGround> setGround,
            Action<float?> setStepTargetRadians,
            DamageDelegate damage,
            Func<float?> getProgress,
            Func<Vector3> getPosition,
            Func<Vector3?> getStepTarget,
            Func<int> getLifes,
            Func<IGround> getGround,
            Func<bool> getRotationAllowed,
            Action raiseStartStep,
            Action raiseWait,
            Action raiseFalled,
            Action raiseHappy,
            Action<int> raiseStartedRotation,
            Action raiseStoppedRotation)
        {
            _switchState = switchState;
            _setProgess = setProgess;
            _setPosition = setPosition;
            _setStepTarget = setStepTarget;
            _setGround = setGround;
            _setStepTargetRadians = setStepTargetRadians;

            _damage = damage;

            _getProgress = getProgress;
            _getPosition = getPosition;
            _getStepTarget = getStepTarget;
            _getLifes = getLifes;
            _getGround = getGround;
            _getRotationAllowed = getRotationAllowed;

            _raiseStartStep = raiseStartStep;
            _raiseWait = raiseWait;
            _raiseFalled = raiseFalled;
            _raiseHappy = raiseHappy;
            _raiseStartedRotation = raiseStartedRotation;
            _raiseStoppedRotation = raiseStoppedRotation;
        }


        public void SwitchState<T>() where T : State
        {
            _switchState.Invoke(typeof(T));
        }


        public void Damage(out bool died)
        {
            _damage.Invoke(out died);
        }


        public void RaiseStartStep()
        {
            _raiseStartStep.Invoke();
        }


        public void RaiseWait()
        {
            _raiseWait.Invoke();
        }


        public void RaiseFalled()
        {
            _raiseFalled.Invoke();
        }


        public void RaiseHappy()
        {
            _raiseHappy.Invoke();
        }


        public void RaiseStartedRotation(int direction)
        {
            _raiseStartedRotation.Invoke(direction);
        }


        public void RaiseStoppedRotation()
        {
            _raiseStoppedRotation.Invoke();
        }
    }
}
