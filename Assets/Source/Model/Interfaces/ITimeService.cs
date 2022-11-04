namespace Splatrika.LongLongStep.Model
{
    public interface ITimeService: IRevertableAction
    {
        void Register(IRevertableAction @object);
        void Unregister(IRevertableAction @object);
    }
}
