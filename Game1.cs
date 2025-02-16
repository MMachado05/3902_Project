using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Blocks;

namespace Project;

public class Game1 : Game
{
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
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        input = Keyboard.GetState();
        previous = Keyboard.GetState();


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        SolidBlockSpriteFactory.Instance.LoadAllTextures(Content);
        manager = new SolidBlockManager(_spriteBatch);

        nextBlockCommand = new NextBlockCommand(manager);
        previousBlockCommand = new PreviousBlockCommand(manager);
        solidBlockController = new SolidBlockController(this, nextBlockCommand, previousBlockCommand);
        activeBlock = manager.GetCurrentBlock();


        // TODO: use this.Content to load your game content here
    }

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

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        activeBlock.Draw();
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
