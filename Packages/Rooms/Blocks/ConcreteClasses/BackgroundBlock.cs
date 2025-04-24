using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Rooms.Blocks.ConcreteClasses
{
    public class BackgroundBlock : IBlock
    {
        private Texture2D _texture;
        private Rectangle _textureSource;
        private Rectangle _renderedLocation;
        public Rectangle Location { get => this._renderedLocation; set => Location = value; }
        public int LeftXCoord { get { return this._renderedLocation.X; } }
        public int RightXCoord
        {
            get
            {
                return this._renderedLocation.X + this._renderedLocation.Width;
            }
        }
        public int BottomYCoord
        {
            get
            {
                return this._renderedLocation.Y + this._renderedLocation.Height;
            }
        }

        public int PlayerHealthEffect { get => 0; }
        public bool IsPassable { get => false; }
        public bool SwitchRoom { get; set; }

        public BackgroundBlock(Texture2D texture, Rectangle source,
           Rectangle destination)
        {
            this._texture = texture;
            this._textureSource = source;


            this._renderedLocation = destination;

        }



        public void CollideWith(IGameObject collider, Vector2 from)
        {
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this._texture, this._renderedLocation,
               this._textureSource, Color.White);

        }
    }
}
