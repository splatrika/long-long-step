using System;

namespace Splatrika.LongLongStep.Model
{
    public interface ITutorialService
    {
        float TargetPlayerRotation { get; }
        int PlayerRotationDirection { get; }

        event Action StartedWaitingForRotate;
        event Action StopedWaitingForRotate;
        event Action AccentedGoal;
        event Action AccentedPlayer;
        event Action FreeGameUnlocked;
        event Action Completed;
        event Action Failed;
    }
}
