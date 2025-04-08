using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Rooms.Blocks.ConcreteClasses
{
    public class SolidBlock : IBlock
    {
        public Rectangle Location { get => this._renderedLocation; set => Location = value; }
        public int PlayerHealthEffect { get => 0; }
        public bool IsPassable { get => false; }
        public bool SwitchRoom {get;set;}

        private Texture2D _texture;
        private Rectangle _textureSource;

        private int _horizontalBlockInstances;
        private int _verticalBlockInstances;

        private Rectangle _renderedLocation;
        public int LeftXCoord { get { return this._renderedLocation.X; } }
        public int RightXCoord
        {
            get
            {
                return this._renderedLocation.X + this._renderedLocation.Width;
            }
        }
        public int TopYCoord { get { return this._renderedLocation.Y; } }
        public int BottomYCoord
        {
            get
            {
                return this._renderedLocation.Y + this._renderedLocation.Height;
            }
        }

        public SolidBlock(Texture2D texture, Rectangle source, int horizontals, int verticals,
            Rectangle destination)
        {
            this._texture = texture;
            this._textureSource = source;

            this._horizontalBlockInstances = horizontals;
            this._verticalBlockInstances = verticals;

            this._renderedLocation = destination;
            this.SwitchRoom = false;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this._texture, this._renderedLocation,
                this._textureSource, Color.Gray);
        }

        public void CollideWith(IGameObject collider)
        {
            // NOTE: Empty method, since blocks won't respond to collisions
        }
    }
}
