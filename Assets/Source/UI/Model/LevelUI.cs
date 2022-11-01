using System;
using Splatrika.LongLongStep.Model;


namespace Splatrika.LongLongStep
{
    public class LevelUI : IDisposable
    {
        public int MaxLifes { get; }

        private bool _rotateLeftPressed;
        private bool _rotateRightPressed;
        private ILevelService _levelService { get; }
        private IPlayerCharacter _playerCharacter { get; }
        private IPauseControlService _pauseService { get; }
        private ILevelsManagementService _levelsManagementService { get; }
        private IScenesService _scenesService { get; }

        public event Action ShowedCompleted;
        public event Action ShowedFailed;
        public event Action HidedPlayerHUD;
        public event Action ShowedPlayerHUD;
        public event Action ShowedPause;
        public event Action HidedPause;
        public event Action<int> UpdatedLifes;
        public event Action HidedPauseButton;


        public LevelUI(
            ILevelService levelService,
            IObjectProviderService<IPlayerCharacter> playerProvider,
            IPauseControlService pauseService,
            ILevelsManagementService levelsService,
            IScenesService scenesService)
        {
            _levelService = levelService;
            _playerCharacter = playerProvider.Object;
            _pauseService = pauseService;

            _levelService.Completed += OnLevelCompleted;
            _levelService.Failed += OnLevelFailed;
            _playerCharacter.Damaged += OnPlayerDamaget;

            MaxLifes = _playerCharacter.Lifes;
            _levelsManagementService = levelsService;
            _scenesService = scenesService;
        }


        public void Dispose()
        {
            _levelService.Completed -= OnLevelCompleted;
            _levelService.Failed -= OnLevelFailed;
            _playerCharacter.Damaged -= OnPlayerDamaget;
        }


        public void Pause()
        {
            _pauseService.Pause();
            ShowedPause?.Invoke();
            HidedPlayerHUD?.Invoke();
        }


        public void Unpause()
        {
            _pauseService.Unpause();
            HidedPause?.Invoke();
            ShowedPlayerHUD?.Invoke();
        }


        public void SetRotationLeft(bool value)
        {
            _rotateLeftPressed = value;
            if (!_rotateLeftPressed && !_rotateRightPressed)
            {
                _playerCharacter.StopRotation();
                return;
            }
            _playerCharacter.StartRotation(-1);
        }


        public void SetRotationRight(bool value)
        {
            _rotateRightPressed = value;
            if (!_rotateLeftPressed && !_rotateRightPressed)
            {
                _playerCharacter.StopRotation();
                return;
            }
            _playerCharacter.StartRotation(1);
        }


        public void Exit()
        {
            _scenesService.Load(Scenes.LevelSelect);
        }


        public void GiveUp()
        {
            Exit();
        }


        public void Continue()
        {
            if (_levelsManagementService.HasNext())
            {
                _levelsManagementService.TryLoadNext();
            }
            else
            {
                Exit();
            }
        }


        public void Restart()
        {
            _levelsManagementService.Load(
                _levelsManagementService.Current.Id);
        }


        public void TryAgain()
        {
            Restart();
        }


        private void OnLevelCompleted()
        {
            ShowedCompleted?.Invoke();
            HidedPlayerHUD?.Invoke();
            HidedPauseButton?.Invoke();
        }


        private void OnLevelFailed()
        {
            ShowedFailed?.Invoke();
            HidedPlayerHUD?.Invoke();
            HidedPauseButton?.Invoke();
        }


        private void OnPlayerDamaget()
        {
            UpdatedLifes?.Invoke(_playerCharacter.Lifes);
        }
    }
}
