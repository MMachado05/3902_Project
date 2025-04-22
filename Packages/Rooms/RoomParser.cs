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
using Project.Rooms.Blocks.ConcreteClasses;

namespace Project.Rooms
{
    public class RoomParser
    {
        public IRoom LoadRoom(string filePath, GameRenderer gr,
             ContentManager content, int tileWidth, int tileHeight, CollisionManager collisionManager)
        {
            DoorBlock door;
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
            IBlock background = SolidBlockFactory.Instance.GreenBg();

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
            string songName = "";

            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                // Check for room metadata
                if (items[0].Equals("song"))
                {
                    songName = items[1];
                    System.Console.WriteLine(line);
                }
                if (items[0].Equals("type"))
                {
                    background = CreateBackground(items[1]);
                    System.Console.WriteLine(line);
                }

                else
                {
                    for (int x = 0; x < items.Length; x++)
                    {
                        switch (items[x])
                        {
                            case "bl":
                                internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "CreateBricks");
                                break;
                            case "ob":
                                internalMap[x, y] = AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "CreateWoodPlanks");
                                break;
                            case "drR":
                                door = (DoorBlock)AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "CreateRightDoor");
                                internalMap[x, y] = door;
                                break;
                            case "drL":
                                door = (DoorBlock)AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "CreateLeftDoor");
                                internalMap[x, y] = door;
                                break;
                            case "drT":
                                door = (DoorBlock)AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "CreateTopDoor");
                                internalMap[x, y] = door;
                                break;
                            case "drD":
                                door = (DoorBlock)AddBlockToRoom(x, y, gr, tileWidth, tileHeight, "CreateDownDoor");
                                internalMap[x, y] = door;
                                break;
                            case "pl":
                                playerSpriteLocation.X = x * gr.TileWidth;
                                playerSpriteLocation.Y = (y - 1) * gr.TileHeight;
                                break;
                            case "aq":

                                enemyManager.AddEnemy(new Aquamentus(new Rectangle(x * gr.TileWidth, (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight)));
                                break;
                            case "rg":

                                enemyManager.AddEnemy(new RedGoriya(new Rectangle(x * gr.TileWidth, (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight)));
                                break;
                            case "st":

                                enemyManager.AddEnemy(new Stalfos(new Rectangle(x * gr.TileWidth, (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight)));
                                break;
                            case "it":

                                itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateHeartSprite()));
                                break;
                            case "ar":

                                itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateArrowSprite()));
                                break;
                            case "sw":

                                itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateSwordSprite()));
                                break;
                            case "co":

                                itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateCoinSprite()));
                                break;
                            case "bm":

                                itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateBombSprite()));
                                break;
                            case "bmr":

                                itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateBoomerangSprite()));
                                break;
                            case "bw":

                                itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateBowSprite()));
                                break;
                            case "frb":

                                itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateFireballSprite()));
                                break;
                            case "ky":

                                itemManager.addItem(new StationaryItem(new Rectangle(x * gr.TileWidth, (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight), 0, ItemFactory.Instance.CreateKeySprite()));
                                break;
                            default:
                                break;
                        }
                    }
                    y++;
                }
            }

            return new BaseRoom(collisionManager, itemManager, enemyManager, playerSpriteLocation, internalMap, background, songName);
        }

        private IBlock CreateBackground(string bgName)
        {
            IBlock result;
            switch (bgName)
            {
                case "room1":
                    result = SolidBlockFactory.Instance.room1Background();
                    break;
                case "room2":
                    result = SolidBlockFactory.Instance.room2Background();
                    break;
                case "room3":
                    result = SolidBlockFactory.Instance.room3Background();
                    break;
                case "room4":
                    result = SolidBlockFactory.Instance.room4Background();
                    break;
                case "room5":
                    result = SolidBlockFactory.Instance.room5Background();
                    break;
                case "room6":
                    result = SolidBlockFactory.Instance.room6Background();
                    break;
                case "room7":
                    result = SolidBlockFactory.Instance.room7Background();
                    break;
                case "room8":
                    result = SolidBlockFactory.Instance.room8Background();
                    break;
                case "boss":
                    result = SolidBlockFactory.Instance.boosBackground();
                    break;
                default:
                    result = null;
                    break;
            }
            System.Console.WriteLine(bgName);
            return result;
        }

        private IBlock AddBlockToRoom(int x, int y, GameRenderer gr, int width, int hieght, String blockName)
        {
            IBlock result;
            switch (blockName)
            {
                case "CreateBricks":
                    result = SolidBlockFactory.Instance.CreateBricks(1, 1,
                                       new Rectangle(x * gr.TileWidth,
                                         (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight));

                    break;
                case "CreateWoodPlanks":
                    result = SolidBlockFactory.Instance.CreateWoodPlanks(1, 1,
                                       new Rectangle(x * gr.TileWidth,
                                         (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight));
                    break;
                case "CreateRightDoor":
                    result = SolidBlockFactory.Instance.CreateRightDoor(
                                      new Rectangle(x * gr.TileWidth,
                                        (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight));
                    break;
                case "CreateTopDoor":
                    result = SolidBlockFactory.Instance.CreateTopDoor(
                                      new Rectangle(x * gr.TileWidth,
                                        (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight));
                    break;
                case "CreateDownDoor":
                    result = SolidBlockFactory.Instance.CreateBottomDoor(
                                      new Rectangle(x * gr.TileWidth,
                                        (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight));
                    break;
                case "CreateLeftDoor":
                    result = SolidBlockFactory.Instance.CreateLeftDoor(
                                      new Rectangle(x * gr.TileWidth,
                                        (y - 1) * gr.TileHeight, gr.TileWidth, gr.TileHeight));
                    break;
                default:
                    result = null;
                    break;

            }
            return result;

        }
    }
}
