using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.LongLongStep.Presenter.PlayerCharacterPresenterStates
{
    public class FallState : State
    {
        public FallState(StatesContext context) : base(context)
        {
        }


        public override sealed void OnStart()
        {
            foreach (var foot in Context.Foots)
            {
                foot.gameObject.SetActive(false);
            }
        }


        public override sealed void OnStepStarted()
        {
            foreach (var foot in Context.Foots)
            {
                foot.gameObject.SetActive(true);
            }
            Context.Foots[Context.SteppingFoot].position =
                Context.Model.Position;
            Context.SwitchState<StepState>();
        }
    }
}
