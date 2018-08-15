using System;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using OpenTable;
using OpenTable.BotGame;
using Xunit;

namespace BotGame.UnitTests
{
    public class BotGameTests
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(0,-1)]
        [InlineData(-1,0)]
        [InlineData(-1,-1)]
        public void GivenInvalidBoardSize_WhenGameIsCreated_ThenExceptionIsRaised(int x, int y)
        {
            Action action = ()=> new OpenTable.BotGame.Game(x, y, null);

            action.ShouldThrow<BotGameException>();
        }

        [Fact]
        public void WhenCreateNexBotIsCalled_ThenBotIsRegistered()
        {
            var rows = 10;
            var columns = 10;
            var botX = 5;
            var botY = 3;
            var botDirection = CardinalPoint.S;
            var bot = Substitute.For<IBot>();
            var botPosition = new Position(new Coordinates{X=botX, Y=botY},botDirection);
            bot.Position.Returns(botPosition);
            var botFactory = Substitute.For<IBotFactory>();
            botFactory.CreateRegularBot(Arg.Is<Coordinates>(c=>c.X==botX && c.Y==botY), botDirection, Arg.Any<IGameBoard>()).Returns(bot);

            var game = new Game(rows, columns, botFactory);
            
            game.CreateNexBot(new Coordinates{X=botX,Y=botY},botDirection);

            game.GetBotPositions().First().ShouldBeEquivalentTo(botPosition);
        }

        [Fact]
        public void GivenBot_WhenMoveIsCalled_ThenBotMoveIsCalled()
        {
            var inputMovement = Movement.L;
            var rows = 10;
            var columns = 10;

            var botPosition = new Position(new Coordinates{X=10, Y=5},CardinalPoint.E);
            var bot = Substitute.For<IBot>();
            bot.Position.Returns(botPosition);

            var botFactory = Substitute.For<IBotFactory>();
            botFactory.CreateRegularBot(Arg.Is<Coordinates>(c=>c.X==botPosition.Coordinates.X && c.Y==botPosition.Coordinates.Y), botPosition.Direction, Arg.Any<IGameBoard>()).Returns(bot);

            var game = new Game(rows, columns, botFactory);
            
            game.CreateNexBot(new Coordinates{X=botPosition.Coordinates.X,Y=botPosition.Coordinates.Y},botPosition.Direction);

            game.MoveBot(inputMovement);

            bot.Received().Move(inputMovement);
        }

        [Fact]
        public void GivenManuBots_WhenMoveIsCalled_ThenOnlyLastBotIsAffected()
        {
            var inputMovement = Movement.L;
            var rows = 10;
            var columns = 10;

            var bot1Position = new Position(new Coordinates{X=10, Y=5},CardinalPoint.E);
            var bot2Position = new Position(new Coordinates{X=5, Y=3},CardinalPoint.S);

            var bot1 = Substitute.For<IBot>();
            bot1.Position.Returns(bot1Position);

            var bot2 = Substitute.For<IBot>();
            bot2.Position.Returns(bot2Position);
            
            var botFactory = Substitute.For<IBotFactory>();
            botFactory.CreateRegularBot(Arg.Is<Coordinates>(c=>c.X==bot1Position.Coordinates.X && c.Y==bot1Position.Coordinates.Y), bot1Position.Direction, Arg.Any<IGameBoard>()).Returns(bot1);
            botFactory.CreateRegularBot(Arg.Is<Coordinates>(c=>c.X==bot2Position.Coordinates.X && c.Y==bot2Position.Coordinates.Y), bot2Position.Direction, Arg.Any<IGameBoard>()).Returns(bot2);

            var game = new Game(rows, columns, botFactory);
            
            game.CreateNexBot(bot1Position.Coordinates, bot1Position.Direction);
            game.CreateNexBot(bot2Position.Coordinates, bot2Position.Direction);

            game.MoveBot(inputMovement);

            bot1.DidNotReceive().Move(inputMovement);
            bot2.Received().Move(inputMovement);
        }

    }
}