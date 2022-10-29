using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep.Presenter
{
    public class PhysicsAdapter
        : MonoBehaviour, IPhysicsService, IComponentAdapter
    {
        public const string GroundLayer = "Ground";


        public bool HasGround(Vector3 point)
        {
            var mask = LayerMask.GetMask(GroundLayer);
            return Physics.Raycast(new Ray(point, Vector3.down), 0.2f, mask);
        }
    }
}
