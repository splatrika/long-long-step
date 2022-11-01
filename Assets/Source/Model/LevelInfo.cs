namespace Splatrika.LongLongStep.Model
{
    public class LevelInfo
    {
        public int Id { get; }
        public string Scene { get; }


        public LevelInfo(int id, string scene)
        {
            Id = id;
            Scene = scene;
        }
    }
}
