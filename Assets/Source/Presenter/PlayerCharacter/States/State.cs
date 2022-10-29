using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.LongLongStep.Presenter.PlayerCharacterPresenterStates
{
    public abstract class State
    {
        protected StatesContext Context { get; }

        public virtual void OnStart() { }
        public virtual void OnUpdate(float deltaTime) { }
        public virtual void OnStepStarted() { }
        public virtual void OnWait() { }
        public virtual void OnFalled() { }
        public virtual void OnDied() { }
        public virtual void OnStepTargetUpdated(Vector3? value) { }


        public State(StatesContext context)
        {
            Context = context;
        }
    }
}
