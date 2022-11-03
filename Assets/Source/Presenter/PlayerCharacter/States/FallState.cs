using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.LongLongStep.Presenter.PlayerCharacterPresenterStates
{
    public class FallState : State
    {
        private Vector3 _lastPosition;
        private Quaternion _lastRotation;


        public FallState(StatesContext context) : base(context)
        {
        }


        public override sealed void OnStart()
        {
            _lastPosition = Context.SelfTransform.position;
            _lastRotation = Context.SelfTransform.rotation;
            Context.Rigidbody.isKinematic = false;
        }


        public override sealed void OnStepStarted()
        {
            Context.Foots[Context.SteppingFoot].position =
                Context.Model.Position;
            Context.SwitchState<StepState>();
            Context.Rigidbody.isKinematic = true;
            Context.SelfTransform.position = _lastPosition;
            Context.SelfTransform.rotation = _lastRotation;
        }
    }
}
