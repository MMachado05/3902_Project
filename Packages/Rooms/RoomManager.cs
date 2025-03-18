using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Project.Blocks;
using Project.Enemies;
using Project.Packages;

namespace Project.rooms
{
    public class RoomManager
    {
        // Temp variables before I complete the refactor
        private Game1 game;
        private SolidBlockManager blockManager;
        private EnemyManager enemyManager;

        private RoomParser roomParser;

        private IRoom[,] Map;
        private int currentRoomX;
        private int currentRoomY;

        private const int ROWS = 0;
        private const int COLS = 1;

        public RoomManager(SolidBlockManager manager, EnemyManager enemyManager, Game1 game)
        {
            currentRoomX = currentRoomY = 0;

            this.game = game;
            this.blockManager = manager;
            this.enemyManager = enemyManager;
            this.roomParser = new RoomParser();
        }

        public void LoadRoomsFromContent(ContentManager Content)
        {
            // Create reader for the list of all rooms
            string pathPrefix = Environment.CurrentDirectory + "/Rooms/";
            string roomListPath = pathPrefix + "rooms.csv";
            if (!File.Exists(roomListPath))
            {
                pathPrefix = Environment.CurrentDirectory + "/../../../../Rooms";
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
                          new BaseRoom(blockManager, enemyManager, game,
                              roomParser.loadRoom(pathPrefix + roomRow[i]));
                    }
                    mapRoomX++;
                }
                mapRoomY++;
            }
        }

        [System.Obsolete("2D array being used; need to refactor")]
        public void SwitchToPreviousRoom()
        {
            do
            {
                this.currentRoomX++;
                if (this.currentRoomX > this.Map.GetLength(ROWS))
                {
                    this.currentRoomX = 0;
                    this.currentRoomY++;
                    if (this.currentRoomY > this.Map.GetLength(COLS))
                        this.currentRoomY = 0;
                }
            } while (this.Map[currentRoomX, currentRoomY] == null);

            /*if (RoomsList.Count == 0) return;*/
            /*if (currentRoomIndex <= 0)*/
            /*    currentRoomIndex = RoomsList.Count - 1;*/
            /*else*/
            /*    currentRoomIndex--;*/
        }

        [System.Obsolete("2D array being used; need to refactor")]
        public void SwitchToNextRoom()
        {
            do
            {
                this.currentRoomX--;
                if (this.currentRoomX < 0)
                {
                    this.currentRoomX = this.Map.GetLength(ROWS) - 1;
                    this.currentRoomY--;
                    if (this.currentRoomY < 0)
                        this.currentRoomY = this.Map.GetLength(COLS) - 1;
                }
            } while (this.Map[currentRoomX, currentRoomY] == null);

            /*if (RoomsList.Count == 0) return;*/
            /*if (currentRoomIndex >= RoomsList.Count - 1)*/
            /*    currentRoomIndex = 0;*/
            /*else*/
            /*    currentRoomIndex++;*/
        }

        public IRoom GetCurrentRoom()
        {
            return this.Map[this.currentRoomX, this.currentRoomY];
        }

    }
}
