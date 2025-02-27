using System.Collections.Generic;
using Project.Blocks;

namespace Project.Packages.Characters
{
    class CollisionManager
    {
        public void UpdateCollisions(Player player, List<SolidBlock> blocks)
        {
            foreach (var block in blocks)
            {
                if (player.PositionRect.Intersects(block.BoundingBox))
                {
                    var collisionCmd = new PlayerBlockCollisionCommand(player, block);
                    collisionCmd.Execute();
                }
            }
        }

    }

}
