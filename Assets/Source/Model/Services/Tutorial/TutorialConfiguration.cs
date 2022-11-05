namespace Splatrika.LongLongStep.Model
{
    public class TutorialConfiguration
    {
        public int PlayerRotationDirection { get; set; } // 1 or -1
        public float TargetPlayerRotation { get; set; } // radians
        public float AccentGoalTime { get; set; }


        public TutorialConfiguration(
            int playerRotationDirection,
            float targetPlayerRotation,
            float accentGoalTime)
        {
            PlayerRotationDirection = playerRotationDirection;
            TargetPlayerRotation = targetPlayerRotation;
            AccentGoalTime = accentGoalTime;
        }
    }
}
