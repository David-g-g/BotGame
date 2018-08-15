using Xunit;
using System;
using OpenTable.BotGame;
using NSubstitute;
using FluentAssertions;

namespace BotGame.UnitTests
{
    public class RegularBotTests
    {
        [Theory]
        [InlineData(11, 10)]
        [InlineData(-1, 10)]
        [InlineData(10, 11)]
        [InlineData(10, -1)]
        public void GivenCoordinateOutOfBoard_WhenBotIsCreated_ThenExceptionIsRaised(int x, int y)
        {
            var movementFactory = NSubstitute.Substitute.For<IBotMovementHandlerFactory>();
            movementFactory.CreateMovement(Arg.Any<BotMovement>()).Returns((IBotMovementHandler)null);
            var board = NSubstitute.Substitute.For<IGameBoard>();
            board.Columns.Returns(10);
            board.Rows.Returns(10);

            var coordinates = new Coordinates{X=x, Y=y};

            Action action = ()=> {new RegularBot(coordinates, CardinalPoint.N, board, movementFactory);};

            action.ShouldThrow<BotGameException>();          
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(10, 10)]
        [InlineData(5, 0)]
        [InlineData(0, 5)]
        public void GivenValidCoordinates_WhenBotIsCreated_ThenExceptionShouldNotBeRaised(int x, int y)
        {
            var movementFactory = NSubstitute.Substitute.For<IBotMovementHandlerFactory>();
            movementFactory.CreateMovement(Arg.Any<BotMovement>()).Returns((IBotMovementHandler)null);
            var board = NSubstitute.Substitute.For<IGameBoard>();
            board.Columns.Returns(10);
            board.Rows.Returns(10);

            var coordinates = new Coordinates{X=x, Y=y};

            Action action = ()=>new RegularBot(coordinates, CardinalPoint.N, board, movementFactory);

            action.ShouldNotThrow<BotGameException>();            
        }

        [Fact]
        public void BotHasRotateMoviment()
        {
            var movementFactory = NSubstitute.Substitute.For<IBotMovementHandlerFactory>();
            movementFactory.CreateMovement(Arg.Any<BotMovement>()).Returns((IBotMovementHandler)null);
            var board = NSubstitute.Substitute.For<IGameBoard>();
            board.Columns.Returns(10);
            board.Rows.Returns(10);

            var coordinates = new Coordinates{X=10, Y=10};

            var bot = new RegularBot(coordinates, CardinalPoint.N, board, movementFactory);

            movementFactory.Received().CreateMovement(Arg.Is<BotMovement>(BotMovement.Rotate));
        }

        [Fact]
        public void BotHasAdvanceMoviment()
        {
            var movementFactory = NSubstitute.Substitute.For<IBotMovementHandlerFactory>();
            movementFactory.CreateMovement(Arg.Any<BotMovement>()).Returns((IBotMovementHandler)null);
            var board = NSubstitute.Substitute.For<IGameBoard>();
            board.Columns.Returns(10);
            board.Rows.Returns(10);

            var coordinates = new Coordinates{X=10, Y=10};

            var bot = new RegularBot(coordinates, CardinalPoint.N, board, movementFactory);

            movementFactory.Received().CreateMovement(Arg.Is<BotMovement>(BotMovement.Advance));
        }

        [Fact]
        public void GivenMovementAndBotThatCanNotHandleThatMovement_WhenMoveIsCalled_HandleIsNotCalled()
        {
            var movementFactory = NSubstitute.Substitute.For<IBotMovementHandlerFactory>();
            var movement = Substitute.For<IBotMovementHandler>();
            movementFactory.CreateMovement(Arg.Any<BotMovement>()).Returns(movement);
            var board = NSubstitute.Substitute.For<IGameBoard>();
            board.Columns.Returns(10);
            board.Rows.Returns(10);
            var coordinates = new Coordinates{X=5, Y=5};

            movement.CanHandleMovement(Arg.Any<Movement>()).Returns(false);

            var bot = new RegularBot(coordinates, CardinalPoint.N, board, movementFactory);

            bot.Move(Movement.L);

            movement.DidNotReceive().HandleMovement(Arg.Any<Position>(),Movement.L);
        }

        [Fact]
        public void GivenMovementAndBotThatCHandleThatMovement_WhenMoveIsCalled_HandleIsCalled()
        {
            var inputMoviment = Movement.L;
            var movementFactory = NSubstitute.Substitute.For<IBotMovementHandlerFactory>();
            var botMovement = Substitute.For<IBotMovementHandler>();
            movementFactory.CreateMovement(Arg.Any<BotMovement>()).Returns(botMovement);
            
            var board = NSubstitute.Substitute.For<IGameBoard>();
            board.Columns.Returns(10);
            board.Rows.Returns(10);
            
            var coordinates = new Coordinates{X=5, Y=5};

            botMovement.CanHandleMovement(Arg.Any<Movement>()).Returns(true);

            var bot = new RegularBot(coordinates, CardinalPoint.N, board, movementFactory);

            bot.Move(inputMoviment);

            botMovement.Received().HandleMovement(Arg.Is<Position>(p=>p.Coordinates.X==coordinates.X && 
                                                                    p.Coordinates.Y==coordinates.Y && 
                                                                    p.Direction==CardinalPoint.N),inputMoviment);
        }

        [Theory]
        [InlineData(11, 10)]
        [InlineData(-1, 10)]
        [InlineData(10, 11)]
        [InlineData(10, -1)]
        public void GivenMovementToOutsideTheBoardGame_WhenMoveIsCalled_ThenExceptionIsRaised(int x, int y){

            var inputMoviment = Movement.L;
            var movementFactory = NSubstitute.Substitute.For<IBotMovementHandlerFactory>();
            var botMovement = Substitute.For<IBotMovementHandler>();
            botMovement.CanHandleMovement(Arg.Any<Movement>()).Returns(true);
            botMovement.HandleMovement(Arg.Any<Position>(),inputMoviment).Returns(new Position(new Coordinates{X=x, Y=y},CardinalPoint.N));
            movementFactory.CreateMovement(Arg.Any<BotMovement>()).Returns(botMovement);
            
            var board = NSubstitute.Substitute.For<IGameBoard>();
            board.Columns.Returns(10);
            board.Rows.Returns(10);
            
            var coordinates = new Coordinates{X=5, Y=5};

            var bot = new RegularBot(coordinates, CardinalPoint.N, board, movementFactory);

            Action action = () => bot.Move(inputMoviment); 

            action.ShouldThrow<BotGameException>();            
        }

        [Theory]
        [InlineData(2, 2,CardinalPoint.N)]
        [InlineData(0, 0,CardinalPoint.S)]
        [InlineData(1, 10,CardinalPoint.E)]
        public void GivenValidMovement_WhenMoveIsCalled_ThenBotPositionIsUpdated(int x, int y, CardinalPoint cardinalPoint){

            var inputMoviment = Movement.L;
            var movementFactory = NSubstitute.Substitute.For<IBotMovementHandlerFactory>();
            var botMovement = Substitute.For<IBotMovementHandler>();
            botMovement.CanHandleMovement(Arg.Any<Movement>()).Returns(true);
            botMovement.HandleMovement(Arg.Any<Position>(),inputMoviment).Returns(new Position(new Coordinates{X=x, Y=y},cardinalPoint));
            movementFactory.CreateMovement(Arg.Any<BotMovement>()).Returns(botMovement);
            
            var board = NSubstitute.Substitute.For<IGameBoard>();
            board.Columns.Returns(10);
            board.Rows.Returns(10);
            
            var coordinates = new Coordinates{X=5, Y=5};

            var bot = new RegularBot(coordinates, CardinalPoint.N, board, movementFactory);
            bot.Move(inputMoviment);

            var expectedPosition = new Position(new Coordinates{X=x, Y=y},cardinalPoint);
            bot.Position.ShouldBeEquivalentTo(expectedPosition);
        }
    }
}