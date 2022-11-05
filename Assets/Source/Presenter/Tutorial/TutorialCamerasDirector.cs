using Cinemachine;
using Splatrika.LongLongStep.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.LongLongStep.Presenter
{
    public class TutorialCamerasDirector : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera _player;

        [SerializeField]
        private CinemachineVirtualCamera _goal;

        private ITutorialService _tutorial;


        [Inject]
        public void Init(ITutorialService tutorial)
        {
            _tutorial = tutorial;
            _tutorial.AccentedGoal += OnTutorialAccentedGoal;
            _tutorial.AccentedPlayer += OnTutorialAccentedPlayer;
        }


        private void OnDestroy()
        {
            _tutorial.AccentedGoal -= OnTutorialAccentedGoal;
            _tutorial.AccentedPlayer -= OnTutorialAccentedPlayer;
        }


        private void OnTutorialAccentedGoal()
        {
            ResetPriority();
            _goal.Priority = 1;
        }


        private void OnTutorialAccentedPlayer()
        {
            ResetPriority();
            _player.Priority = 1;
        }


        private void ResetPriority()
        {
            _player.Priority = 0;
            _goal.Priority = 0;
        }
    }
}
