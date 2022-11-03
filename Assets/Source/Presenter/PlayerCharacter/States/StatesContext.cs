using System;
using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep.Presenter.PlayerCharacterPresenterStates
{
    public class StatesContext
    {
        public PlayerCharacter Model => _getModel.Invoke();
        public Transform[] Foots => _getFoots.Invoke();
        public Transform StepTarget => _getStepTarget.Invoke();
        public Rigidbody Rigidbody => _getRigidbody.Invoke();
        public Transform SelfTransform => _getSelfTransform.Invoke();
        public int SteppingFoot
        {
            get => _getSteppingFoot.Invoke();
            set => _setSteppingFoot.Invoke(value);
        }
        public int StayingFoot => Foots.Length - SteppingFoot - 1;
        public Vector3 PreviousPosition { get; set; }

        private Action<Type> _switchState;
        private Func<PlayerCharacter> _getModel;
        private Func<Transform[]> _getFoots;
        private Func<Transform> _getStepTarget;
        private Func<int> _getSteppingFoot;
        private Func<Rigidbody> _getRigidbody;
        private Func<Transform> _getSelfTransform;
        private Action<int> _setSteppingFoot;


        public StatesContext(
            Action<Type> switchState,
            Func<PlayerCharacter> getModel,
            Func<Transform[]> getFoots,
            Func<Transform> getStepTarget,
            Func<int> getSteppingFoot,
            Func<Rigidbody> getRigidbody,
            Func<Transform> getSelfTransform,
            Action<int> setSteppingFoot)
        {
            _switchState = switchState;
            _getModel = getModel;
            _getFoots = getFoots;
            _getStepTarget = getStepTarget;
            _getSteppingFoot = getSteppingFoot;
            _getRigidbody = getRigidbody;
            _getSelfTransform = getSelfTransform;
            _setSteppingFoot = setSteppingFoot;
        }


        public void SwitchState<T>() where T : State
        {
            _switchState.Invoke(typeof(T));
        }
    }
}
