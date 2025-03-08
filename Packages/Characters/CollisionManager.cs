using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Blocks;
using Project.rooms;

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
            player.PositionRect = new Rectangle((int)player.PreviousPosition.X - 32, (int)player.PreviousPosition.Y - 32,
                                         player.PositionRect.Width, player.PositionRect.Height);

            // Can prob add reduce health logic here later; trigger damage animation
        }

        public void UpdateCollisions(Player player, List<object>solidBlocks)
        {
          // NOTE: From Boggus: One optimization; take adjacent blocks and "consolidate"
          // them when calculating collisions, so as to reducce the number of "objects"
          // we need to compare for intersections. E.g. A single wall would be one, long
          // rectangle, rather than various individual blocks all next to one another.
          // In that vein, create an object class (similar to blocks) that is just
          // a rectangular "block"
            
            foreach (var block in solidBlocks)
            {
                
                SolidBlock blockHolder = (SolidBlock)block;
                if (player.PositionRect.Intersects(blockHolder.BoundingBox))
                {
                    ICommand collisionCommand = new PlayerBlockCollisionCommand(player, blockHolder, this);
                    collisionCommand.Execute();
                }
            }
        }

    }

}

// NOTE: From Boggus: If you have a dictionary, you *could* have a single Collision Manager
// class, but otherwise, it's not a bad idea to have individual collision managers for each
// type of collision
