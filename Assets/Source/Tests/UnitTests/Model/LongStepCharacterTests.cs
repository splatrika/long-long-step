using Moq;
using NUnit.Framework;
using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep.Tests.UnitTests
{
    public class LongStepCharacterTests
    {
        private LongStepCharacterConfiguration _configuration;
        private LongStepCharacter _character;
        private Mock<IPhysicsService> _physicsServiceMock;
        private ILogger _fakeLogger;


        [SetUp]
        public void Init()
        {
            _configuration = new LongStepCharacterConfiguration
            {
                StepDuration = 5,
                RotationSpeed = 1,
                WaitTime = 3,
                FallTime = 4,
                StepLength = 1.5f,
                Lifes = 3
            };
            _physicsServiceMock = new Mock<IPhysicsService>();
            _fakeLogger = new Mock<ILogger>().Object;
            _character = new LongStepCharacter(
                _fakeLogger, _physicsServiceMock.Object, _configuration);
        }


        [Test]
        public void ShouldStartStepAfterWait()
        {
            var stepStarted = false;
            _character.StepStarted += () => stepStarted = true;
            _character.Update(_configuration.WaitTime);
            Assert.True(stepStarted);
        }


        [Test]
        public void ShouldWaitAfterStepIfHasGround()
        {
            WaitForStep(_character);
            var waitStarted = false;
            _character.Wait += () => waitStarted = true;
            _physicsServiceMock.Setup(x => x.HasGround(It.IsAny<Vector3>()))
                .Returns(true);
            _character.Update(_configuration.StepDuration);
            Assert.True(waitStarted);
        }


        [Test]
        public void ShouldFallAfterStepIfHasNotGround()
        {
            WaitForStep(_character);
            var falled = false;
            _character.Falled += () => falled = true;
            _physicsServiceMock.Setup(x => x.HasGround(It.IsAny<Vector3>()))
                .Returns(false);
            _character.Update(_configuration.StepDuration);
            Assert.True(falled);
        }


        [Test]
        public void ShouldStartStepAfterFallIfHasLifes()
        {
            WaitForFall(_character);
            var stepStarted = false;
            _character.StepStarted += () => stepStarted = true;
            _character.Update(_configuration.FallTime);
            Assert.True(stepStarted);
        }


        [Test]
        public void ShouldDieAfterFallIfHasNotMoreLifes()
        {
            bool died = TryCreateDied();
            Assert.True(died);
        }


        [Test]
        public void ShouldReciveDamageAfterFall()
        {
            WaitForFall(_character);
            var damaged = false;
            _character.Damaged += () => damaged = true;
            _character.Update(_configuration.FallTime);
            Assert.True(damaged);
            Assert.AreEqual(_configuration.Lifes - 1, _character.Lifes);
        }


        [Test]
        public void ShouldHasStepTargetOnStep()
        {
            WaitForStep(_character);
            Assert.NotNull(_character.StepTarget);
        }


        [Test]
        public void ShouldHasStepWithSpecifyiedStepLength()
        {
            WaitForStep(_character);
            var stepMove = (Vector3)_character.StepTarget - _character.Position;
            Assert.AreEqual(_configuration.StepLength, stepMove.magnitude,
                0.01f);
        }


        [Test]
        public void ShouldNotHasStepTargetWhenIsWaiting()
        {
            Assert.Null(_character.StepTarget);
        }


        [Test]
        public void ShouldNotHasStepTargetWhenIsFalling()
        {
            WaitForFall(_character);
            Assert.Null(_character.StepTarget);
        }


        [Test]
        public void ShouldNotHasStepTargetWhenIsDied()
        {
            TryCreateDied();
            Assert.Null(_character.StepTarget);
        }


        [Test]
        public void ShouldMoveAfterStep()
        {
            WaitForStep(_character);
            _physicsServiceMock.Setup(x => x.HasGround(It.IsAny<Vector3>()))
                .Returns(true);
            var stepTarget = _character.StepTarget;
            Vector3? newPosition = null;
            _character.Moved += position => newPosition = position;
            _character.Update(_configuration.StepDuration);
            Assert.AreEqual(stepTarget, _character.Position);
            Assert.AreEqual(_character.Position, newPosition);
        }


        [Test]
        public void ShouldRotateWhenIsStepping()
        {
            WaitForStep(_character);
            var lastTarget = _character.StepTarget;
            _character.StartRotation(1);
            _character.Update(_configuration.StepDuration / 2);
            Assert.AreNotEqual(lastTarget, _character.StepTarget);
        }


        [Test]
        public void ShouldNotRotateWhenIsWaiting()
        {
            _character.StartRotation(1);
            _character.Update(1);
            Assert.Null(_character.StepTarget);
        }


        [Test]
        public void ShouldNotRotateWhenIsFalling()
        {
            WaitForFall(_character);
            _character.StartRotation(1);
            _character.Update(1);
            Assert.Null(_character.StepTarget);
        }


        [Test]
        public void ShouldNotRotateWhenIsDied()
        {
            TryCreateDied();
            _character.StartRotation(1);
            _character.Update(1);
            Assert.Null(_character.StepTarget);
        }


        private void WaitForStep(LongStepCharacter character)
        {
            character.Update(_configuration.WaitTime);
        }


        private void WaitForFall(LongStepCharacter character)
        {
            _physicsServiceMock.Setup(x => x.HasGround(It.IsAny<Vector3>()))
                .Returns(false);
            character.Update(_configuration.WaitTime);
            character.Update(_configuration.StepDuration);
        }


        private bool TryCreateDied()
        {
            _configuration.Lifes = 1;
            _character = new LongStepCharacter(
                _fakeLogger, _physicsServiceMock.Object, _configuration);
            var died = false;
            _character.Died += () => died = true;
            WaitForFall(_character);
            _character.Update(_configuration.FallTime);
            return died;
        }
    }
}
