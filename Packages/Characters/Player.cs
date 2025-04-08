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
                int slashX = this.Location.X, slashY = this.Location.Y;
                switch (LastActiveDirection)
                {
                    case Direction.Up:
                        slashY -= ItemFactory.Instance.BasicAttackHeight;
                        break;
                    case Direction.Down:
                        slashY += this.Location.Height;
                        break;
                    case Direction.Left:
                        slashX -= ItemFactory.Instance.BasicAttackWidth;
                        break;
                    case Direction.Right:
                        slashX += this.Location.Width;
                        break;
                }
                this._activeItem = ItemFactory.Instance.CreateBasicAttack(LastActiveDirection,
                    slashX, slashY);
            }
        }

        public void Update(GameTime gameTime)
        {
            System.Console.WriteLine(this.Sprite.State);
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25f)
            {
                Sprite.Update(gameTime);
                elapsedTime = 0f;
                if (this._activeItem != null)
                    this._activeItem.Update(gameTime);
            }

            switch (this.Sprite.State)
            {
                case CharacterState.Stopped:
                    if (_velocity.X != 0 || _velocity.Y != 0)
                    {
                        this.ChangeSprite(PlayerSpriteFactory.Instance.NewWalkingPlayerSprite(
                              LastActiveDirection, this.invincibleTime > 0
                              ));
                    }
                    break;
                case CharacterState.Walking:
                    this._previousLocation = Location;
                    Location = new Rectangle(Location.X + (int)this._velocity.X,
                        Location.Y + (int)this._velocity.Y,
                        Location.Width, Location.Height);
                    break;
                case CharacterState.FinishedAttack:
                    // Throw away the "item" related to default attack
                    // TODO: Change this logic when more items are implemented, I know it's
                    //  shoddy at best.
                    this._activeItem.Location = new Rectangle(0, 0, -1, -1);
                    this._activeItem = null;
                    this.SetStaticSprite();
                    break;
            }

            if (this._velocity.X == 0
                && this._velocity.Y == 0
                && this.Sprite.State != CharacterState.Attacking)
                this.SetStaticSprite();

            // Count down the invincibility frame timer
            invincibleTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (health > 5)
            {
                health = 5;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle? position = null)
        {
            Sprite.Draw(spriteBatch, position.HasValue ? position.Value : this.Location);
            if (this.Sprite.State == CharacterState.Attacking && this._activeItem != null)
                this._activeItem.Draw(spriteBatch);
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
