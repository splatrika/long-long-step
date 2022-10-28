namespace Splatrika.LongLongStep.Model.PlayerCharacterStates
{
    public abstract class State
    {
        protected StatesContext Context { get; }

        public virtual void OnStart() { }
        public virtual void Update(float deltaTime) { }
        public virtual void StartRotation(int direction) { }
        public virtual void StopRotation() { }


        protected State(StatesContext context)
        {
            Context = context;
        }
    }
}
