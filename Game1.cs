using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Project.Enemies;
using Project.Enemies.EnemyClasses;
using Project.Controllers.ControllerClasses;
using Project.Commands.CommandClasses;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardController _keyboardController;
        private Player player; 

        public ISprite playerSprite; // Not best practice, but easiest fix. Could later create read-only property for playerSprite
        private Rectangle playerPosition;
        private Vector2 playerPositionVector;
        public SpriteType spriteType;

        public string lastDirection = "Down"; // Default direction set to "down" for now; also public not best practice but easy fix for now.
        
        // Kev adds:
        private EnemyManager enemyManager;
        private EnemyController enemyController;

        private float elapsedTime;

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

            player = new Player();

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

            // Initialize KeyboardController with movement and quit commands, pass in player and game
            _keyboardController = new KeyboardController(player, this);

            // Kev adds:
            enemyManager = new EnemyManager();
            
            ICommand previousEnemyCommand = new CommandPreviousEnemy(this, enemyManager);
            ICommand nextEnemyCommand = new CommandNextEnemy(this, enemyManager);

            Dictionary<Keys, ICommand> enemyCommands = new Dictionary<Keys, ICommand>();
            enemyCommands.Add(Keys.O, previousEnemyCommand);
            enemyCommands.Add(Keys.P, nextEnemyCommand);

            enemyController = new EnemyController(enemyCommands);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            enemyManager = new EnemyManager();

            ICommand previousEnemyCommand = new CommandPreviousEnemy(this, enemyManager);
            ICommand nextEnemyCommand = new CommandNextEnemy(this, enemyManager);

            Dictionary<Keys, ICommand> enemyCommands = new Dictionary<Keys, ICommand>
            {
                { Keys.O, previousEnemyCommand },
                { Keys.P, nextEnemyCommand }
            };

            enemyController = new EnemyController(enemyCommands);
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _keyboardController.Update();

            // Check if player has stopped moving
            KeyboardState state = Keyboard.GetState();
            if (!(state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.D)) && player.Sprite.State != SpriteState.Attacking)
            {
                player.SetStaticSprite(); // Set idle sprite; moved to player function
            }

            player.Update(gameTime);

            // Kev adds
            enemyController.Update();


            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25)
            {
                enemyManager.GetCurrentEnemy().UpdateAnimation(gameTime);
                elapsedTime = 0f;
            }

            enemyManager.GetCurrentEnemy().UpdateState(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            player.Draw(_spriteBatch); // updated to player class

            // Kev adds:
            Enemy currentEnemy = enemyManager.GetCurrentEnemy();
            if (currentEnemy is Goblin)
                goblinTexture.Draw(_spriteBatch, currentEnemy.Position);
            else if (currentEnemy is Skeleton)
                _spriteBatch.Draw(skeletonTexture, currentEnemy.Position, Color.White);
            else if (currentEnemy is Dragon)
                _spriteBatch.Draw(dragonTexture, currentEnemy.Position, Color.White);
            
            enemyManager.GetCurrentEnemy().Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
