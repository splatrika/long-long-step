using System;

namespace Splatrika.LongLongStep.Model
{
    public interface ILevelService
    {
        event Action Completed;
        event Action Failed;
    }
}
