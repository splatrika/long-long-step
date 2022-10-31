namespace Splatrika.LongLongStep.Model.PlayerCharacterStates
{
    public class HappyState : State
    {
        public HappyState(StatesContext context)
            : base(context)
        {
        }


        public override void OnStart()
        {
            Context.RaiseHappy();
        }
    }
}
