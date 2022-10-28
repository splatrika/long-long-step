using UnityEngine;

namespace Splatrika.LongLongStep.Model
{
    public interface IPhysicsService
    {
        bool HasGround(Vector3 point);
    }
}
