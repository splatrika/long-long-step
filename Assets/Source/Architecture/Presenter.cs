using System;
using UnityEngine;

namespace Splatrika.LongLongStep.Architecture
{
    public abstract class Presenter : MonoBehaviour, IPresenter
    {
        public object Model { get; private set; }

        protected virtual void OnConstruct(object model) { }
        protected virtual void OnUpdate(float deltaTime) { }
        protected virtual void OnFixedUpdate(float deltaTime) { }
        protected virtual void OnDispose() { }


        public bool TryGetModel<T>(out T model) where T: class
        {
            if (Model is T)
            {
                model = (T)Model;
                return true;
            }
            model = null;
            return false;
        }


        public void Construct(object model)
        {
            Model = model;
            OnConstruct(model);
        }


        private void Update()
        {
            if (Model is IUpdatable updatable)
            {
                updatable.Update(Time.deltaTime);
            }
            OnUpdate(Time.deltaTime);
        }


        private void FixedUpdate()
        {
            if (Model is IFixedUpdatable updatable)
            {
                updatable.FixedUpdate(Time.fixedDeltaTime);
            }
            OnFixedUpdate(Time.fixedDeltaTime);
        }


        private void OnDestroy()
        {
            if (Model is IDisposable disposable)
            {
                disposable.Dispose();
            }
            OnDispose();
        }
    }
}
