using System;
using Xunit;
using NSubstitute;
using OpenTable.BotGame;

namespace BotGame.IntegrationTests
{
    public class BotGameTests
    {
        [Fact]
        public void GivenTwoBots_WhenGameIsPlayed_BotsFinishInExpectedPositions()
        {
            var inputStream = Substitute.For<IInputStream>();
            var outpuStream = Substitute.For<IOutputStream>();
            var gameOrchestrator = new GameOrchestrator(inputStream, outpuStream);

            inputStream.ReadLine().Returns("5 5",
                                           "1 2 N",                                            
                                            "LMLMLMLMM",
                                            "3 3 E ",
                                            "MMRMMRMRRM",
                                            "");

            gameOrchestrator.Start();

            outpuStream.Received().WriteLine(Arg.Is<string>("Position x:1 y:3 direction:N"));
            outpuStream.Received().WriteLine(Arg.Is<string>("Position x:5 y:1 direction:E"));
        }
    }
}
