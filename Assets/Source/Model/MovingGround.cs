using Splatrika.LongLongStep.Architecture;
using UnityEngine;

namespace Splatrika.LongLongStep.Model
{
    public class MovingGround : IGround, IUpdatable
    {
        public bool Enabled { get; private set; }
        public Vector3 Anchor { get; private set; }
        public Vector3 PointA { get; }
        public Vector3 PointB { get; }
        public float Speed { get; }
        public float WaitTime { get; }

        private IPauseService _pauseService { get; }
        private float _waitTimeLeft;
        private float _position;
        private float _positionDirection;
        private float _positionSpeed;
        private float _pointsDistance;


        public MovingGround(
            IPauseService pauseService,
            MovingGroundConfiguration configuration)
        {
            _pauseService = pauseService;

            Enabled = true;
            Anchor = configuration.PointA;
            PointA = configuration.PointA;
            PointB = configuration.PointB;
            Speed = configuration.Speed;
            WaitTime = configuration.WaitTime;

            _position = 0;
            _positionDirection = 1;
            _pointsDistance = Vector3.Distance(PointA, PointB);
            _positionSpeed = Speed / _pointsDistance;
        }


        public void SetEnabled(bool value)
        {
            Enabled = value;
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
