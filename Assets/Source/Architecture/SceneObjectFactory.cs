using UnityEngine;
using Zenject;

namespace Splatrika.LongLongStep.Architecture
{
    public abstract class SceneObjectFactory<TModel, TPresenter> : MonoBehaviour
        where TPresenter: Presenter
    {
        private DiContainer _container;

        protected virtual void OnFirstInit() { }
        protected virtual void AdditionalBindings(DiContainer container) { }
        protected virtual void SetupPresenter(TPresenter presenter) { }


        [Inject]
        public void Init(SceneContext context)
        {
            OnFirstInit();
            _container = new DiContainer(context.Container);
            BindAdapters(_container);
            AdditionalBindings(_container);
        }


        private void BindAdapters(DiContainer container)
        {
            var adapters = GetComponents<IComponentAdapter>();
            foreach (var adapter in adapters)
            {
                var contracts = adapter.GetType().GetInterfaces();
                container.Bind(contracts)
                    .FromInstance(adapter);
            }
        }


        private void Start()
        {
            var model = _container.Instantiate<TModel>();
            var presenter = gameObject.AddComponent<TPresenter>();
            _container.Inject(presenter);
            presenter.Construct(model);
            SetupPresenter(presenter);
        }
    }
}
