using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Project.Blocks;
using Project.Enemies;
using Project.Enemies.EnemyClasses;
using Project.Packages;

namespace Project.rooms
{
    public class LevelOneRoom : BaseRoom
    {
        public LevelOneRoom(SolidBlockManager manager,EnemyManager enemyManager,Game1 game) : base(manager,enemyManager,game)
        {
        }
    }
}