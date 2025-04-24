using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Commands;

namespace Project.Rooms.Blocks.ConcreteClasses
{
    public class DoorBlock : IBlock
    {
        public Rectangle Location { get => this._renderedLocation; }
        public int PlayerHealthEffect { get => 0; }
        public bool IsPassable { get => true; }
        private Texture2D _texture;
        private Rectangle _textureSource;

        private int _horizontalBlockInstances;
        private int _verticalBlockInstances;
        private ICommand _onPlayerCollisionCommand;

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
           Rectangle destination, ICommand onPlayerCollisionCommand)
        {
            this._texture = texture;
            this._textureSource = source;

            this._horizontalBlockInstances = horizontals;
            this._verticalBlockInstances = verticals;

            this._renderedLocation = destination;
            this._onPlayerCollisionCommand = onPlayerCollisionCommand;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this._texture, this._renderedLocation,
                            this._textureSource, Color.White);
        }

        public void CollideWith(IGameObject collider, Vector2 from)
        {
            // TODO: I figure there's a way to do this w/o needing to check the implementation
            // of the collider, but that's a future issue to solve.
            if (collider is Player player && this.Location.Intersects(player.Location))
            {
                _onPlayerCollisionCommand.Execute();
            }
        }
    }
}
