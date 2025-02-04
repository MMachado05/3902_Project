using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // Store the Atlas spritesheet into memory
    private Texture2D atlasTexture;

    private float printScale = 3f;
    private int frameCount = 3;
    private float animationSpeed = 10f;

    public enum MCSpriteNames { UpStopped, RightStopped, DownStopped, LeftStopped, UpWalk, RightWalk, DownWalk, LeftWalk}
    internal ISprite currentSprite; // for changing what sprite class we'll be calling later

    private KeyboardController _keyboardController;
    internal MouseController _mouseController;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        _keyboardController = new KeyboardController(this);
        _mouseController = new MouseController(this, _graphics);

        currentSprite = new UpStopped(atlasTexture, printScale, _graphics);
    }

    protected override void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Loading the spritesheet
        atlasTexture = Content.Load<Texture2D>("Character32x32TextureTemplate");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _keyboardController.Update();
        _mouseController.Update();

        currentSprite.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        currentSprite.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public void SetSprite(MCSpriteNames name)
    {
        switch (name)
        {
            case MCSpriteNames.UpStopped:
                currentSprite = new UpStopped(atlasTexture, printScale, _graphics);
                break;
            case MCSpriteNames.RightStopped:
                currentSprite = new RightStopped(atlasTexture, printScale, _graphics);
                break;
            case MCSpriteNames.DownStopped:
                currentSprite = new DownStopped(atlasTexture, printScale, _graphics);
                break;
            case MCSpriteNames.LeftStopped:
                currentSprite = new LeftStopped(atlasTexture, printScale, _graphics);
                break;
            case MCSpriteNames.UpWalk:
                currentSprite = new UpWalk(atlasTexture, printScale, _graphics, frameCount, animationSpeed);
                break;
            case MCSpriteNames.RightWalk:
                currentSprite = new RightWalk(atlasTexture, printScale, _graphics, frameCount, animationSpeed);
                break;
            case MCSpriteNames.DownWalk:
                currentSprite = new DownWalk(atlasTexture, printScale, _graphics, frameCount, animationSpeed);
                break;
            case MCSpriteNames.LeftWalk:
                currentSprite = new LeftWalk(atlasTexture, printScale, _graphics, frameCount, animationSpeed);
                break;

        }

    }
}
