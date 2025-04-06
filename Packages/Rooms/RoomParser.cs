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
using System.Diagnostics;

namespace Project.Rooms
{
    public class RoomParser
    {
        private IBlock AddBlockToRoom(int x, int y, GameRenderer gr, int width, int hieght, String blockName)
        {
            IBlock result;
            switch (blockName)
            {
                case "CreateBricks":
                    result = SolidBlockFactory.Instance.CreateBricks(1, 1,
                                       new Rectangle(x * gr.TileWidth,
                                         (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight));

                    break;
                case "CreateWoodPlanks":
                    result = SolidBlockFactory.Instance.CreateWoodPlanks(1, 1,
                                       new Rectangle(x * gr.TileWidth,
                                         (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight));
                    break;
                case "CreateDoor":
                    result = SolidBlockFactory.Instance.CreateDoor(
                                      new Rectangle(x * gr.TileWidth,
                                        (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight));
                    break;
                case "GreenBackGround":
                    result = SolidBlockFactory.Instance.GreenBg();
                    break;
                default:
                    result = null;
                    break;

            }
            return result;

        }
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
            IBlock Background = SolidBlockFactory.Instance.GreenBg();


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
                        case "gr":
                            Background = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");
                            break;
                        case "bl":

                            internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "CreateBricks");
                            break;
                        case "ob":
                            internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "CreateWoodPlanks");
                            break;
                        case "dr":
                            // internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "CreateDoor");
                            break;
                        case "pl":
                            //  internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");
                            playerSpriteLocation.X = x * gr.TileWidth;
                            playerSpriteLocation.Y = (y-1) * gr.TileHeight;
                            break;
                        case "aq":
                            //internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            enemyManager.AddEnemy(new Aquamentus(new Rectangle(x * gr.TileWidth, (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight)));
                            break;
                        case "rg":
                            //  internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            enemyManager.AddEnemy(new RedGoriya(new Rectangle(x * gr.TileWidth, (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight)));
                            break;
                        case "st":
                            // internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            enemyManager.AddEnemy(new Stalfos(new Rectangle(x * gr.TileWidth, (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight)));
                            break;
                        case "it":
                            // internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateHeartSprite()));
                            break;
                        case "ar":
                            // internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateArrowSprite()));
                            break;
                        case "sw":
                           // internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateSwordSprite()));
                            break;
                        case "co":
                            // internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateCoinSprite()));
                            break;
                        case "bm":
                            // internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateBombSprite()));
                            break;
                        case "bmr":
                            // internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateBoomerangSprite()));
                            break;
                        case "bw":
                            //internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateBowSprite()));
                            break;
                        case "frb":
                            //internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateFireballSprite()));
                            break;
                        case "ky":
                            // internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y-1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateKeySprite()));
                            break;
                        default:
                            // internalMap[x, y] = internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "GreenBackGround");

                            break;
                    }
                }
                y++;
            }


            return new BaseRoom(collisionManager, itemManager, enemyManager, playerSpriteLocation, internalMap , Background);
        }
    }
}
