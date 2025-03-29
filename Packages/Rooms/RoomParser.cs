using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Project.Enemies;
using Project.Packages;
using Project.Enemies.EnemyClasses;
using Project.Packages.Characters;
using Project.Renderer;
using Project.Rooms.Blocks;
using Project.Items;
using Project.Factories;

namespace Project.Rooms
{
    public class RoomParser
    {
        public IRoom LoadRoom(string filePath, GameRenderer gr,
             ContentManager content, int tileWidth, int tileHeight, CollisionManager collisionManager)
        {
            StreamReader reader = new(filePath);
            string line;
            int roomWidth = 0;
            int roomHeight = 0;

            // Create dimensions for loaded room
            while ((line = reader.ReadLine()) != null)
            {
                roomWidth = Math.Max(roomWidth, line.Split(",").Length);
                roomHeight++;
            }
            IBlock[,] internalMap = new IBlock[roomWidth, roomHeight];

            // Restart reader
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            // Create block objects for room
            int y = 0;

            EnemyManager enemyManager = new EnemyManager();
            ItemManager itemManager = new ItemManager();

            Rectangle playerSpriteLocation = new Rectangle();
            playerSpriteLocation.Width = tileWidth;
            playerSpriteLocation.Height = tileHeight;

            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');
                for (int x = 0; x < items.Length; x++)
                {
                    switch (items[x])
                    {
                        case "bl":
                            internalMap[x, y] =
                              SolidBlockFactory.Instance.CreateBricks(1, 1,
                                  new Rectangle(x * gr.TileWidth,
                                    y * gr.TileHeight, gr.TileWidth, gr.TileHeight));
                            break;
                        case "ob":
                            internalMap[x, y] =
                              SolidBlockFactory.Instance.CreateWoodPlanks(1, 1,
                                  new Rectangle(x * gr.TileWidth,
                                    y * gr.TileHeight, gr.TileWidth, gr.TileHeight));
                            break;
                        case "dr":
                            internalMap[x, y] =
                              SolidBlockFactory.Instance.CreateDoor(
                                  new Rectangle(x * gr.TileWidth,
                                    y * gr.TileHeight, gr.TileWidth, gr.TileHeight));
                            break;
                        case "pl":
                            playerSpriteLocation.X = x * gr.TileWidth;
                            playerSpriteLocation.Y = y * gr.TileHeight;
                            break;
                        case "en":
                            // TODO: We should change the convention here to explicitly
                            // express *which* enemy is being spawned. We'll add it to 
                            // the enemy manager that'll get passed into the room.
                            enemyManager.AddEnemy(new Aquamentus(new Rectangle(x * gr.TileWidth, y * gr.TileHeight, gr.TileWidth, gr.TileHeight)));
                            break;
                        case "it":
                            itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, y * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateHeartSprite()));
                            break;
                        default:
                            break;
                    }
                }
                y++;
            }


            return new BaseRoom(collisionManager, itemManager, enemyManager, playerSpriteLocation, internalMap);
        }
    }
}
