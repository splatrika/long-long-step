namespace Splatrika.LongLongStep.Model
{
    public class LevelConfiguration
    {
        public float ActionTime { get; set; }
        public float WaitTime { get; set; }


        public LevelConfiguration(float actionTime, float waitTime)
        {
            ActionTime = actionTime;
            WaitTime = waitTime;
        }
    }
}
