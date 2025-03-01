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
                    ICommand collisionCommand = new PlayerBlockCollisionCommand(player, block);
                    collisionCommand.Execute();
                }
            }
        }

    }

}
