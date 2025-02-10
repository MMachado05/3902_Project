using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Project
{


    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardController _keyboardController;

        private ISprite playerSprite;
        private Rectangle playerPosition;
        private Vector2 playerPositionVector;
        private float elapsedTime;
        public SpriteType spriteType;

        private bool isAttacking = false;

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
            playerPositionVector = new Vector2(100, 100);
            elapsedTime = 0f;

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
                isMoving = false;
            }
            else isMoving = true;


            _keyboardController.Update();

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25)
            {
                playerSprite.Update();
                elapsedTime = 0f;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            playerSprite.Draw(_spriteBatch, playerPositionVector);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ChangePlayerSprite(ISprite newSprite)
        {
            playerSprite = newSprite;
        }

        public void MovePlayer(int dx, int dy, string direction)
        {
            playerPositionVector.X += dx;
            playerPositionVector.Y += dy;
            playerPosition = new Rectangle(playerPosition.X + dx, playerPosition.Y + dy, playerPosition.Width, playerPosition.Height);
            lastDirection = direction; // Store last movement direction, used to setting the attack and idle animations
        }

        public void Attack()
        {   
            switch (lastDirection)
            {
                case "Up":
                if (!isAttacking){
                    isAttacking = true;
                    ChangePlayerSprite(SpriteFactory.Instance.NewUpAttackingPlayer());
                }
                    break;
                case "Down":
                if (!isAttacking){
                    isAttacking = true;
                    ChangePlayerSprite(SpriteFactory.Instance.NewDownAttackingPlayer());
                                }
                    break;
                case "Left":
                    if (!isAttacking){
                    isAttacking = true;
                    ChangePlayerSprite(SpriteFactory.Instance.NewLeftAttackingPlayer());
                                }
                    break;
                case "Right":
                    if (!isAttacking){
                    isAttacking = true;
                    ChangePlayerSprite(SpriteFactory.Instance.NewRightAttackingPlayer());
                                }
                    break;
            }
            
        }

        private void SetStaticSprite()
        {

            switch (lastDirection)
            {
                case "Up":
                if (!isAttacking)
                    ChangePlayerSprite(SpriteFactory.Instance.NewUpStoppedPlayer());
                    break;
                case "Down":
                if (!isAttacking)
                    ChangePlayerSprite(SpriteFactory.Instance.NewDownStoppedPlayer());
                    break;
                case "Left":
                if (!isAttacking)
                    ChangePlayerSprite(SpriteFactory.Instance.NewLeftStoppedPlayer());
                    break;
                case "Right":
                if (!isAttacking)
                    ChangePlayerSprite(SpriteFactory.Instance.NewRightStoppedPlayer());
                    break;
            }
        }
    }
}
