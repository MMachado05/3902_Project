﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Blocks;
using Project.Enemies;
using Project.Packages;
using Project.Packages.Items;
using Project.renderer;
using Project.rooms;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch; // Not best practice
        private KeyboardController _keyboardController;
        public Player player;

        public ISprite playerSprite; // Not best practice, but easiest fix. Could later create read-only property for playerSprite
        public Direction spriteType;

        public string lastDirection = "Down"; // Default direction set to "down" for now; also public not best practice but easy fix for now.

        private EnemyManager enemyManager;


        // Not best practice; should be moved out of game1
        /// <summary>
        /// </summary>

        private SolidBlockManager blockManager;
        Renderer renderer;
        RoomsManager roomManager;
        MouseController mouseController;
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




            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player();


            // Load all textures
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            SolidBlockSpriteFactory.Instance.LoadAllTextures(Content);
            blockManager = new SolidBlockManager(_spriteBatch);
        


            // Set initial sprite to static down
            playerSprite = PlayerSpriteFactory.Instance.NewDownStoppedPlayer();

            // Initialize KeyboardController with movement and quit commands, pass in player and game

            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            enemyManager = new EnemyManager();
            _keyboardController = new KeyboardController(player, this, blockManager, enemyManager);

            roomManager = new RoomsManager(blockManager,enemyManager,this);
            renderer = new Renderer(roomManager,enemyManager);
            mouseController = new MouseController(this,_graphics,roomManager);

            ItemFactory.Instance.LoadContent(Content);
            itemManager = new ItemManager();
            
        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            _keyboardController.Update();
            mouseController.Update();

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
            if (currentState.IsKeyDown(Keys.D1) && !previousState.IsKeyDown(Keys.D1))
            {
                itemManager.currentItemIndex = 0;
            }
            if (currentState.IsKeyDown(Keys.D2) && !previousState.IsKeyDown(Keys.D2))
            {
                itemManager.currentItemIndex = 1;
            }
            if (currentState.IsKeyDown(Keys.D3) && !previousState.IsKeyDown(Keys.D3))
            {
                itemManager.currentItemIndex = 2;
            }
            itemManager.getCurrentItem().Update();
            previousState = currentState;
            renderer.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            renderer.Draw(_spriteBatch);


            enemyManager.GetCurrentEnemy().Draw(_spriteBatch);
            itemManager.getCurrentItem().Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
