using System;
using FluentAssertions;
using OpenTable.BotGame;
using Xunit;

namespace BotGame.UnitTests
{
    public class AdvanceMovimentHandlerTests
    {

        [Fact]
        public void GivenNorthDirection_WhenAdvanceIsCalled_ThenRobotGoesUp()
        {            
            var advancehandler = new AdvanceMovementHandler();
            var currenPosition =  new Position(new Coordinates{Y=10, X=20}, CardinalPoint.N);

            var newPosition = advancehandler.HandleMovement(currenPosition, Movement.M);

            newPosition.Coordinates.ShouldBeEquivalentTo(new Coordinates{Y=11, X=20});
        }

        [Fact]
        public void GivenSouthDirection_WhenAdvanceIsCalled_ThenRobotGoesDown()
        {            
            var advancehandler = new AdvanceMovementHandler();
            var currenPosition =  new Position(new Coordinates{Y=10, X=20}, CardinalPoint.S);

            var newPosition = advancehandler.HandleMovement(currenPosition, Movement.M);

            newPosition.Coordinates.ShouldBeEquivalentTo(new Coordinates{Y=9, X=20});
        }

        [Fact]
        public void GivenEastDirection_WhenAdvanceIsCalled_ThenRobotGoesRight()
        {            
            var advancehandler = new AdvanceMovementHandler();
            var currenPosition =  new Position(new Coordinates{Y=10, X=20}, CardinalPoint.E);

            var newPosition = advancehandler.HandleMovement(currenPosition, Movement.M);

            newPosition.Coordinates.ShouldBeEquivalentTo(new Coordinates{Y=10, X=21});
        }

        [Fact]
        public void GivenWestDirection_WhenAdvanceIsCalled_ThenRobotGoesLeft()
        {            
            var advancehandler = new AdvanceMovementHandler();
            var currenPosition =  new Position(new Coordinates{Y=10, X=20}, CardinalPoint.W);

            var newPosition = advancehandler.HandleMovement(currenPosition, Movement.M);

            newPosition.Coordinates.ShouldBeEquivalentTo(new Coordinates{Y=10, X=19});
        }

        [Theory]
        [InlineData("L", false)]
        [InlineData("R", false)]
        [InlineData("M", true)]
        public void WhenCanHandleIsCalled_ThenExpectedResultIsReturned (string movement, bool expectedResult)
        {
            var advanceHandler = new AdvanceMovementHandler();
            var parsedMovement =  (Movement)Enum.Parse(typeof(Movement),movement);

            var response = advanceHandler.CanHandleMovement(parsedMovement);

            response.Should().Be(expectedResult);
        }

        [Fact]
        public void WhenAdvanceIsCalled_ThenOriginalDirectionIsReturned()
        {
            var originalDirection = CardinalPoint.S;
            var advanceHandler = new AdvanceMovementHandler();
            var currenPosition =  new Position(new Coordinates{X=5,Y=3}, originalDirection);

            var newPosition = advanceHandler.HandleMovement(currenPosition, Movement.M);

            newPosition.Direction.ShouldBeEquivalentTo(originalDirection);
        }
    }
}
