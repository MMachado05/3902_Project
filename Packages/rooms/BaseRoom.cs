using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Enemies;
using Project.rooms;
using Project.Blocks;
using System.Diagnostics;
using Project.Enemies.EnemyClasses;
using System.ComponentModel.DataAnnotations;
namespace Project.Packages
{
    public abstract class BaseRoom : IRoom
    {
        private SolidBlockManager manager;
        EnemyManager _enemyManager ;

        public Dictionary<Vector2, String> room;
        public RoomParser parser;
        Player Player;
        Game1 game;
        Enemy enemy;
        float  elapsedTime;
         List<object> itemList;
         List<object> result;
         Enemy enemies;
        public BaseRoom(SolidBlockManager manager,EnemyManager enemyManager,Game1 game)
        {
            _enemyManager = enemyManager;
            this.game = game;
            this.manager = manager;
            parser = new RoomParser();
            room = parser.loadRoom("../../../Data/room1.csv");

        }
        public List<Object> roomMap()
        {
            result = new List<object>();
            foreach (var item in room)
            {
                Rectangle dest;
                SolidBlock block;

                switch (item.Value)
                {
                    case "bl":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        block = manager.boardersBrick(dest);
                        result.Add(block);
                        break;
                    case "ob":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        block = manager.obstacleBlock(dest);
                        result.Add(block);
                        break;
                    case "dr":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        block = manager.doorBlock(dest);
                        result.Add(block);
                        break;
                    case "en":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        _enemyManager.addEnemy(new Aquamentus(new Vector2(dest.X,dest.Y)));
                        _enemyManager.addEnemy(new RedGoriya(new Vector2(dest.X,dest.Y)));
                        _enemyManager.addEnemy(new Stalfos(new Vector2(dest.X,dest.Y)));
                        break;
                    case "pl":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                       // Player = new Player(new Vector2(dest.X,dest.Y));
                        result.Add(Player);
                        break;
                }

            }
            return result;
        }
        /*
        public void Draw()
        {
             itemList = roomMap();

            foreach (var items in itemList)
            {
                switch (items)
                {
                    case SolidBlock solidBlock:
                        solidBlock.Draw();
                        break;
                    case Player player:
                        player.Draw(game._spriteBatch);
                        break;
                    case IItem item:
                        Console.WriteLine("I");

                        // nothing 
                        break;
                    case EnemyManager enemy:
                        enemy.GetCurrentEnemy().Draw(game._spriteBatch);
                        
                        Console.WriteLine("E");

                        // nothing
                        break;
                    default:

                        break;
                }

            }

        }*/
        
      
      
    }
}