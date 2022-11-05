using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep.Scene
{
    public class TutorialInstaller : LevelInstaller
    {
        [Header("Tutorial")]

        [SerializeField]
        private float _playerRotationTargetDegrees;

        [SerializeField]
        private RotationDirection _playerRotationDirection;

        [SerializeField]
        private float _showGoalDuration;


        protected override sealed void InstallAdditionalBindings()
        {
            var configuration = new TutorialConfiguration(
                (int)_playerRotationDirection,
                _playerRotationTargetDegrees * Mathf.Deg2Rad,
                _showGoalDuration);
            Container.Bind<TutorialConfiguration>()
                .FromInstance(configuration);

            Container.BindInterfacesTo<Tutorial>()
                .AsSingle();
        }


        public enum RotationDirection : int
        {
            Left = -1,
            Right = 1
        }
    }
}
