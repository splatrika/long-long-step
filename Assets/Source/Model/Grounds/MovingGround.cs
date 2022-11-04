using System;
using Splatrika.LongLongStep.Architecture;
using UnityEngine;

namespace Splatrika.LongLongStep.Model
{
    public class MovingGround : IGround, IUpdatable, IDisposable,
        IRevertableAction
    {
        public bool Enabled { get; private set; }
        public Vector3 Anchor { get; private set; }
        public Vector3 PointA { get; }
        public Vector3 PointB { get; }
        public float MovementDuration { get; }
        public float WaitTime { get; }

        private IPauseService _pauseService { get; }
        private ITimeService _timeService { get; }
        private float _waitTimeLeft;
        private float _position;
        private float _positionDirection;
        private float _positionSpeed;


        public MovingGround(
            IPauseService pauseService,
            ITimeService timeService,
            MovingGroundConfiguration configuration)
        {
            _pauseService = pauseService;
            _timeService = timeService;

            Enabled = true;
            Anchor = configuration.PointA;
            PointA = configuration.PointA;
            PointB = configuration.PointB;
            MovementDuration = configuration.MovementDuration;
            WaitTime = configuration.WaitTime;

            _position = 0;
            _positionDirection = 1;
            _positionSpeed = 1 / MovementDuration;
            _waitTimeLeft = configuration.WaitAtStart ? WaitTime : 0;

            timeService.Register(this);
        }


        public void Dispose()
        {
            _timeService.Unregister(this);
        }


        public void SetEnabled(bool value)
        {
            Enabled = value;
        }


        public void RevertToPreviousAction()
        {
            Vector3 point;
            if (_positionDirection == -1)
            {
                point = PointA;
                _position = 0;
            }
            else if (_positionDirection == 1)
            {
                point = PointB;
                _position = 1;
            }
            else
            {
                throw new InvalidOperationException("Invalid direction");
            }
            _positionDirection *= -1;
            Anchor = point;
            _waitTimeLeft = 0;
        }


        public void Update(float deltaTime)
        {
            if (_pauseService.IsPaused || !Enabled)
            {
                return;
            }
            if (_waitTimeLeft > 0)
            {
                _waitTimeLeft -= deltaTime;
                return;
            }
            _position += _positionSpeed * _positionDirection * deltaTime;
            if (_position >= 1 || _position <= 0)
            {
                _waitTimeLeft = WaitTime;
                _positionDirection *= -1;
            }
            if (_position > 1)
            {
                _position = 1;
            }
            if (_position < 0)
            {
                _position = 0;
            }
            Anchor = Vector3.Lerp(PointA, PointB, _position);
        }
    }
}
