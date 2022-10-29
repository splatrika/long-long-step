using System;

namespace Splatrika.LongLongStep.Model
{
    public interface IPauseService
    {
        bool IsPaused { get; }

        event Action Paused;
        event Action Updaused;
    }
}
