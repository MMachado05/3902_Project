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

    private Texture2D heartTexture;
    private Texture2D arrowTexture;

    ISprite heartSprite;
    ISprite arrowSprite;

    Arrow arrowObject = new Arrow();
    HeartItem heartObject = new HeartItem();
    

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        Texture2D heartTexture = Content.Load<Texture2D>("test_item");
        Texture2D arrowTexture = Content.Load<Texture2D>("arrow");
        heartSprite = new AnimatedLoopSprite(heartTexture, new Rectangle(0, 0, 64, 64), 3, 4, new SpriteState());
        arrowSprite = new StationarySprite(arrowTexture, new Rectangle(0, 0, 32, 32), 3, new SpriteState());

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        heartObject.Update();
        arrowObject.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        heartObject.Draw(heartSprite, _spriteBatch);
        arrowObject.Draw(arrowSprite, _spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
