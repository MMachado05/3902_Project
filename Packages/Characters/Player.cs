using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters.Enums;
using Project.Factories;
using Project.Items;
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
        private IItem _activeItem;

        private List<DirectionRegister> _activeDirections;

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

            this._activeItem = null;
            this._activeDirections = new List<DirectionRegister>();
        }

        public void RegisterDirection(Direction direction, int dx, int dy)
        {
            // Only register inactive directions to avoid double-ups
            foreach (DirectionRegister dr in this._activeDirections)
            {
                if (dr.Direction == direction) return;
            }

            this._activeDirections.Add(new DirectionRegister(direction, dx, dy));
            this.velocity.X += dx;
            this.velocity.Y += dy;
        }

        public void DeregisterDirection(Direction direction)
        {
            int i = 0;
            bool found = false;

            while (i < this._activeDirections.Count && !found)
            {
                if (this._activeDirections[i].Direction == direction)
                {
                    this.velocity.X -= this._activeDirections[i].Dx;
                    this.velocity.Y -= this._activeDirections[i].Dy;
                    found = true;
                }
                else i++;
            }

            this._activeDirections.RemoveAt(i);

            if (this._activeDirections.Count == 0)
                this.LastDirection = direction;
        }

        public Direction LastActiveDirection
        {
            get
            {
                if (this._activeDirections.Count == 0)
                    return this.LastDirection;

                return this._activeDirections[this._activeDirections.Count - 1].Direction;
            }
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

        public void Attack()
        {
            if (this._activeItem == null)
            {

            }
        }

        public void Update(GameTime gameTime)
        {
            if (this.velocity.X == 0 && this.velocity.Y == 0 && this.Sprite.State != CharacterState.Attacking)
                this.SetStaticSprite();

            // Check if we're in the middle of an attack - if not, Update position
            if (this.Sprite.State != CharacterState.Attacking)
            {
                this._previousLocation = Location;
                Location = new Rectangle(Location.X + (int)this.velocity.X,
                    Location.Y + (int)this.velocity.Y,
                    Location.Width, Location.Height);
            }

            // Check if we should animate sprite
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25f)
            {
                Sprite.Update(gameTime);
                elapsedTime = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle? position = null)
        {
            Sprite.Draw(spriteBatch, position.HasValue ? position.Value : this.Location);
        }

        public void CollideWith(IGameObject collider)
        {
            // In another branch, currenly using an IGameObject.IsPassable property

            if (collider is SolidBlock)
            {
                this.Location = this._previousLocation;
            }
        }

        private class DirectionRegister
        {
            public DirectionRegister(Direction direction, int dx, int dy)
            {
                this.Direction = direction;
                this.Dx = dx;
                this.Dy = dy;
            }

            public DirectionRegister() { }

            public Direction Direction { get; set; }
            public int Dx { get; set; }
            public int Dy { get; set; }
        }
    }
}
