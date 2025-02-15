using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Project.Blocks
{
    public class SolidBlockController : IController
    {
        private NextBlockCommand NextBlock;
        private PreviousBlockCommand PreviousBlock;

       // private KeyboardState input;
        bool up;
        Game1 _game;
         public SolidBlockController(Game1 game, NextBlockCommand nextBlock)
        {
            this.up = false;
            NextBlock = nextBlock;
            //PreviousBlock = previousBlock;
            _game = game;
        }

        public void ProcessControls()
        {
              if((_game.input.IsKeyDown(Keys.NumPad2)|| _game.input.IsKeyDown(Keys.D2))&& !_game.input.Equals(_game.previous)){
               NextBlock.Execute();
                }
        }
    }
}