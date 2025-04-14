using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project;
using Project.Characters;
using Project.Rooms.Blocks;

namespace Project.Rooms.Blocks.ConcreteClasses
{
    public class DoorBlock : IBlock
    {
        public Rectangle Location { get => this._renderedLocation; }
        public enum DoorDirection { Up, Down, Left, Right }
        public DoorDirection Direction { get; set; }
        public int PlayerHealthEffect { get => 0; }
        public bool IsPassable { get => true; }
        public bool SwitchRoom { get; set; }
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

        Rectangle IGameObject.Location { get => this.Location; set => throw new NotImplementedException(); }

        public DoorBlock(Texture2D texture, Rectangle source, int horizontals, int verticals,
           Rectangle destination, DoorDirection direction)
        {
            this._texture = texture;
            this._textureSource = source;

            this._horizontalBlockInstances = horizontals;
            this._verticalBlockInstances = verticals;

            this._renderedLocation = destination;
            Direction = direction;
            SwitchRoom = false;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this._texture, this._renderedLocation,
                            this._textureSource, Color.White);
        }

        public void CollideWith(IGameObject collider)
        {
            if (collider is Player player)
            {
                if (this.Location.Intersects(player.Location))
                {
                    this.SwitchRoom = true;
                }
            }

        }
    }
}
