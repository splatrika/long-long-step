using System;
using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep
{
    public class TutorialUI: IDisposable
    {
        public Direction HintDirection { get; private set; }

        private ITutorialService _tutorial { get; }
        private IPlayerCharacter _playerCharacter { get; }

        public event Action ShowedControlHint;
        public event Action HidedControlHint;
        public event Action TutorialFinished;


        public TutorialUI(
            ITutorialService tutorial,
            IObjectProviderService<IPlayerCharacter> playerProvider,
            ILogger logger)
        {
            _tutorial = tutorial;
            _playerCharacter = playerProvider.Object;

            _tutorial.StartedWaitingForRotate += OnStartedWaitingForRotate;
            _tutorial.StopedWaitingForRotate += OnStoppedWaitingForRotate;
            _tutorial.FreeGameUnlocked += OnFreeGameUnlocked;

            if (_tutorial.PlayerRotationDirection == 1)
            {
                HintDirection = Direction.Right;
            }
            else if (_tutorial.PlayerRotationDirection == -1)
            {
                HintDirection = Direction.Left;
            }
            else
            {
                logger.LogError(nameof(TutorialUI),
                    "Invalid rotation direction");
            }
        }


        public void Dispose()
        {
            _tutorial.StartedWaitingForRotate -= OnStartedWaitingForRotate;
            _tutorial.StopedWaitingForRotate -= OnStoppedWaitingForRotate;
            _tutorial.FreeGameUnlocked -= OnFreeGameUnlocked;
        }


        public void SetLeft(bool pressed)
        {
            SetRotation(-1, pressed);
        }


        public void SetRight(bool pressed)
        {
            SetRotation(1, pressed);
        }


        private void SetRotation(int direction, bool pressed)
        {
            if (pressed && _tutorial.PlayerRotationDirection == direction)
            {
                _playerCharacter.StartRotation(direction);
            }
            if (!pressed)
            {
                _playerCharacter.StopRotation();
            }
        }


        private void OnStartedWaitingForRotate()
        {
            ShowedControlHint?.Invoke();
        }


        private void OnStoppedWaitingForRotate()
        {
            HidedControlHint?.Invoke();
        }


        private void OnFreeGameUnlocked()
        {
            TutorialFinished?.Invoke();
        }


        public enum Direction
        {
            Left,
            Right
        }
    }
}
