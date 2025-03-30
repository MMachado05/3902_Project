using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters.Enums;
using Project.Factories;
using Project.Rooms.Blocks.ConcreteClasses;
using Project.Sprites;

namespace Project.Characters
{
    public class Player : IGameObject
    {
        public ISprite Sprite { get; private set; }
        public Rectangle Location { get; set; }
        private Rectangle _previousLocation;
        public Direction LastDirection { get; private set; }
        public Direction SpriteType { get; set; }
        public Boolean isDamaged;
        public Vector2 velocity;

        private float elapsedTime;

        public Player()
        {
            // Set initial default states
            Location = new Rectangle(36, 36, 20, 44);
            this._previousLocation = Location;
            velocity = new Vector2(0, 0);

            LastDirection = Direction.Down;
            isDamaged = false;

            // Initially use a "stopped" sprite (down facing)
            Sprite = PlayerSpriteFactory.Instance.NewStoppedPlayerSprite(Direction.Down, false);
        }


        public void UpdateVelocity(int dx, int dy, Direction direction)
        {
            if (dx != 0) this.velocity.X = dx;
            if (dy != 0) this.velocity.Y = dy;
            this.LastDirection = direction;
        }

        public void ChangeSprite(ISprite newSprite)
        {
            Sprite = newSprite;
        }

        public void SetStaticSprite()
        {
            this.velocity.X = 0;
            this.velocity.Y = 0;

            SpriteType = LastDirection;
            Sprite.State = CharacterState.Stopped;

            ChangeSprite(PlayerSpriteFactory.Instance.NewStoppedPlayerSprite(SpriteType, isDamaged));
        }

        public void Update(GameTime gameTime)
        {
            // Update position
            this._previousLocation = Location;
            Location = new Rectangle(Location.X + (int)this.velocity.X,
                Location.Y + (int)this.velocity.Y,
                                         Location.Width, Location.Height);

            // Check if we should animate sprite
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25f)
            {
                Sprite.Update();
                elapsedTime = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle? position = null)
        {
            Sprite.Draw(spriteBatch, position.HasValue ? position.Value : this.Location);
        }

        public void CollideWith(IGameObject collider)
        {
            // TODO:Implement, include a check for what *kind* of game object it is
            if (collider is SolidBlock)
            {
                this.Location = this._previousLocation;
            }
        }
    }
}
