using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters.Enums;
using Project.Factories;
using Project.Rooms.Blocks.ConcreteClasses;
using Project.Sprites;
using Project.Enemies.EnemyClasses;
using Project.Items;

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
        public Direction SpriteType { get; set; }
        public Vector2 velocity;

        public int health;
        public float invincibleTime;

        private float elapsedTime;

        public Player()
        {
            // Set initial default states
            Location = new Rectangle(36, 36, 20, 44);
            this._previousLocation = Location;
            velocity = new Vector2(0, 0);
            health = 5;
            invincibleTime = 0;
            LastDirection = Direction.Down;

            // Initially use a "stopped" sprite (down facing)
            Sprite = PlayerSpriteFactory.Instance.NewStoppedPlayerSprite(Direction.Down, false);
        }


        public void Move(int dx, int dy, Direction direction)
        {
            this.velocity.X = dx;
            this.velocity.Y = dy;
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

            ChangeSprite(PlayerSpriteFactory.Instance.NewStoppedPlayerSprite(SpriteType, invincibleTime > 0));
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
            // Count down the invincibility frame timer
            invincibleTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            if (collider is Enemy && invincibleTime < 0)
            {
                health -= 1;
                invincibleTime = 1;
            }

            if (collider is Item)
            {
                health += 2;
            }
        }
    }
}
