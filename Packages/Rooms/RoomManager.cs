using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Enemies;
using Project.renderer;

namespace Project.rooms
{
    public class RoomManager
    {
        private const int ROWS = 0;
        private const int COLS = 1;

        // TODO: We'll want collision handlers in each room for enemies, and items

        private RoomParser roomParser;

        private IRoom[,] Map;
        private int currentRoomX;
        private int currentRoomY;

        public int CurrentRoomRow { get { return currentRoomX; } }
        public int CurrentRoomColumn { get { return currentRoomY; } }

        /// <summary>
        /// Be sure to run LoadRoomsFromContent before calling any other methods.
        /// </summary>
        public RoomManager(EnemyManager enemyManager)
        {
            currentRoomX = currentRoomY = 0;

            this.roomParser = new RoomParser();
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
                mapWidth = Math.Max(mapWidth, roomLine.Split(",").Length);
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
                for (int i = 0; i < roomRow.Length; i++)
                {
                    if (roomRow[i] != "") // Ignore "rooms" that don't exist
                    {
                        this.Map[mapRoomX, mapRoomY] =
                          roomParser.LoadRoom(pathPrefix + roomRow[i],
                              gr, content);
                    }
                    mapRoomX++;
                }
                mapRoomY++;
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
            // TODO: Implement this
        }

        public void GotoRoomBelow()
        {
            this.GetCurrentRoom().IsOnScreen = false;
            this.GetCurrentRoom().SavedPlayerLocation = this.GetCurrentRoom().PlayerLocation;

            this.currentRoomY++;
            if (this.currentRoomY >= this.Map.GetLength(COLS))
                this.currentRoomY = 0;
            // TODO: Save the current player location to the "default player location"
            // attribute in the room.
        }
        public void GotoRoomAbove()
        {
            this.GetCurrentRoom().IsOnScreen = false;
            this.GetCurrentRoom().SavedPlayerLocation = this.GetCurrentRoom().PlayerLocation;

            this.currentRoomY--;
            if (this.currentRoomY < 0)
                this.currentRoomY = this.Map.GetLength(COLS) - 1;
            // TODO: Save the current player location to the "default player location"
            // attribute in the room.
        }
        public void GotoRoomToRight()
        {
            this.GetCurrentRoom().IsOnScreen = false;
            this.GetCurrentRoom().SavedPlayerLocation = this.GetCurrentRoom().PlayerLocation;

            this.currentRoomX++;
            if (this.currentRoomX >= this.Map.GetLength(ROWS))
                this.currentRoomX = 0;
            // TODO: Save the current player location to the "default player location"
            // attribute in the room.
        }
        public void GotoRoomToLeft()
        {
            this.GetCurrentRoom().IsOnScreen = false;
            this.GetCurrentRoom().SavedPlayerLocation = this.GetCurrentRoom().PlayerLocation;

            this.currentRoomX--;
            if (this.currentRoomX < 0)
                this.currentRoomX = this.Map.GetLength(ROWS) - 1;
            // TODO: Save the current player location to the "default player location"
            // attribute in the room.
        }

        public IRoom GetCurrentRoom()
        {
            return this.Map[this.currentRoomX, this.currentRoomY];
        }
    }
}
