using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Blocks;

namespace Project
{
    public class Player
    {
        public ISprite Sprite { get; private set; }
        public Vector2 PositionVector { get; set; }
        public Rectangle PositionRect { get; set; }
        public string LastDirection { get; private set; }
        public Direction SpriteType { get; set; }
        public Boolean isDamaged;

        private float elapsedTime;
        private Vector2 _previousPosition;

        // Add public property to expose _previousPosition
        public Vector2 PreviousPosition => _previousPosition;
        public Player()
        {
            // Set initial default states
            PositionVector = new Vector2(36, 36);
            PositionRect = new Rectangle(36, 36, 64, 64);
            // Because the vector is the origin, we need to offset the top-left corner of
            //  the rect in order to have the rect properly surround the sprite.
            LastDirection = "Down";
            isDamaged = false;

            // Initially use a "stopped" sprite (down facing)
            Sprite = PlayerSpriteFactory.Instance.NewDownStoppedPlayer();
        }


        public void Move(int dx, int dy, string direction)
        {
            // Store previous movement here to prevent moving upon collision.
            _previousPosition = PositionVector;

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
                        ChangeSprite(PlayerSpriteFactory.Instance.NewDamagedUpStoppedPlayer());
                    else
                        ChangeSprite(PlayerSpriteFactory.Instance.NewUpStoppedPlayer());
                    SpriteType = Direction.Up;
                    Sprite.State = SpriteState.Stopped;
                    break;
                case "Down":
                    if (isDamaged)
                        ChangeSprite(PlayerSpriteFactory.Instance.NewDamagedDownStoppedPlayer());
                    else
                        ChangeSprite(PlayerSpriteFactory.Instance.NewDownStoppedPlayer());
                    SpriteType = Direction.Down;
                    Sprite.State = SpriteState.Stopped;
                    break;
                case "Left":
                    if (isDamaged)
                        ChangeSprite(PlayerSpriteFactory.Instance.NewDamagedLeftStoppedPlayer());
                    else
                        ChangeSprite(PlayerSpriteFactory.Instance.NewLeftStoppedPlayer());
                    SpriteType = Direction.Left;
                    Sprite.State = SpriteState.Stopped;
                    break;
                case "Right":
                    if (isDamaged)
                        ChangeSprite(PlayerSpriteFactory.Instance.NewDamagedRightStoppedPlayer());
                    else
                        ChangeSprite(PlayerSpriteFactory.Instance.NewRightStoppedPlayer());
                    SpriteType = Direction.Right;
                    Sprite.State = SpriteState.Stopped;
                    break;
            }

        }

        public void HandleBlockCollision(SolidBlock block)
        {
            // Revertint player to last safe position when colliding
            // NOTE: Because the sprite is not actually drawn exactly where the bounding box is, we have to do these weird offsets.
            // This can be fixed by fixing the sprite textures for the blocks at some point. Then the offsets can be removed.
            PositionVector = _previousPosition;
            PositionRect = new Rectangle((int)_previousPosition.X - 32, (int)_previousPosition.Y - 32,
                                         PositionRect.Width, PositionRect.Height);

            // Can add reduce health logic here later; trigger damage animation
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
