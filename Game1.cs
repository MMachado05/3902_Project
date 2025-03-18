using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Blocks;
using Project.Commands;
using Project.Commands.CommandClasses;
using Project.Content;
using Project.Enemies;
using Project.Packages.Characters;
using Project.Packages.Items;
using Project.renderer;
using Project.rooms;

namespace Project
{
    public class Game1 : Game
    {
        // Basic Game fields
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch; // Not best practice

        // Controllers
        private IController _keyboardController;
        private IController _mouseController;

        public Player player;
        private Rectangle playerPosition;
        private Vector2 playerPositionVector;
        // NOTE: From Boggus: The Controllers all implement IController, so we should
        // use that abstraction

        public ISprite playerSprite; // Not best practice, but easiest fix. Could later create read-only property for playerSprite
        public Direction spriteType;
        KeyboardState input;

        public string lastDirection = "Down"; // Default direction set to "down" for now; also public not best practice but easy fix for now.
        private CollisionManager collisionManager;

        // NOTE: From Boggus: enemyManager, etc. should all be hidden behind a room manager.
        // Remove dependencies here once they've been moved into their appropriate classes

        private EnemyManager enemyManager;
        private float elapsedTime;


        // Not best practice; should be moved out of game1
        /// <summary>
        /// </summary>

        private SolidBlockManager blockManager;
        Renderer renderer;
        RoomManager roomManager;
        public void restart()
        {
            this.LoadContent();
            this.Initialize();
        }

        // should be moved out of game1
        ItemManager itemManager;
        KeyboardState previousState = new KeyboardState();


        private ItemController _itemController;
        PlayerItemCollisionHandler playerItemCollisionHandler;

        public Game1()
        {
            // Since we're in constructor, no need to call GraphicsDeviceManager.ApplyChanges()
            // :)
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 960; // Set width
            _graphics.PreferredBackBufferHeight = 704; // Set height

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.player = new Player();
            this.enemyManager = new EnemyManager();

            // Set up controllers
            this.SetUpKeyboardController();

            input = Keyboard.GetState();
            base.Initialize();
        }

        private void SetUpKeyboardController()
        {
          KeyboardController kbc = new KeyboardController();

          // Player movement
          kbc.RegisterKey(Keys.W, new MoveCommand(player, Direction.Up));
          kbc.RegisterKey(Keys.A, new MoveCommand(player, Direction.Left));
          kbc.RegisterKey(Keys.S, new MoveCommand(player, Direction.Down));
          kbc.RegisterKey(Keys.D, new MoveCommand(player, Direction.Right));

          // Attacking
          kbc.RegisterKey(Keys.Z, new AttackCommand(player));
          kbc.RegisterKey(Keys.N, new AttackCommand(player));
          
          // TODO: Arrow keys to change current room
          
          // Game logic
          kbc.RegisterKey(Keys.Q, new QuitCommand(this));
          kbc.RegisterKey(Keys.R, new RestartGameCommand(this));

          // Debugging commands
          kbc.RegisterKey(Keys.E, new DamageCommand(player));

          kbc.RegisterKey(Keys.O, new CommandPreviousEnemy(this, this.enemyManager));
          kbc.RegisterKey(Keys.P, new CommandNextEnemy(this, this.enemyManager));

          this._keyboardController = kbc;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load all textures
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            SolidBlockSpriteFactory.Instance.LoadAllTextures(Content);
            blockManager = new SolidBlockManager(_spriteBatch);

            // Set initial sprite to static down
            playerSprite = PlayerSpriteFactory.Instance.NewStoppedPlayerSprite(Direction.Down, false);

            // Load item sprites and create item manager
            ItemFactory.Instance.LoadContent(Content);
            itemManager = new ItemManager(this);
            // Initialize ItemController with commands for switching items
            _itemController = new ItemController(itemManager, this);

            // Initialize KeyboardController with movement and quit commands, pass in player and game

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            collisionManager = new CollisionManager();
            roomManager = new RoomManager(blockManager, enemyManager, this);
            roomManager.LoadRoomsFromContent(Content);
            renderer = new Renderer(roomManager, enemyManager, collisionManager);
            _mouseController = new MouseController(this, _graphics, roomManager);

            playerItemCollisionHandler = new PlayerItemCollisionHandler(itemManager, player);
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _keyboardController.Update();
            _mouseController.Update();
            _itemController.Update();

            // Check if player has stopped moving
            input = Keyboard.GetState();

            //collisionManager.UpdateCollisions(player, blockManager.GetAllBlocks());

            if (!(input.IsKeyDown(Keys.W) || input.IsKeyDown(Keys.A) || input.IsKeyDown(Keys.S) || input.IsKeyDown(Keys.D)) && player.Sprite.State != CharacterState.Attacking)
            {
                player.SetStaticSprite(); // Set idle sprite
            }


            // Update lastDirection based on movement input (def need to change this approach)
            if (input.IsKeyDown(Keys.W)) lastDirection = "Up";
            else if (input.IsKeyDown(Keys.S)) lastDirection = "Down";
            else if (input.IsKeyDown(Keys.A)) lastDirection = "Left";
            else if (input.IsKeyDown(Keys.D)) lastDirection = "Right";


            //should be replaced with level loader
            _itemController.Update();

            // Update world items
            foreach (IItem item in itemManager.GetWorldItems())
            {
                item.Update();
            }

            //Placing and updating inventory items
            itemManager.GetCurrentItem().Update();
            itemManager.PlaceInventoryItem();


            renderer.Update(gameTime);

            playerItemCollisionHandler.HandlePlayerItemCollision();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            renderer.Draw(_spriteBatch);
            // Draw world items
            foreach (IItem item in itemManager.GetWorldItems())
            {
                item.Draw(_spriteBatch);
            }
            // Draw inventory items
            itemManager.GetCurrentItem().Draw(_spriteBatch);
            enemyManager.GetCurrentEnemy().Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
