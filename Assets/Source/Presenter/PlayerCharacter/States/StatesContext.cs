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
        private Action<int> _setSteppingFoot;


        public StatesContext(
            Action<Type> switchState,
            Func<PlayerCharacter> getModel,
            Func<Transform[]> getFoots,
            Func<Transform> getStepTarget,
            Func<int> getSteppingFoot,
            Action<int> setSteppingFoot)
        {
            _switchState = switchState;
            _getModel = getModel;
            _getFoots = getFoots;
            _getStepTarget = getStepTarget;
            _getSteppingFoot = getSteppingFoot;
            _setSteppingFoot = setSteppingFoot;
        }


        public void SwitchState<T>() where T : State
        {
            _switchState.Invoke(typeof(T));
        }
    }
}
