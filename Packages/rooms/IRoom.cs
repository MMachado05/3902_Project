using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;

namespace Project.rooms
{
    public interface IRoom
    {
    public List<Object> roomMap();
     public int getPlayerIndex();
    }
}