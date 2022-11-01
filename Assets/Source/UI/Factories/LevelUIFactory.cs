using System.Collections;
using System.Collections.Generic;
using Splatrika.LongLongStep.Architecture;
using UnityEngine;
using UnityEngine.UI;

namespace Splatrika.LongLongStep
{
    [GUI]
    public class LevelUIFactory : SceneObjectFactory<LevelUI, LevelUIPresenter>
    {
        [SerializeField]
        private CanvasGroup _pauseMenu;

        [SerializeField]
        private CanvasGroup _levelCompleteMenu;

        [SerializeField]
        private CanvasGroup _levelFailMenu;

        [SerializeField]
        private Button _pause;

        [SerializeField]
        private Button _unpause;

        [SerializeField]
        private Button _continue;

        [SerializeField]
        private Button _exit;

        [SerializeField]
        private Button _restart;

        [SerializeField]
        private Button _tryAgain;

        [SerializeField]
        private Button _giveUp;

        [SerializeField]
        private HealthBar _healthBar;

        [SerializeField]
        private string _leftInputButton;

        [SerializeField]
        private string _rightInputButton;


        protected override void SetupPresenter(LevelUIPresenter presenter)
        {
            presenter.Init(_pauseMenu, _levelCompleteMenu, _levelFailMenu,
                _pause, _unpause, _continue, _exit, _restart, _tryAgain,
                _giveUp, _healthBar, _leftInputButton, _rightInputButton);
        }
    }
}
