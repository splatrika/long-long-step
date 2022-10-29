using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep.Presenter
{
    public class PhysicsAdapter
        : MonoBehaviour, IPhysicsService, IComponentAdapter
    {
        public const string GroundLayer = "Ground";

        private RaycastHit[] hitsBuffer = new RaycastHit[100];


        public bool HasGround(Vector3 point, out IGround ground)
        {
            var mask = LayerMask.GetMask(GroundLayer);
            //return Physics.Raycast(new Ray(point, Vector3.down), 0.2f, mask);
            var ray = new Ray(point, Vector3.down);
            var length = 0.2f;
            var hits = Physics.RaycastNonAlloc(ray, hitsBuffer, length, mask);
            for (int i = 0; i < hits; i++)
            {
                var hit = hitsBuffer[i];
                if (hit.collider.TryGetComponent(out IPresenter hitted))
                {
                    if (hitted.TryGetModel(out ground))
                    {
                        return true;
                    }
                }
            }
            ground = null;
            return false;
            
        }
    }
}
