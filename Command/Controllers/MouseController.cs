using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project
{
    public class MouseController : IController // somewhat bare for now

    {
        private Game1 _game;
        private GraphicsDeviceManager _graphics;

        public MouseController(Game1 game, GraphicsDeviceManager graphics)
        {

            _game = game;
            _graphics = graphics;

        }

        public void Update()
        {
            MouseState state = Mouse.GetState();

        }
    }
}
