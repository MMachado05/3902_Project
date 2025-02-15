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
        public SpriteType SpriteType { get; set; }
        
        // tracks if player is moving, or you can keep that logic in Update
    //   private bool isMoving;    

        private float elapsedTime;

        public Player()
        {
            // Set initial default states
            // (Optionally move these from Game1.Initialize to here)
            PositionVector = new Vector2(100, 100);
            PositionRect    = new Rectangle(100, 100, 30, 30);
            LastDirection   = "Down";
            SpriteType      = SpriteType.WalkingDown; // or whichever you like

            // Initially use a "stopped" sprite (down facing)
            Sprite          = SpriteFactory.Instance.NewDownStoppedPlayer();
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
                    ChangeSprite(SpriteFactory.Instance.NewUpStoppedPlayer());
                    break;
                case "Down":
                    ChangeSprite(SpriteFactory.Instance.NewDownStoppedPlayer());
                    break;
                case "Left":
                    ChangeSprite(SpriteFactory.Instance.NewLeftStoppedPlayer());
                    break;
                case "Right":
                    ChangeSprite(SpriteFactory.Instance.NewRightStoppedPlayer());
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            // Example logic: check if we should animate sprite
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
