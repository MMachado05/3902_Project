using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
<<<<<<< HEAD
using Project.Blocks;
=======
using Project.Commands.CommandClasses;
using Project.Controllers.ControllerClasses;
using Project.Enemies;
>>>>>>> main

namespace Project
{
<<<<<<< HEAD
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public SolidBlock activeBlock;
    public NextBlockCommand nextBlockCommand;
    public PreviousBlockCommand previousBlockCommand;
    public KeyboardState input;
    public KeyboardState previous;
    SolidBlockController solidBlockController;

    SolidBlockManager manager;

    /*public void nextBlock()
     {
         if(position<arrayOfSolidBlock.Length-1){
             position++;
             activeBlock = arrayOfSolidBlock[position];
         }else{
             activeBlock=arrayOfSolidBlock[0];
             position=0;
         }
     }
     public void previousBlock()
     {
         if(position>0){
             position--;
             activeBlock = arrayOfSolidBlock[position];
         }else{
             activeBlock=arrayOfSolidBlock[arrayOfSolidBlock.Length-1];
             position=arrayOfSolidBlock.Length-1;
         }
     }*/


    public Game1()
=======
    public class Game1 : Game
>>>>>>> main
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardController _keyboardController;
        private Player player; 

<<<<<<< HEAD
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        input = Keyboard.GetState();
        previous = Keyboard.GetState();

=======
        public ISprite playerSprite; // Not best practice, but easiest fix. Could later create read-only property for playerSprite
        private Rectangle playerPosition;
        private Vector2 playerPositionVector;
        public Direction spriteType;
>>>>>>> main

        public string lastDirection = "Down"; // Default direction set to "down" for now; also public not best practice but easy fix for now.
        
        // Kev adds:
        private EnemyManager enemyManager;
        private EnemyController enemyController;

<<<<<<< HEAD
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        SolidBlockSpriteFactory.Instance.LoadAllTextures(Content);
        manager = new SolidBlockManager(_spriteBatch);

        nextBlockCommand = new NextBlockCommand(manager);
        previousBlockCommand = new PreviousBlockCommand(manager);
        solidBlockController = new SolidBlockController(this, nextBlockCommand, previousBlockCommand);
        activeBlock = manager.GetCurrentBlock();

=======
        private float elapsedTime;
>>>>>>> main

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

<<<<<<< HEAD
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        input = Keyboard.GetState();

        solidBlockController.ProcessControls();
        if (!(input.Equals(previous)))
        {
            previous = input;
        }
        activeBlock = manager.GetCurrentBlock();
=======
        protected override void Initialize()
        {
            playerPosition = new Rectangle(100, 100, 30, 30); // Initial character position
            playerPositionVector = new Vector2(100, 100);
>>>>>>> main

            player = new Player();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

<<<<<<< HEAD
        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        activeBlock.Draw();
        _spriteBatch.End();
=======
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

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


                // Kev adds
                enemyController.Update();

            _keyboardController.Update();

            // Check if player has stopped moving
            KeyboardState state = Keyboard.GetState();
            if (!(state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.D)) && player.Sprite.State != SpriteState.Attacking)
            {
                player.SetStaticSprite(); // Set idle sprite; moved to player function
            }                

                player.Update(gameTime);




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
            enemyManager.GetCurrentEnemy().Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
>>>>>>> main

    }
}
