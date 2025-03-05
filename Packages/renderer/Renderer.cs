using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Blocks;
using Project.Enemies;
using Project.Enemies.EnemyClasses;
using Project.renderer;
using Project.rooms;

namespace Project.renderer
{
    public class Renderer : IRenderer
    {
        RoomsManager roomsManager;
        List<object> itemList;
        float elapsedTime;
        EnemyManager enemyManager;

        public Renderer(RoomsManager roomsManager)
        {
            itemList = roomsManager.GetCurrentRoom().roomMap();


        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (var items in itemList)
            {
                switch (items)
                {
                    case SolidBlock solidBlock:
                        solidBlock.Draw();
                        break;
                    case Player player:
                        player.Draw(spriteBatch);
                        break;
                    case IItem item:
                        Console.WriteLine("I");

                        // nothing 
                        break;
                    case Enemy enemy:
                        // nothing 
                        break;
                    default:

                        break;
                }

            }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}