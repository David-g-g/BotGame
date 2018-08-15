using System;
using System.Collections.Generic;

namespace OpenTable.BotGame
{
    public interface IBotMovementHandlerFactory    
    {
        IBotMovementHandler CreateMovement(BotMovement moment);
    }

    public enum BotMovement
    {
        Rotate,
        Advance
    }

    public class BotMovementHandlerFactory : IBotMovementHandlerFactory
    {
        private Dictionary<BotMovement, Type> movements = new Dictionary<BotMovement, Type>{
            {BotMovement.Advance, typeof(AdvanceMovementHandler)},
            {BotMovement.Rotate, typeof(RotateMovementHandler)}
            };

        public IBotMovementHandler CreateMovement(BotMovement movement)
        {
            if (movements.ContainsKey(movement))
            {
                return (IBotMovementHandler)Activator.CreateInstance(movements[movement]);
            }

            throw new Exception($"Bot movement handler not registered movement:{movement}");
        }
    }
}