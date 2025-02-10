using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Enemies;
using Project.Enemies.EnemyClasses;
using Project.Controllers.ControllerClasses;
using Project.Commands.CommandClasses;
using System.Collections.Generic;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
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
            enemyManager = new EnemyManager();

            Dictionary<Keys, ICommand> enemyCommands = new Dictionary<Keys, ICommand>();

            ICommand previousEnemyCommand = new CommandPreviousEnemy(this, enemyManager);
            ICommand nextEnemyCommand = new CommandNextEnemy(this, enemyManager);

            enemyCommands.Add(Keys.O, previousEnemyCommand);
            enemyCommands.Add(Keys.P, nextEnemyCommand);

            enemyController = new EnemyController(enemyCommands);
            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            SpriteFactory.Instance.LoadAllTextures(Content);

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
            enemyController.Update();
            enemyManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
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