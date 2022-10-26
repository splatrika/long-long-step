namespace Splatrika.LongLongStep.Architecture
{
    public interface IPresenter
    {
        object Model { get; }

        bool TryGetModel<T>(out T model) where T : class;
    }
}
