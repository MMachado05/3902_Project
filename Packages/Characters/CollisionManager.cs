using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Blocks;

namespace Project.Packages.Characters
{
    // Needs to be public now for PlayerBlockCollisionCommand.cs
    public class CollisionManager
    {
        public void HandleBlockCollision(Player player, SolidBlock block)
        {
            // Reverting player to last safe position when colliding
            // NOTE: Because the sprite is not actually drawn exactly where the bounding box is, we have to do these weird offsets.
            // This can be fixed by fixing the sprite textures for the blocks at some point. Then the offsets can be removed.
            player.PositionVector = player.PreviousPosition;
            player.PositionRect = new Rectangle((int)player.PreviousPosition.X, (int)player.PreviousPosition.Y,
                                         player.PositionRect.Width, player.PositionRect.Height);

            // Can prob add reduce health logic here later; trigger damage animation
        }

        public void UpdateCollisions(Player player, List<SolidBlock> blocks)
        {
            foreach (var block in blocks)
            {
                if (player.PositionRect.Intersects(block.BoundingBox))
                {
                    ICommand collisionCommand = new PlayerBlockCollisionCommand(player, block, this);
                    collisionCommand.Execute();
                }
            }
        }

    }

}
