using System;
using UnityEngine;

namespace Splatrika.LongLongStep.Model
{
    public interface IPlayerCharacter
    {
        float? Progress { get; }
        Vector3 Position { get; }
        Vector3? StepTarget { get; }
        int Lifes { get; }

        event Action StepStarted;
        event Action Wait;
        event Action Falled;
        event Action Damaged;
        event Action Died;
        event Action<Vector3> Moved;
        event Action<Vector3?> StepTargetUpdated;
        event Action<IGround> TouchedGround;
        event Action Happy;


        void StartRotation(int direction);
        void StopRotation();
    }
}
