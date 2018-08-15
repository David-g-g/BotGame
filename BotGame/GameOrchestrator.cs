using System;
using BotGame;

namespace OpenTable.BotGame
{
    public class GameOrchestrator
    {
        private IInputStream inputStream;
        public IOutputStream outputStream;

        public GameOrchestrator(IInputStream inputStream, IOutputStream outputStream)
        {
            this.inputStream = inputStream;
            this.outputStream = outputStream;
        }

        public void Start()
        {
            try
            {

                var game = StartGame();

                if (game == null)
                {
                    return;
                }

                while (true)
                {

                    var success = CreateBot(game);

                    if (!success)
                    {
                        break;
                    }

                    success = MoveBot(game);

                    if (!success)
                    {
                        break;
                    }
                }

                PrintBotsPosition(game);
            }
            catch (BotGameException e)
            {
                outputStream.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                outputStream.WriteLine(e.ToString());
            }
        }

        private bool CreateBot(Game game)
        {

            var botPositionRaw = GetInputData("Input bot position (top-right coordenate and direction: x y direction) or press enter to exit");

            if (botPositionRaw == "")
            {
                return false;
            }

            var botPosition = CommandParser.ParseBotPosition(botPositionRaw);

            game.CreateNexBot(botPosition.Coordinates, botPosition.Direction);

            return true;
        }
        private Game StartGame()
        {

            var boardSizeRaw = GetInputData("Input board size (top right coordenate: x y) or press enter to exit");

            if (boardSizeRaw == "")
            {
                return null;
            }

            var boardSize = CommandParser.ParseBoardSize(boardSizeRaw);

            var game = new Game(boardSize.X, boardSize.Y, new BotFactory(new BotMovementHandlerFactory()));

            return game;
        }

        private bool MoveBot(Game game)
        {
            var botMovementsRaw = GetInputData("Input bot movements (L(eft) R(ight) M(ove) (LMRMMLMRRM) or press enter to exit");

            if (botMovementsRaw == "")
            {
                return false;
            }

            var botMovements = CommandParser.ParseMovements(botMovementsRaw);

            foreach (var movement in botMovements)
            {
                game.MoveBot(movement);
            }

            return true;
        }

        private void PrintBotsPosition(Game game)
        {
            foreach (var position in game.GetBotPositions())
            {
                this.outputStream.WriteLine($"Position x:{position.Coordinates.X} y:{position.Coordinates.Y} direction:{position.Direction}");
            }
        }
        private string GetInputData(string message)
        {
            this.outputStream.WriteLine(message);
            return this.inputStream.ReadLine();
        }
    }
}