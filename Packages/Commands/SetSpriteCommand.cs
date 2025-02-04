using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class SetSpritesCommand : ICommand
    {
        private Game1 _game1;
        private Game1.MCSpriteNames _name;
        public SetSpritesCommand(Game1 game, Game1.MCSpriteNames name)
        {
            _game1 = game;
            _name = name;
        }

        public void Execute()
        {
            _game1.SetSprite(_name);
        }
    }
}
