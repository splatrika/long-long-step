using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.LongLongStep.Presenter.PlayerCharacterPresenterStates
{
    public class WaitState : State
    {
        private Transform _stayingFoot;


        public WaitState(StatesContext context) : base(context)
        {
        }


        public override sealed void OnStart()
        {
            var stayingFoot = Context.Foots.Length - Context.SteppingFoot - 1;
            _stayingFoot = Context.Foots[stayingFoot];
            _stayingFoot.position = Context.PreviousPosition;
        }


        public override sealed void OnUpdate(float deltaTime)
        {
            _stayingFoot.position = Vector3.Lerp(
                Context.PreviousPosition,
                Context.Model.Position,
                (float)Context.Model.Progress);
        }


        public override sealed void OnStepStarted()
        {
            _stayingFoot.position = Context.PreviousPosition;
            Context.SwitchState<StepState>();
        }
    }
}
