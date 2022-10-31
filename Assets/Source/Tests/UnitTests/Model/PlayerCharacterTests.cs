using Moq;
using NUnit.Framework;
using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep.Tests.UnitTests
{
    public class LongStepCharacterTests
    {
        private PlayerCharacterConfiguration _configuration;
        private PlayerCharacter _character;
        private Mock<IPhysicsService> _physicsServiceMock;
        private Mock<IPauseService> _pauseSerivceMock;
        private Mock<IGround> _groundMock;
        private IRegisterObjectService<IPlayerCharacter> _registerService;
        private ILogger _fakeLogger;


        [SetUp]
        public void Init()
        {
            _configuration = new PlayerCharacterConfiguration
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
            _pauseSerivceMock = new Mock<IPauseService>();
            _groundMock = new Mock<IGround>();
            _registerService =
                new Mock<IRegisterObjectService<IPlayerCharacter>>().Object;

            _character = new PlayerCharacter(
                _fakeLogger, _pauseSerivceMock.Object,
                _physicsServiceMock.Object, _registerService,
                _configuration);
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
            var fakeGround = _groundMock.Object;
            _physicsServiceMock.Setup(
                x => x.HasGround(It.IsAny<Vector3>(), out fakeGround))
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
            var fakeGround = _groundMock.Object;
            _physicsServiceMock.Setup(
                x => x.HasGround(It.IsAny<Vector3>(), out fakeGround))
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


        [Test]
        public void ShouldNotWaitWhenPaused()
        {
            Pause();
            var stepStarted = false;
            _character.StepStarted += () => stepStarted = true;
            WaitForStep(_character);
            Assert.False(stepStarted);
        }


        [Test]
        public void ShouldNotRotateWhenStepStartedAndPaused()
        {
            WaitForStep(_character);
            Pause();
            var lastTarget = _character.StepTarget;
            _character.StartRotation(1);
            _character.Update(_configuration.StepDuration / 2);
            Assert.AreEqual(lastTarget, _character.StepTarget);
        }


        [Test]
        public void ShouldNotStepWhenPaused()
        {
            WaitForStep(_character);
            Pause();
            var falled = false;
            var wait = false;
            _character.Falled += () => falled = true;
            _character.Wait += () => wait = true;
            _character.Update(_configuration.StepDuration);
            Assert.False(falled);
            Assert.False(wait);
        }


        [Test]
        public void ShouldNotFallWhenPaused()
        {
            WaitForFall(_character);
            Pause();
            var stepStarted = false;
            _character.StepStarted += () => stepStarted = true;
            _character.Update(_configuration.FallTime);
            Assert.False(stepStarted);
        }



        [Test]
        public void ShouldTouchGround()
        {
            WaitForStep(_character);
            var fakeGround = _groundMock.Object;
            _physicsServiceMock.Setup(
                x => x.HasGround(It.IsAny<Vector3>(), out fakeGround))
                .Returns(true);
            IGround touched = null;
            _character.TouchedGround += ground => touched = ground;
            _character.Update(_configuration.StepDuration);
            Assert.AreEqual(fakeGround, touched);
        }


        [Test]
        public void ShouldMagnetToTheGroundAnchorAfterStep()
        {
            _groundMock.SetupGet(x => x.Anchor)
                .Returns(new Vector3(1, 2, -2));
            var fakeGround = _groundMock.Object;
            _physicsServiceMock.Setup(
                x => x.HasGround(It.IsAny<Vector3>(), out fakeGround))
                .Returns(true);
            WaitForStep(_character);
            _character.Update(_configuration.StepDuration);
            Assert.AreEqual(fakeGround.Anchor, _character.Position);
        }


        [Test]
        public void ShouldBeHappyAfterTouchGoalGround()
        {
            SetupGoalGround();

            WaitForStep(_character);
            var happy = false;
            _character.Happy += () => happy = true;
            _character.Update(_configuration.StepDuration);
            _character.Update(_configuration.WaitTime);

            Assert.True(happy);
        }


        [Test]
        public void ShouldNotStartStepAfterTouchGoalGround()
        {
            SetupGoalGround();

            WaitForStep(_character);
            var stepStarted = false;
            _character.StepStarted += () => stepStarted = true;
            _character.Update(_configuration.StepDuration);
            _character.Update(_configuration.WaitTime);
            Assert.False(stepStarted);
        }


        private void WaitForStep(PlayerCharacter character)
        {
            character.Update(_configuration.WaitTime);
        }


        private void WaitForFall(PlayerCharacter character)
        {
            var fakeGround = _groundMock.Object;
            _physicsServiceMock.Setup(
                x => x.HasGround(It.IsAny<Vector3>(), out fakeGround))
                .Returns(false);
            character.Update(_configuration.WaitTime);
            character.Update(_configuration.StepDuration);
        }


        private bool TryCreateDied()
        {
            _configuration.Lifes = 1;
            _character = new PlayerCharacter(
                _fakeLogger, _pauseSerivceMock.Object,
                _physicsServiceMock.Object, _registerService,
                _configuration);
            var died = false;
            _character.Died += () => died = true;
            WaitForFall(_character);
            _character.Update(_configuration.FallTime);
            return died;
        }


        private void Pause()
        {
            _pauseSerivceMock.SetupGet(x => x.IsPaused)
                .Returns(true);
            _pauseSerivceMock.Raise(x => x.Paused += null);
        }


        private void SetupGoalGround()
        {
            var fakeGround = new Mock<IGoalGround>().Object as IGround;
            _physicsServiceMock.Setup(
                x => x.HasGround(It.IsAny<Vector3>(), out fakeGround))
                .Returns(true);
        }
    }
}
