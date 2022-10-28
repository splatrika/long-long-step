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

        private Action<Type> _switchState;
        private Action<float?> _setProgess;
        private Action<Vector3> _setPosition;
        private Action<Vector3?> _setStepTarget;
        private DamageDelegate _damage;
        private Func<float?> _getProgress;
        private Func<Vector3> _getPosition;
        private Func<Vector3?> _getStepTarget;
        private Func<int> _getLifes;
        private Action _raiseStartStep;
        private Action _raiseWait;
        private Action _raiseFalled;


        public StatesContext(
            Action<Type> switchState,
            Action<float?> setProgess,
            Action<Vector3> setPosition,
            Action<Vector3?> setStepTarget,
            DamageDelegate damage,
            Func<float?> getProgress,
            Func<Vector3> getPosition,
            Func<Vector3?> getStepTarget,
            Func<int> getLifes,
            Action raiseStartStep,
            Action raiseWait,
            Action raiseFalled)
        {
            _switchState = switchState;
            _setProgess = setProgess;
            _setPosition = setPosition;
            _setStepTarget = setStepTarget;
            _damage = damage;
            _getProgress = getProgress;
            _getPosition = getPosition;
            _getStepTarget = getStepTarget;
            _getLifes = getLifes;
            _raiseStartStep = raiseStartStep;
            _raiseWait = raiseWait;
            _raiseFalled = raiseFalled;
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
    }
}
