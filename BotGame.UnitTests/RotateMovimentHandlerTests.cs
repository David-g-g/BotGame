using System;
using FluentAssertions;
using OpenTable.BotGame;
using Xunit;

namespace BotGame.UnitTests
{
    public class RotateMovimentHandlerTests
    {
        [Theory]
        [InlineData("N", "W")]
        [InlineData("W", "S")]
        [InlineData("S", "E")]
        [InlineData("E", "N")]
        public void WhenRotateLeft_ThenBotRotates90DegreesLeft(string originalDirection, string expectedDirection)
        {
            var rotationHandler = new RotateMovementHandler();
            var currenPosition = new Position(new Coordinates(), (CardinalPoint)Enum.Parse(typeof(CardinalPoint), originalDirection));

            var newPosition = rotationHandler.HandleMovement(currenPosition, Movement.L);

            newPosition.Direction.Should().Be((CardinalPoint)Enum.Parse(typeof(CardinalPoint), expectedDirection));
        }

        [Theory]
        [InlineData("N", "E")]
        [InlineData("E", "S")]
        [InlineData("S", "W")]
        [InlineData("W", "N")]
        public void WhenRotateRight_ThenBotRotates90DegreesRight(string originalDirection, string expectedDirection)
        {
            var rotationHandler = new RotateMovementHandler();
            var currenPosition = new Position(new Coordinates(), (CardinalPoint)Enum.Parse(typeof(CardinalPoint), originalDirection));

            var newPosition = rotationHandler.HandleMovement(currenPosition, Movement.R);

            newPosition.Direction.Should().Be((CardinalPoint)Enum.Parse(typeof(CardinalPoint), expectedDirection));
        }

        [Fact]
        public void WhenRotateIsCalled_ThenOriginalCoordinatesAreReturned()
        {
            var originalCoordinates = new Coordinates { X = 10, Y = 20 };
            var rotationHandler = new RotateMovementHandler();
            var currenPosition = new Position(originalCoordinates, CardinalPoint.N);

            var newPosition = rotationHandler.HandleMovement(currenPosition, Movement.L);

            newPosition.Coordinates.ShouldBeEquivalentTo(originalCoordinates);
        }

        [Theory]
        [InlineData("L", true)]
        [InlineData("R", true)]
        [InlineData("M", false)]
        public void GivenMovement_WhenCanHandleIsCalled_ThenExpectedResultIsReturned(string movement, bool expectedResult)
        {
            var rotationHandler = new RotateMovementHandler();
            var parsedMovement = (Movement)Enum.Parse(typeof(Movement), movement);

            var response = rotationHandler.CanHandleMovement(parsedMovement);

            response.Should().Be(expectedResult);
        }
    }
}
