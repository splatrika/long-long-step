using System.Collections.Generic;

namespace Splatrika.LongLongStep.Model
{
    public interface ILevelsRepositoryService
    {
        IEnumerable<LevelInfo> GetAll();
        LevelInfo Get(int id);
        bool Contains(int id);
    }
}
