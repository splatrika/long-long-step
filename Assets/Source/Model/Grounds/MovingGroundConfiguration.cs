using UnityEngine;

namespace Splatrika.LongLongStep.Model
{
    public class MovingGroundConfiguration
    {
        public Vector3 PointA { get; set; }
        public Vector3 PointB { get; set; }
        public float MovementDuration { get; set; }
        public float WaitTime { get; set; }
        public bool WaitAtStart { get; set; }


        public MovingGroundConfiguration(
            Vector3 pointA,
            Vector3 pointB,
            float speed,
            float waitTime,
            bool waitAtStart)
        {
            PointA = pointA;
            PointB = pointB;
            MovementDuration = speed;
            WaitTime = waitTime;
            WaitAtStart = waitAtStart;
        }
    }
}
