using UnityEngine;
using Zenject;

namespace Splatrika.LongLongStep.Architecture
{
    public abstract class Presenter<TModel> : Presenter, IPresenter
        where TModel : class
    {
        protected TModel TypedModel { get; private set; }

        private ILogger _logger;

        protected virtual void OnConstruct(TModel model) { }


        [Inject]
        public void Init(ILogger logger)
        {
            _logger = logger;
        }


        protected override sealed void OnConstruct(object model)
        {
            if (!(model is TModel))
            {
                _logger.LogError(nameof(Presenter<TModel>),
                    $"{nameof(OnConstruct)}: " +
                    $"Passed model isn't a {typeof(TModel).Name}");
                return;
            }
            TypedModel = (TModel)model;
            OnConstruct(TypedModel);
        }
    }
}
