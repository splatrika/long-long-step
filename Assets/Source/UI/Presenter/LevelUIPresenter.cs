using Splatrika.LongLongStep.Architecture;
using UnityEngine;
using UnityEngine.UI;

namespace Splatrika.LongLongStep
{
    public class LevelUIPresenter : Presenter<LevelUI>
    {
        private CanvasGroup _pauseMenu;
        private CanvasGroup _levelCompleteMenu;
        private CanvasGroup _levelFailMenu;
        private Button _pause;
        private Button _unpause;
        private Button _continue;
        private Button _exit;
        private Button _restart;
        private Button _tryAgain;
        private Button _giveUp;
        private HealthBar _healthBar;
        private string _leftInputButton;
        private string _rightInputButton;


        public void Init(
            CanvasGroup pauseMenu,
            CanvasGroup levelCompleteMenu,
            CanvasGroup levelFailMenu,
            Button pause,
            Button unpause,
            Button @continue,
            Button exit,
            Button restart,
            Button tryAgain,
            Button giveUp,
            HealthBar healthBar,
            string leftInputButton,
            string rightInputButton)
        {
            _pauseMenu = pauseMenu;
            _levelCompleteMenu = levelCompleteMenu;
            _levelFailMenu = levelFailMenu;
            _pause = pause;
            _unpause = unpause;
            _continue = @continue;
            _exit = exit;
            _restart = restart;
            _tryAgain = tryAgain;
            _giveUp = giveUp;
            _healthBar = healthBar;
            _leftInputButton = leftInputButton;
            _rightInputButton = rightInputButton;

            _healthBar.SetMaxLifes(TypedModel.MaxLifes);
            _healthBar.SetLifes(TypedModel.MaxLifes);
            _pauseMenu.gameObject.SetActive(false);
            _levelCompleteMenu.gameObject.SetActive(false);
            _levelFailMenu.gameObject.SetActive(false);

            TypedModel.ShowedCompleted += OnShowedCompleted;
            TypedModel.ShowedFailed += OnShowedFailed;
            TypedModel.HidedPlayerHUD += OnHidedPlayerHUD;
            TypedModel.ShowedPlayerHUD += OnShowedPlayerHUD;
            TypedModel.ShowedPause += OnShowedPause;
            TypedModel.HidedPause += OnHidedPause;
            TypedModel.UpdatedLifes += OnLifesUpdated;
            TypedModel.HidedPauseButton += OnHidedPauseButton;

            _pause.onClick.AddListener(OnPauseClick);
            _unpause.onClick.AddListener(OnUnpaseClick);
            _continue.onClick.AddListener(OnContinueClick);
            _exit.onClick.AddListener(OnExitClick);
            _restart.onClick.AddListener(OnRestartClick);
            _tryAgain.onClick.AddListener(OnTryAgainClick);
            _giveUp.onClick.AddListener(OnGiveUpClick);
        }


        private void OnDestroy()
        {
            TypedModel.ShowedCompleted -= OnShowedCompleted;
            TypedModel.ShowedFailed -= OnShowedFailed;
            TypedModel.HidedPlayerHUD -= OnHidedPlayerHUD;
            TypedModel.ShowedPlayerHUD -= OnShowedPlayerHUD;
            TypedModel.ShowedPause -= OnShowedPause;
            TypedModel.HidedPause -= OnHidedPause;
            TypedModel.UpdatedLifes -= OnLifesUpdated;
            TypedModel.HidedPauseButton -= OnHidedPauseButton;
        }


        protected override void OnUpdate(float deltaTime)
        {
            if (Input.GetButtonDown(_leftInputButton))
            {
                TypedModel.SetRotationLeft(true);
            }
            if (Input.GetButtonUp(_leftInputButton))
            {
                TypedModel.SetRotationLeft(false);
            }
            if (Input.GetButtonDown(_rightInputButton))
            {
                TypedModel.SetRotationRight(true);
            }
            if (Input.GetButtonUp(_rightInputButton))
            {
                TypedModel.SetRotationRight(false);
            }
        }


        private void OnPauseClick()
        {
            TypedModel.Pause();
        }


        private void OnUnpaseClick()
        {
            TypedModel.Unpause();
        }


        private void OnGiveUpClick()
        {
            TypedModel.GiveUp();
        }


        private void OnExitClick()
        {
            TypedModel.Exit();
        }


        private void OnRestartClick()
        {
            TypedModel.Restart();
        }


        private void OnTryAgainClick()
        {
            TypedModel.TryAgain();
        }


        private void OnContinueClick()
        {
            TypedModel.Continue();
        }


        private void OnLifesUpdated(int lifes)
        {
            _healthBar.SetLifes(lifes);
        }


        private void OnShowedCompleted()
        {
            _levelCompleteMenu.gameObject.SetActive(true);
        }


        private void OnShowedFailed()
        {
            _levelFailMenu.gameObject.SetActive(true);
        }


        private void OnShowedPlayerHUD()
        {
            _healthBar.gameObject.SetActive(true);
        }


        private void OnHidedPlayerHUD()
        {
            _healthBar.gameObject.SetActive(false);
        }


        private void OnShowedPause()
        {
            _pauseMenu.gameObject.SetActive(true);
        }


        private void OnHidedPause()
        {
            _pauseMenu.gameObject.SetActive(false);
        }


        private void OnHidedPauseButton()
        {
            _pause.gameObject.SetActive(false);
        }
    }
}
