using NUnit.Framework;
using System.Threading.Tasks;

namespace StateMachine.Tests
{
    [TestFixture]
    public class StateMachine_StringStates
    {
        private IStateMachine<string, string> _stateMachine;

        [SetUp]
        public void Setup()
        {
            _stateMachine = new StateMachine<string, string>("state_A");
            _stateMachine.In("state_A").On("A-B").GoTo("state_B");
            _stateMachine.In("state_B").On("B-C").GoTo("state_C");
        }

        [Test]
        public async Task Should_Change_State()
        {
            // Arrange

            // Act
            var result = await _stateMachine.Fire("A-B");

            // Assert
            Assert.IsTrue(result.StateChanged);
        }

        [Test]
        public async Task ShouldNot_Change_State()
        {
            // Arrange

            // Act
            var result = await _stateMachine.Fire("B-C");

            // Assert
            Assert.IsFalse(result.StateChanged);
        }

        [Test]
        public async Task Should_Fire_Twice()
        {
            // Arrange

            // Act
            var result1 = await _stateMachine.Fire("A-B");
            var result2 = await _stateMachine.Fire("B-C");

            // Assert
            Assert.IsTrue(result1.StateChanged);
            Assert.IsTrue(result2.StateChanged);
        }
    }
}