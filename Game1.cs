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
    public KeyboardState input;
    public KeyboardState previous;
    SolidBlockController solidBlockController;
    SolidBlock [] arrayOfSolidBlock;
    Texture2D blockSheet;
    Rectangle destination;
    int position;

        public void nextBlock()
         {
             if(position<arrayOfSolidBlock.Length-1){
                 position++;
                 activeBlock = arrayOfSolidBlock[position];
             }else{
                 activeBlock=arrayOfSolidBlock[0];
                 position=0;
             }
         }

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        position=0;
        input = Keyboard.GetState();
        previous = Keyboard.GetState();

        arrayOfSolidBlock = new SolidBlock [4];
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
         blockSheet = Content.Load<Texture2D>("SolidBlocks32x32");
        destination= new Rectangle(0,0,32,32);
         Rectangle a = new Rectangle(0,0,32,32);
        Rectangle b = new Rectangle(32,0,32,32);
        Rectangle c= new Rectangle(64,0,32,32);
        Rectangle d= new Rectangle(96,0,32,32);
      SolidBlock blocksa = new SolidBlock(_spriteBatch,blockSheet,destination,a) ;
        SolidBlock blocksb = new SolidBlock(_spriteBatch,blockSheet,destination,b) ;
        SolidBlock blocksc = new SolidBlock(_spriteBatch,blockSheet,destination,c) ;
        SolidBlock blocksd = new SolidBlock(_spriteBatch,blockSheet,destination,d) ;
         arrayOfSolidBlock[0]= blocksa;
        arrayOfSolidBlock[1]= blocksb;
        arrayOfSolidBlock[2]= blocksc;
        arrayOfSolidBlock[3]= blocksd;
        activeBlock = arrayOfSolidBlock[0];

       nextBlockCommand = new NextBlockCommand(this);
        solidBlockController = new SolidBlockController(this,nextBlockCommand);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
             input = Keyboard.GetState();
       
       solidBlockController.ProcessControls();
       if(!(input.Equals(previous))){
            previous = input;
       }

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
