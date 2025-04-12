using System;
using System.IO;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Commands.RoomCommands;
using Project.Controllers;
using Project.Packages;
using Project.Packages.Characters;
using Project.Packages.Sounds;
using Project.Renderer;
using Project.Rooms.Blocks;

namespace Project.Rooms
{
    public class RoomManager
    {
        private const int ROWS = 0;
        private const int COLS = 1;
        int newY;
        int newX;

        private RoomParser roomParser;
        private CollisionManager collisionManager;
        private IRoom[,] Map;
        private int currentRoomX;
        private int currentRoomY;
        Player _player;

        public int CurrentRoomRow { get { return currentRoomX; } }
        public int CurrentRoomColumn { get { return currentRoomY; } }

        /// <summary>
        /// Be sure to run LoadRoomsFromContent before calling any other methods.
        /// </summary>
        public RoomManager()
        {
            currentRoomX = currentRoomY = 0;

            this.roomParser = new RoomParser();
            this.collisionManager = new CollisionManager();
        }

        public void LoadRoomsFromContent(ContentManager content, GameRenderer gr)
        {
            // Create reader for the list of all rooms
            string pathPrefix = Environment.CurrentDirectory + "/Rooms/";
            string roomListPath = pathPrefix + "rooms.csv";
            if (!File.Exists(roomListPath))
            {
                pathPrefix = Environment.CurrentDirectory + "../../../../Rooms/";
                roomListPath = pathPrefix + "rooms.csv";
            }
            StreamReader roomListReader = new StreamReader(roomListPath);

            // Get "dimensions" of CSV map to create room array
            string roomLine;
            int mapWidth = 0;
            int mapHeight = 0;
            while ((roomLine = roomListReader.ReadLine()) != null)
            {
                mapHeight++;
                mapWidth = Math.Max(mapWidth, roomLine.Split(",").Length - 1);
            }

            // Restart reader
            roomListReader.DiscardBufferedData();
            roomListReader.BaseStream.Seek(0, SeekOrigin.Begin);

            // Add rooms to map from CSV
            this.Map = new IRoom[mapWidth, mapHeight];
            string[] roomRow;
            int mapRoomX = 0;
            int mapRoomY = 0;

            while ((roomLine = roomListReader.ReadLine()) != null)
            {
                roomRow = roomLine.Split(",");
                mapRoomX = 0;
                for (int i = 0; i < roomRow.Length - 1; i++)
                {
                    if (roomRow[i] != "-") // Ignore "rooms" that don't exist
                    {
                        this.Map[mapRoomX, mapRoomY] =
                          roomParser.LoadRoom(pathPrefix + roomRow[i],
                              gr, content, gr.TileWidth, gr.TileHeight, collisionManager);
                    }
                    mapRoomX++;
                }
                mapRoomY++;
            }
        }

        public void AssignPlayer(Player player)
        {
            _player = player;
            for (int i = 0; i < this.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Map.GetLength(1); j++)
                {

                    if (this.Map[i, j] != null)
                        this.Map[i, j].AssignPlayer(player);

                }
            }
        }


        // TODO: Implement this; to be called by Renderer
        public void DrawCurrentRoom(SpriteBatch sb)
        {
            this.GetCurrentRoom().Draw(sb);
            // TODO: Draw enemies
            // TODO: Draw items
        }


        public void Update(GameTime gameTime)
        {
            this.GetCurrentRoom().Update(gameTime);

            if (this.currentRoomX == 2 && this.currentRoomY == 4)
            {
                SoundEffectManager.Instance.playBossMusic();
            }
            else
            {
                SoundEffectManager.Instance.playDungeonMusic();
            }
        }

        public void GotoRoomBelow()
        {/*
            this.GetCurrentRoom().IsOnScreen = false;
            Rectangle playerLocation = this.GetCurrentRoom().PlayerLocation;
            this.GetCurrentRoom().SavedPlayerLocation = new Rectangle(playerLocation.Location.X, playerLocation.Y, playerLocation.Width, playerLocation.Height);
            newY = this.currentRoomY + 1;
            if (newY < this.Map.GetLength(COLS) && this.Map[this.currentRoomX, newY] != null)
            {
                this.currentRoomY = newY;
            }/*/
            this.GetCurrentRoom().IsOnScreen = false;
            Rectangle playerLocation = this.GetCurrentRoom().PlayerLocation;
            newY = this.currentRoomY + 1;

            if (newY < this.Map.GetLength(COLS) && this.Map[this.currentRoomX, newY] != null)
            {
                this.currentRoomY = newY;
                this.GetCurrentRoom().AssignPlayer(_player);
                _player.Location = new Rectangle(playerLocation.X, 10, playerLocation.Width, playerLocation.Height); // top of the new room
            }
        }
        public void GotoRoomAbove()
        {
            /*
            this.GetCurrentRoom().IsOnScreen = false;
            Rectangle playerLocation = this.GetCurrentRoom().PlayerLocation;
            this.GetCurrentRoom().SavedPlayerLocation = new Rectangle(playerLocation.Location.X, playerLocation.Y , playerLocation.Width, playerLocation.Height);
            newY = this.currentRoomY - 1;

            // Check if we're out of bounds
            if (newY >= 0 && this.Map[this.currentRoomX, newY] != null)
            {
                this.currentRoomY = newY;
            }
            /*/
            this.GetCurrentRoom().IsOnScreen = false;
            Rectangle playerLocation = this.GetCurrentRoom().PlayerLocation;
            newY = this.currentRoomY - 1;

            if (newY >= 0 && this.Map[this.currentRoomX, newY] != null)
            {
                this.currentRoomY = newY;
                this.GetCurrentRoom().AssignPlayer(_player);
                _player.Location = new Rectangle(playerLocation.X, 640, playerLocation.Width, playerLocation.Height); // bottom of the new room
            }
        }
        public void GotoRoomToRight()
        {/*
            
            this.GetCurrentRoom().IsOnScreen = false;
            Rectangle playerLocation = this.GetCurrentRoom().PlayerLocation;
            this.GetCurrentRoom().SavedPlayerLocation = new Rectangle(playerLocation.Location.X, playerLocation.Y, playerLocation.Width, playerLocation.Height);
            newX = this.currentRoomX + 1;
            if (newX < this.Map.GetLength(ROWS) && this.Map[newX, this.currentRoomY] != null)
            {
                this.currentRoomX = newX;
            }*/
            this.GetCurrentRoom().IsOnScreen = false;
            Rectangle playerLocation = this.GetCurrentRoom().PlayerLocation;
            newX = this.currentRoomX + 1;

            if (newX < this.Map.GetLength(ROWS) && this.Map[newX, this.currentRoomY] != null)
            {
                this.currentRoomX = newX;
                this.GetCurrentRoom().AssignPlayer(_player);
                _player.Location = new Rectangle(10, playerLocation.Y, playerLocation.Width, playerLocation.Height); // move player just inside new room
            }
        }
        public void GotoRoomToLeft()
        {
            /*
            this.GetCurrentRoom().IsOnScreen = false;
            Rectangle playerLocation = this.GetCurrentRoom().PlayerLocation;
            this.GetCurrentRoom().SavedPlayerLocation = new Rectangle(playerLocation.Location.X, playerLocation.Y, playerLocation.Width, playerLocation.Height);
            newX = this.currentRoomX - 1;
            if (newX >= 0 && this.Map[newX, this.currentRoomY] != null)
            {
                this.currentRoomX = newX;
            }/*/
            this.GetCurrentRoom().IsOnScreen = false;
            Rectangle playerLocation = this.GetCurrentRoom().PlayerLocation;
            newX = this.currentRoomX - 1;

            if (newX >= 0 && this.Map[newX, this.currentRoomY] != null)
            {
                this.currentRoomX = newX;
                this.GetCurrentRoom().AssignPlayer(_player);
                _player.Location = new Rectangle(900, playerLocation.Y, playerLocation.Width, playerLocation.Height); // right side of the new room
            }
        }

        public IRoom GetCurrentRoom()
        {
            return this.Map[this.currentRoomX, this.currentRoomY];
        }


    }
}
