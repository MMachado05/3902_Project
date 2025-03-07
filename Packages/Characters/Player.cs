using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public class Player
    {
        public ISprite Sprite { get; private set; }
        public Vector2 PositionVector { get;  set; }
        public Rectangle PositionRect { get;  set; }
        public String LastDirection { get; private set; }
        public Direction SpriteType { get; set; }
        public Boolean isDamaged;
        private Dictionary<String, Direction> stringDirToEnum;
        public Vector2 velocity;

        private float elapsedTime;
        private Vector2 _previousPosition;

        // Add public property to expose _previousPosition
        public Vector2 PreviousPosition => _previousPosition;

        public Player()
        {
            // TODO: This is a hotfix because other methods expect to pass in strings.
            // Eventually, everything should be using the enums.
            stringDirToEnum = new Dictionary<string, Direction>();
            stringDirToEnum.Add("Up", Direction.Up);
            stringDirToEnum.Add("Down", Direction.Down);
            stringDirToEnum.Add("Left", Direction.Left);
            stringDirToEnum.Add("Right", Direction.Right);
            // Set initial default states
            PositionVector = new Vector2(36, 36);
            PositionRect = new Rectangle(36, 36, 64, 64);
            velocity = new Vector2(0, 0);
            // Because the vector is the origin, we need to offset the top-left corner of
            //  the rect in order to have the rect properly surround the sprite.
            LastDirection = "Down";
            isDamaged = false;

            // Initially use a "stopped" sprite (down facing)
            Sprite = PlayerSpriteFactory.Instance.NewStoppedPlayerSprite(Direction.Down, false);
        }


        public void Move(int dx, int dy, string direction)
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
            SpriteType = this.stringDirToEnum[LastDirection];
            Sprite.State = SpriteState.Stopped;
            ChangeSprite(PlayerSpriteFactory.Instance.NewStoppedPlayerSprite(SpriteType, isDamaged));
        }


        public void Update(GameTime gameTime)
        {
            // Move correctly
            // Store previous movement here to prevent moving upon collision.
            _previousPosition = PositionVector;

            // Update position
            PositionVector = new Vector2(PositionVector.X + this.velocity.X,
                PositionVector.Y + this.velocity.Y);
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
            Sprite.Draw(spriteBatch, PositionVector);
        }
    }
}
