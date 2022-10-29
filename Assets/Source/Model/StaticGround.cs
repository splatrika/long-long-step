using UnityEngine;

namespace Splatrika.LongLongStep.Model
{
    public class StaticGround : IGround
    {
        public Vector3 Anchor { get; }


        public StaticGround(StaticGroundConfiguration configuration)
        {
            Anchor = configuration.Anchor;
        }
    }
}
