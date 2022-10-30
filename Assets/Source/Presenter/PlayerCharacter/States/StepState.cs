using UnityEngine;

namespace Splatrika.LongLongStep.Presenter.PlayerCharacterPresenterStates
{
    public class StepState : State
    {
        public StepState(StatesContext context) : base(context)
        {
        }


        public override sealed void OnStart()
        {
            Context.SteppingFoot++;
            Context.SteppingFoot %= Context.Foots.Length;
            Context.PreviousPosition = Context.Model.Position;
            Context.StepTarget.gameObject.SetActive(true);
        }


        public override sealed void OnUpdate(float deltaTime)
        {
            var steppingPosition = Vector3.Lerp(
                Context.Model.Position,
                (Vector3)Context.Model.StepTarget,
                (float)Context.Model.Progress);
            Context.Foots[Context.SteppingFoot].position = steppingPosition;
            Context.StepTarget.position = (Vector3)Context.Model.StepTarget;
        }


        public override void OnMoved(Vector3 position)
        {
            Context.Foots[Context.StayingFoot].position = position;
        }


        public override sealed void OnFalled()
        {
            Context.SwitchState<FallState>();
        }


        public override sealed void OnWait()
        {
            Context.StepTarget.gameObject.SetActive(false);
            Context.Foots[Context.SteppingFoot].position =
                Context.Model.Position;
            Context.SwitchState<WaitState>();
        }
    }
}
