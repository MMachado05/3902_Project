using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Project.Enemies;
using Project.Enemies.EnemyClasses;
using Project.Controllers.ControllerClasses;
using Project.Commands.CommandClasses;
using System.Reflection.Metadata.Ecma335;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardController _keyboardController;

        public ISprite playerSprite; // Not best practice, but easiest fix. Could later create read-only property for playerSprite
        private Rectangle playerPosition;
        private Vector2 playerPositionVector;
        private float elapsedTime;
        public SpriteType spriteType;
        //public SpriteState spriteState;

        public string lastDirection = "Down"; // Default direction set to "down" for now; also public not best practice but easy fix for now.
        private bool isMoving = false; // Tracks if player is currently moving; used to set static animation
        
        // Kev adds:
        private EnemyManager enemyManager;
        private EnemyController enemyController;
        private ISprite goblinTexture;
        private Texture2D skeletonTexture;
        private Texture2D dragonTexture;

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
            _keyboardController = new KeyboardController(this, lastDirection);

            // Kev adds:
            enemyManager = new EnemyManager();
            
            ICommand previousEnemyCommand = new CommandPreviousEnemy(this, enemyManager);
            ICommand nextEnemyCommand = new CommandNextEnemy(this, enemyManager);

            Dictionary<Keys, ICommand> enemyCommands = new Dictionary<Keys, ICommand>();
            enemyCommands.Add(Keys.O, previousEnemyCommand);
            enemyCommands.Add(Keys.P, nextEnemyCommand);

            enemyController = new EnemyController(enemyCommands);

            goblinTexture = SpriteFactory.Instance.NewDownStoppedPlayer();
            skeletonTexture = new Texture2D(GraphicsDevice, 50, 50);
            dragonTexture = new Texture2D(GraphicsDevice, 50, 50);

            Color[] goblinData = new Color[50 * 50];
            Color[] skeletonData = new Color[50 * 50];
            Color[] dragonData = new Color[50 * 50];
            for (int i = 0; i < goblinData.Length; i++) goblinData[i] = Color.Green;
            for (int i = 0; i < skeletonData.Length; i++) skeletonData[i] = Color.White;
            for (int i = 0; i < dragonData.Length; i++) dragonData[i] = Color.Red;

            skeletonTexture.SetData(skeletonData);
            dragonTexture.SetData(dragonData);
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _keyboardController.Update();

            // Check if player has stopped moving
            KeyboardState state = Keyboard.GetState();
            if (!(state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.D)) && playerSprite.State != SpriteState.Attacking)
            {
                SetStaticSprite(); // Set idle sprite; moved to another function for clarity
                isMoving = false;
            }
            else isMoving = true;

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25)
            {
                playerSprite.Update();
                elapsedTime = 0f;
            }

            // if (playerSprite.State == SpriteState.FinishedAttack)
            // {
            //     SetStaticSprite();
            //     isMoving = false;
            // }

            // Kev adds
            enemyController.Update();
            enemyManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            playerSprite.Draw(_spriteBatch, playerPositionVector);

            // Kev adds:
            Enemy currentEnemy = enemyManager.GetCurrentEnemy();
            if (currentEnemy is Goblin)
                goblinTexture.Draw(_spriteBatch, currentEnemy.Position);
            else if (currentEnemy is Skeleton)
                _spriteBatch.Draw(skeletonTexture, currentEnemy.Position, Color.White);
            else if (currentEnemy is Dragon)
                _spriteBatch.Draw(dragonTexture, currentEnemy.Position, Color.White);
            
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