using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Packages.Items;

namespace Project;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    ItemManager itemManager;

    KeyboardState previousState;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        itemManager = new ItemManager();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        ItemFactory.Instance.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

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

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        itemManager.getCurrentItem().Draw(_spriteBatch);

        //heartObject.Draw(heartSprite, _spriteBatch);
        //arrowObject.Draw(arrowSprite, _spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
