using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project.rooms;

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
        private ICommand nextRoom;
        private RoomManager roomManager;
        private ButtonState _previouslyPressed;
        private bool isDuplicate;

        public MouseController(Game1 game, GraphicsDeviceManager graphics, RoomManager roomManager)
        {
            this.roomManager = roomManager;
            _graphics = graphics;
            this.isDuplicate = false;
        }

        public void Update()
        {
            MouseState state = Mouse.GetState();
            ButtonState currentlyPressed = state.LeftButton;

            if (state.LeftButton == ButtonState.Pressed)
            {
                // Quad 1
                if (state.X < (_graphics.PreferredBackBufferWidth / 2) && state.Y < (_graphics.PreferredBackBufferHeight / 2) && !isDuplicate)
                {
                    nextRoom.Execute();
                    isDuplicate = true;

                }

            }
            else if (state.LeftButton == ButtonState.Released)
                isDuplicate = false;
        }
    }
}
