namespace Splatrika.LongLongStep.Model.TutorialStates
{
    public abstract class State
    {
        protected StatesContext Context { get; }

        public virtual void OnStart() { }
        public virtual void Update(float deltaTime) { }
        public virtual void OnPlayerFalled() { }
        public virtual void OnPlayerStepStarted() { }
        public virtual void OnPlayerWait() { }
        public virtual void OnPlayerStartedRotation() { }
        public virtual void OnPlayerStoppedRotation() { }


        protected State(StatesContext context)
        {
            Context = context;
        }
    }
}
