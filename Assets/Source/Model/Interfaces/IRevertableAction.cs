namespace Splatrika.LongLongStep.Model
{
    public interface IRevertableAction
    {
        void RevertToPreviousAction();
    }
}
