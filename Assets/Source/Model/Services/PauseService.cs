using System;

namespace Splatrika.LongLongStep.Model
{
    public class PauseService : IPauseService, IPauseControlService
    {
        public bool IsPaused { get; private set; }

        public event Action Paused;
        public event Action Updaused;


        public void Pause()
        {
            IsPaused = true;
            Paused?.Invoke();
        }


        public void Unpause()
        {
            IsPaused = false;
            Updaused?.Invoke();
        }
    }
}
