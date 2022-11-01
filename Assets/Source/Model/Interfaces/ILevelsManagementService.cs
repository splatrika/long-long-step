namespace Splatrika.LongLongStep.Model
{
    public interface ILevelsManagementService
    {
        LevelInfo Current { get; }

        void Load(int id);
        bool HasNext();
        void TryLoadNext();
    }
}
