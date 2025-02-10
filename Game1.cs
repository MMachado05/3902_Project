using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project
{


    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardController _keyboardController;

        private ISprite playerSprite;
        private Rectangle playerPosition;

        private string lastDirection = "Down"; // Default direction set to "down" for now.
        private bool isMoving = false; // Tracks if player is currently moving; used to set static animation

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            playerPosition = new Rectangle(100, 100, 30, 30); // Initial character position

            base.Initialize(); // Does this go first or last????
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load all textures
            SpriteFactory.Instance.LoadAllTextures(Content);

            // Set initial sprite to static down
            playerSprite = SpriteFactory.Instance.NewDownStoppedPlayer();

            // Initialize KeyboardController with movement commands
            _keyboardController = new KeyboardController(this);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Check if player has stopped moving
            KeyboardState state = Keyboard.GetState();
            if (!(state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.D)))
            {
                SetStaticSprite(); // Set idle sprite; moved to another function for clarity
            }
            else
            {
                isMoving = true;
            }


            _keyboardController.Update();
            playerSprite.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            playerSprite.Draw(_spriteBatch, new Vector2(playerPosition.X, playerPosition.Y));
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ChangePlayerSprite(ISprite newSprite)
        {
            playerSprite = newSprite;
        }

        public void MovePlayer(int dx, int dy, string direction)
        {
            playerPosition = new Rectangle(playerPosition.X + dx, playerPosition.Y + dy, playerPosition.Width, playerPosition.Height);
            lastDirection = direction; // Store last movement direction, used to setting the attack animation
        }

        public void Attack()
        {
            switch (lastDirection)
            {
                case "Up":
                    ChangePlayerSprite(SpriteFactory.Instance.NewUpAttackingPlayer());
                    break;
                case "Down":
                    ChangePlayerSprite(SpriteFactory.Instance.NewDownAttackingPlayer());
                    break;
                case "Left":
                    ChangePlayerSprite(SpriteFactory.Instance.NewLeftAttackingPlayer());
                    break;
                case "Right":
                    ChangePlayerSprite(SpriteFactory.Instance.NewRightAttackingPlayer());
                    break;
            }
        }

        private void SetStaticSprite()
        {
            switch (lastDirection)
            {
                case "Up":
                    ChangePlayerSprite(SpriteFactory.Instance.NewUpStoppedPlayer());
                    break;
                case "Down":
                    ChangePlayerSprite(SpriteFactory.Instance.NewDownStoppedPlayer());
                    break;
                case "Left":
                    ChangePlayerSprite(SpriteFactory.Instance.NewLeftStoppedPlayer());
                    break;
                case "Right":
                    ChangePlayerSprite(SpriteFactory.Instance.NewRightStoppedPlayer());
                    break;
            }
        }
    }
}
