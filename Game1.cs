using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Blocks;
using Project.Commands.CommandClasses;
using Project.Controllers.ControllerClasses;
using Project.Enemies;
using Project.Packages.Items;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch; // Not best practice
        private KeyboardController _keyboardController;
        private Player player;

        public ISprite playerSprite; // Not best practice, but easiest fix. Could later create read-only property for playerSprite
        private Rectangle playerPosition;
        private Vector2 playerPositionVector;
        public Direction spriteType;

        public string lastDirection = "Down"; // Default direction set to "down" for now; also public not best practice but easy fix for now.

        private EnemyManager enemyManager;
        private EnemyController enemyController;

        private float elapsedTime;

        // Not best practice; should be moved out of game1
        public SolidBlock activeBlock;
        public NextBlockCommand nextBlockCommand;
        public PreviousBlockCommand previousBlockCommand;
        public KeyboardState input;
        public KeyboardState previous;
        SolidBlockController solidBlockController;

        private SolidBlockManager manager;
        Dictionary<Keys, ICommand> enemyCommands;
        public void restart()
        {
            this.LoadContent();
            this.Initialize();
        }

        // should be moved out of game1
        ItemManager itemManager;
        KeyboardState previousState = new KeyboardState();


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

            input = Keyboard.GetState();
            previous = Keyboard.GetState();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load all textures
            SpriteFactory.Instance.LoadAllTextures(Content);

            // Set initial sprite to static down
            playerSprite = SpriteFactory.Instance.NewDownStoppedPlayer();

            // Load item sprites and create item manager
            ItemFactory.Instance.LoadContent(Content);
            itemManager = new ItemManager();

            // Initialize KeyboardController with movement and quit commands, pass in player and game
            _keyboardController = new KeyboardController(player, this, itemManager);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            enemyManager = new EnemyManager();

            ICommand previousEnemyCommand = new CommandPreviousEnemy(this, enemyManager);
            ICommand nextEnemyCommand = new CommandNextEnemy(this, enemyManager);


            enemyCommands = new Dictionary<Keys, ICommand>
            {
                { Keys.O, previousEnemyCommand },
                { Keys.P, nextEnemyCommand }
            };

            enemyController = new EnemyController(enemyCommands);

            SolidBlockSpriteFactory.Instance.LoadAllTextures(Content);
            manager = new SolidBlockManager(_spriteBatch);

            nextBlockCommand = new NextBlockCommand(manager);
            previousBlockCommand = new PreviousBlockCommand(manager);
            solidBlockController = new SolidBlockController(this, nextBlockCommand, previousBlockCommand);
            activeBlock = manager.GetCurrentBlock();

            
        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            enemyController.Update();

            _keyboardController.Update();

            // Check if player has stopped moving
            input = Keyboard.GetState();
            if (!(input.IsKeyDown(Keys.W) || input.IsKeyDown(Keys.A) || input.IsKeyDown(Keys.S) || input.IsKeyDown(Keys.D)) && player.Sprite.State != SpriteState.Attacking)
            {
                player.SetStaticSprite(); // Set idle sprite
            }

            player.Update(gameTime);

            KeyboardState currentState = Keyboard.GetState();

            // Checking for keys pressed to switch items; should be moved out of game1
            if (currentState.IsKeyDown(Keys.I) && !previousState.IsKeyDown(Keys.I))
            {
                itemManager.nextItem();
            }
            if (currentState.IsKeyDown(Keys.U) && !previousState.IsKeyDown(Keys.U))
            {
                itemManager.previousItem();
            }

            itemManager.getCurrentItem().Update();
            previousState = currentState;

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25)
            {
                enemyManager.GetCurrentEnemy().UpdateAnimation(gameTime);
                elapsedTime = 0f;
            }

            enemyManager.GetCurrentEnemy().UpdateState(gameTime);

            // should be moved out of game1
            solidBlockController.Update();
            if (!(input.Equals(previous)))
            {
                previous = input;
            }
            activeBlock = manager.GetCurrentBlock();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            activeBlock.Draw();

            player.Draw(_spriteBatch);
        
            enemyManager.GetCurrentEnemy().Draw(_spriteBatch);
            itemManager.getCurrentItem().Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
