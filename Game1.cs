using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Blocks;
using Project.Commands.CommandClasses;
using Project.Controllers.ControllerClasses;
using Project.Enemies;

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
        public Direction spriteType;

        public string lastDirection = "Down"; // Default direction set to "down" for now; also public not best practice but easy fix for now.

        // Kev adds:
        private EnemyManager enemyManager;
        private EnemyController enemyController;

        private float elapsedTime;
        // Solid block variable

        public SolidBlock activeBlock;
        public NextBlockCommand nextBlockCommand;
        public PreviousBlockCommand previousBlockCommand;
        public KeyboardState input;
        public KeyboardState previous;
        SolidBlockController solidBlockController;

        SolidBlockManager manager;

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
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        itemManager = new ItemManager();
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

            // Initialize KeyboardController with movement and quit commands, pass in player and game
            _keyboardController = new KeyboardController(player, this);

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

            SolidBlockSpriteFactory.Instance.LoadAllTextures(Content);
            manager = new SolidBlockManager(_spriteBatch);

            nextBlockCommand = new NextBlockCommand(manager);
            previousBlockCommand = new PreviousBlockCommand(manager);
            solidBlockController = new SolidBlockController(this, nextBlockCommand, previousBlockCommand);
            activeBlock = manager.GetCurrentBlock();

        }
        // TODO: use this.Content to load your game content here
        ItemFactory.Instance.LoadContent(Content);
    }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // Kev adds
            enemyController.Update();

            _keyboardController.Update();

            // Check if player has stopped moving
            input = Keyboard.GetState();
            if (!(input.IsKeyDown(Keys.W) || input.IsKeyDown(Keys.A) || input.IsKeyDown(Keys.S) || input.IsKeyDown(Keys.D)) && player.Sprite.State != SpriteState.Attacking)
            {
                player.SetStaticSprite(); // Set idle sprite; moved to player function
            }

            player.Update(gameTime);



        KeyboardState currentState = Keyboard.GetState();

        // Check if the space key was just pressed
        if (currentState.IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space))
        {
            itemManager.SwitchItem();
        }

        itemManager.getCurrentItem().Update();
        previousState = currentState;

        base.Update(gameTime);
    }
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25)
            {
                enemyManager.GetCurrentEnemy().UpdateAnimation(gameTime);
                elapsedTime = 0f;
            }

            enemyManager.GetCurrentEnemy().UpdateState(gameTime);

            // block 
            // input = Keyboard.GetState();

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

            player.Draw(_spriteBatch); // updated to player class

            // Kev adds:          
            enemyManager.GetCurrentEnemy().Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
