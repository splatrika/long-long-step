using UnityEngine;

namespace Splatrika.LongLongStep.Model
{
    public class StaticGroundConfiguration
    {
        public Vector3 Anchor { get; }


        public StaticGroundConfiguration(Vector3 anchor)
        {
            Anchor = anchor;
        }
    }
}
