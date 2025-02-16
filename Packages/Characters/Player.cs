using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Project
{
    public class Player
    {
        public ISprite Sprite { get; private set; }
        public Vector2 PositionVector { get; private set; }
        public Rectangle PositionRect { get; private set; }
        public string LastDirection { get; private set; }
        public Direction SpriteType { get; set; }
        public Boolean isDamaged;

        private float elapsedTime;

        public Player()
        {
            // Set initial default states
            PositionVector = new Vector2(100, 100);
            PositionRect = new Rectangle(100, 100, 30, 30);
            LastDirection = "Down";
            isDamaged = false;

            // Initially use a "stopped" sprite (down facing)
            Sprite = SpriteFactory.Instance.NewDownStoppedPlayer();
        }

        public void Move(int dx, int dy, string direction)
        {
            // Update position
            PositionVector = new Vector2(PositionVector.X + dx, PositionVector.Y + dy);
            PositionRect = new Rectangle(PositionRect.X + dx, PositionRect.Y + dy,
                                         PositionRect.Width, PositionRect.Height);

            // Keep track of last direction
            LastDirection = direction;
        }

        public void ChangeSprite(ISprite newSprite)
        {
            Sprite = newSprite;
        }
        public void SetStaticSprite()
        {

            switch (LastDirection)
            {
                case "Up":
                    if (isDamaged)
                        ChangeSprite(SpriteFactory.Instance.NewDamagedUpStoppedPlayer());
                    else
                        ChangeSprite(SpriteFactory.Instance.NewUpStoppedPlayer());
                    SpriteType = Direction.Up;
                    Sprite.State = SpriteState.Stopped;
                    break;
                case "Down":
                    if (isDamaged)
                        ChangeSprite(SpriteFactory.Instance.NewDamagedDownStoppedPlayer());
                    else
                        ChangeSprite(SpriteFactory.Instance.NewDownStoppedPlayer());
                    SpriteType = Direction.Down;
                    Sprite.State = SpriteState.Stopped;
                    break;
                case "Left":
                    if (isDamaged)
                        ChangeSprite(SpriteFactory.Instance.NewDamagedLeftStoppedPlayer());
                    else
                        ChangeSprite(SpriteFactory.Instance.NewLeftStoppedPlayer());
                    SpriteType = Direction.Left;
                    Sprite.State = SpriteState.Stopped;
                    break;
                case "Right":
                    if (isDamaged)
                        ChangeSprite(SpriteFactory.Instance.NewDamagedRightStoppedPlayer());
                    else
                        ChangeSprite(SpriteFactory.Instance.NewRightStoppedPlayer());
                    SpriteType = Direction.Right;
                    Sprite.State = SpriteState.Stopped;
                    break;
            }

        }

        public void Update(GameTime gameTime)
        {
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
