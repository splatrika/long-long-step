using Moq;
using NUnit.Framework;
using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep.Tests.UnitTests
{
    public class MovingGroundTests
    {
        private MovingGround _ground;
        private MovingGroundConfiguration _configuration;
        private Mock<IPauseService> _pauseServiceMock;
        private float _timeToMove;


        [SetUp]
        public void Init()
        {
            _pauseServiceMock = new Mock<IPauseService>();
            _configuration = new MovingGroundConfiguration(
                pointA: Vector3.right,
                pointB: Vector3.left,
                speed: 10,
                waitTime: 5);
            _ground = new MovingGround(_pauseServiceMock.Object,
                _configuration);

            _timeToMove =
                Vector3.Distance(_configuration.PointA, _configuration.PointB) /
                _configuration.Speed;

        }


        [Test]
        public void ShouldMove()
        {
            var lastDistance = Vector3.Distance(
                _ground.Anchor, _configuration.PointB);
            _ground.Update(_timeToMove / 2);
            var currentDistance = Vector3.Distance(
                _ground.Anchor, _configuration.PointB);
            Assert.Less(currentDistance, lastDistance);
            Assert.NotZero(currentDistance);
        }


        [Test]
        public void ShouldWaitAfterMove()
        {
            _ground.Update(_timeToMove);
            var lastAnchor = _ground.Anchor;
            _ground.Update(_configuration.WaitTime / 2);
            Assert.AreEqual(lastAnchor, _ground.Anchor);
        }


        [Test]
        public void ShouldBeAbleToBeDisabled()
        {
            _ground.SetEnabled(false);
            Assert.False(_ground.Enabled);
        }


        [Test]
        public void ShouldNotBeMovingWhenDisabled()
        {
            _ground.SetEnabled(false);
            AssertShouldNotBeMoving();
        }


        [Test]
        public void ShouldNotBeMovingWhenPaused()
        {
            Pause();
            AssertShouldNotBeMoving();
        }


        [Test]
        public void ShouldChangeDirectionAfterWait()
        {
            _ground.Update(_timeToMove);
            _ground.Update(_configuration.WaitTime);
            _ground.Update(_timeToMove);
            Assert.AreEqual(_configuration.PointA, _ground.Anchor);
        }


        private void AssertShouldNotBeMoving()
        {
            var lastAnchor = _ground.Anchor;
            _ground.Update(_timeToMove / 2);
            Assert.AreEqual(lastAnchor, _ground.Anchor);
        }


        private void Pause()
        {
            _pauseServiceMock.SetupGet(x => x.IsPaused)
                .Returns(true);
            _pauseServiceMock.Raise(x => x.Paused += null);
        }
    }
}
