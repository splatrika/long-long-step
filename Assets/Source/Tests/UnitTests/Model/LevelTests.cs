using Moq;
using NUnit.Framework;
using Splatrika.LongLongStep.Model;

namespace Splatrika.LongLongStep.Tests.UnitTests
{
    public class LevelTests
    {
        private Level _level;
        private Mock<IPlayerCharacter> _playerCharacterMock;
        private Mock<IObjectProviderService<IPlayerCharacter>> _providerMock;
        private ITimeService _timeService;


        [SetUp]
        public void Init()
        {
            _playerCharacterMock = new Mock<IPlayerCharacter>();
            _providerMock = new Mock<IObjectProviderService<IPlayerCharacter>>();
            _timeService = new Mock<ITimeService>().Object;
            _level = new Level(_providerMock.Object, _timeService);

            _providerMock.Raise(x => x.Ready += null,
                _playerCharacterMock.Object);
        }


        [Test]
        public void ShouldBeCompletedAfterPlayerHasTouchedGoalGround()
        {
            var goalGround = new Mock<IGoalGround>().Object;

            var completed = false;
            _level.Completed += () => completed = true;
            _playerCharacterMock
                .Raise(x => x.TouchedGround += null, goalGround);

            Assert.True(completed);
        }


        [Test]
        public void ShouldNotBeCompletedAfterPlayeHasTounchedRegularGround()
        {
            var regularGround = new Mock<IGround>().Object;

            var completed = false;
            _level.Completed += () => completed = true;
            _playerCharacterMock
                .Raise(x => x.TouchedGround += null, regularGround);

            Assert.False(completed);
        }


        [Test]
        public void ShouldBeFailedAfterPlayerHasDied()
        {
            var failed = false;
            _level.Failed += () => failed = true;
            _playerCharacterMock.Raise(x => x.Died += null);

            Assert.True(failed);
        }
    }
}
