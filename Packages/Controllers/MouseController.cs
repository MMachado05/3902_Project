using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project;

namespace Project
{
    public class MouseController : IController

    {
        private Game1 _game;
        private GraphicsDeviceManager _graphics;
        private ICommand staticSprite;
        private ICommand animatedSprite;
        private ICommand movingStaticSprite;
        private ICommand movingAnimatedSprite;

        public MouseController(Game1 game, GraphicsDeviceManager graphics)
        {

            _game = game;
            _graphics = graphics;
            //staticSprite = new SetSpritesCommand(_game, Game1.LuigiSpriteNames.Static);
            //animatedSprite = new SetSpritesCommand(_game, Game1.LuigiSpriteNames.Animated);
            //movingStaticSprite = new SetSpritesCommand(_game, Game1.LuigiSpriteNames.MovingStatic);
            //movingAnimatedSprite = new SetSpritesCommand(_game, Game1.LuigiSpriteNames.MovingAnimated);

        }

        public void Update()
        {
            MouseState state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed)
            {
                // Quad 1
                if (state.X < (_graphics.PreferredBackBufferWidth / 2) && state.Y < (_graphics.PreferredBackBufferHeight / 2))
                {
                    staticSprite.Execute();
                }
                // Quad 2
                else if (state.X > (_graphics.PreferredBackBufferWidth / 2) && state.Y < (_graphics.PreferredBackBufferHeight / 2))
                {
                    animatedSprite.Execute();
                }
                // Quad 3
                else if (state.X < (_graphics.PreferredBackBufferWidth / 2) && state.Y > (_graphics.PreferredBackBufferHeight / 2))
                {
                    movingStaticSprite.Execute();

                }
                // Quad 4
                else if (state.X > (_graphics.PreferredBackBufferWidth / 2) && state.Y > (_graphics.PreferredBackBufferHeight / 2))
                {
                    movingAnimatedSprite.Execute();
                }

            }

            if (state.RightButton == ButtonState.Pressed)
            {
                //new QuitCommand(_game).Execute();
            }
        }
    }
}
