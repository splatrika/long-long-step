using UnityEngine;

namespace Splatrika.LongLongStep.Model
{
    public class MovingGroundConfiguration
    {
        public Vector3 PointA { get; set; }
        public Vector3 PointB { get; set; }
        public float Speed { get; set; }
        public float WaitTime { get; set; }


        public MovingGroundConfiguration(
            Vector3 pointA,
            Vector3 pointB,
            float speed,
            float waitTime)
        {
            PointA = pointA;
            PointB = pointB;
            Speed = speed;
            WaitTime = waitTime;
        }
    }
}
