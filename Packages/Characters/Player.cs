using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters.Enums;
using Project.Factories;
using Project.Items;
using Project.Packages.Sounds;
using Project.Sprites;

namespace Project.Characters
{
    public class Player : IGameObject
    {
        public Rectangle Location { get; set; }
        public int PlayerHealthEffect { get => 0; }
        public bool IsPassable { get => true; }

        private Rectangle _previousLocation;
        public ISprite Sprite { get; private set; }
        public Direction LastDirection { get; private set; }
        public Direction LastActiveDirection
        {
            get
            {
                if (this._activeDirections.Count == 0)
                    return this.LastDirection;

                return this._activeDirections[this._activeDirections.Count - 1].Direction;
            }
        }
        private Vector2 _velocity;
        private IItem _activeItem;

        private List<DirectionRegister> _activeDirections;

        public int health;
        public float invincibleTime;

        private float elapsedTime;

        public Player()
        {
            // Set initial default states
            Location = new Rectangle(36, 36, 20, 44);
            this._previousLocation = Location;
            _velocity = new Vector2(0, 0);
            health = 5;
            invincibleTime = 0;
            LastDirection = Direction.Down;

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
            this._velocity.X += dx;
            this._velocity.Y += dy;
        }

        public void DeregisterDirection(Direction direction)
        {
            int i = 0;
            bool found = false;

            while (i < this._activeDirections.Count && !found)
            {
                if (this._activeDirections[i].Direction == direction)
                {
                    _velocity.X -= _activeDirections[i].Dx;
                    _velocity.Y -= _activeDirections[i].Dy;
                    found = true;
                }
                else i++;
            }

            this._activeDirections.RemoveAt(i);

            if (this._activeDirections.Count == 0)
                this.LastDirection = direction;
        }

        public void ChangeSprite(ISprite newSprite)
        {
            Sprite = newSprite;
        }

        public void SetStaticSprite()
        {
            Sprite.State = CharacterState.Stopped;
            ChangeSprite(PlayerSpriteFactory.Instance.NewStoppedPlayerSprite(LastActiveDirection, invincibleTime > 0));
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
            // Count down the invincibility frame timer
            invincibleTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle? position = null)
        {
            Sprite.Draw(spriteBatch, position.HasValue ? position.Value : this.Location);
        }

        public void CollideWith(IGameObject collider)
        {
            if (!collider.IsPassable)
            {
                this.Location = this._previousLocation;
            }

            int collisionHealthEffect = collider.PlayerHealthEffect;
            if (invincibleTime < 0)
            {
                health += collisionHealthEffect;
                if (collisionHealthEffect < 0) // Causing damage
                {
                    SoundEffectManager.Instance.playDamage();
                    invincibleTime = 1;
                }
                if (collisionHealthEffect > 0) // Healing
                {
                    SoundEffectManager.Instance.playHeal();
                }
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
