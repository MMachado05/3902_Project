using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public class Player : IGameObject
    {
        public ISprite Sprite { get; private set; }
        public Rectangle PositionRect { get; set; }
        public Direction LastDirection { get; private set; }
        public Direction SpriteType { get; set; }
        public Boolean isDamaged;
        public Vector2 velocity;

        private float elapsedTime;
        private Vector2 _previousPosition;

        // Add public property to expose _previousPosition
        public Vector2 PreviousPosition => _previousPosition;

        public Player()
        {
            // Set initial default states
            PositionRect = new Rectangle(36, 36, 20, 44);
            velocity = new Vector2(0, 0);
            // Because the vector is the origin, we need to offset the top-left corner of
            //  the rect in order to have the rect properly surround the sprite.
            LastDirection = Direction.Down;
            isDamaged = false;

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
            ChangeSprite(PlayerSpriteFactory.Instance.NewStoppedPlayerSprite(SpriteType, isDamaged));
        }


        public void Update(GameTime gameTime)
        {
            // Update position
            PositionRect = new Rectangle(PositionRect.X + (int)this.velocity.X,
                PositionRect.Y + (int)this.velocity.Y,
                                         PositionRect.Width, PositionRect.Height);

            // Check if we should animate sprite
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25f)
            {
                Sprite.Update();
                elapsedTime = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the sprite at the current position
            Sprite.Draw(spriteBatch, PositionRect);
        }

        public void CollideWith(IGameObject collider)
        {
            // TODO:Implement, include a check for what *kind* of game object it is
        }
    }
}
