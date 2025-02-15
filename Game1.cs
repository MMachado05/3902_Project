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
        private Player player; 

        public ISprite playerSprite; // Not best practice, but easiest fix. Could later create read-only property for playerSprite
        private Rectangle playerPosition;
        private Vector2 playerPositionVector;
        public SpriteType spriteType;

        public string lastDirection = "Down"; // Default direction set to "down" for now; also public not best practice but easy fix for now.
        
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
            if (!(state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.D)) && player.Sprite.State != SpriteState.Attacking)
            {
                player.SetStaticSprite(); // Set idle sprite; moved to player function
            }

            player.Update(gameTime);

            // Kev adds
            enemyController.Update();
            enemyManager.Update(gameTime);

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
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}