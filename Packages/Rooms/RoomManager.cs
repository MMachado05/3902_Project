using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Packages.Characters;
using Project.Packages.Sounds;
using Project.Renderer;

namespace Project.Rooms
{
    public class RoomManager
    {
        private const int ROWS = 0;
        private const int COLS = 1;
        private int newY;
        private int newX;

        private RoomParser roomParser;
        private CollisionManager collisionManager;
        private IRoom[,] Map;
        private int currentRoomX;
        private int currentRoomY;
        private Player _player;

        public int CurrentRoomRow
        {
            get { return currentRoomX; }
        }
        public int CurrentRoomColumn
        {
            get { return currentRoomY; }
        }

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
                        this.Map[mapRoomX, mapRoomY] = roomParser.LoadRoom(
                            pathPrefix + roomRow[i],
                            gr,
                            content,
                            gr.TileWidth,
                            gr.TileHeight,
                            collisionManager
                        );
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
        {
            IRoom currentRoom = this.GetCurrentRoom();
            currentRoom.SavedPlayerLocation = new Rectangle(
                _player.Location.X,
                _player.Location.Y - 20,
                _player.Location.Width,
                _player.Location.Height
            );
            currentRoom.IsOnScreen = false;

            newY = currentRoomY + 1;

            if (newY < Map.GetLength(COLS) && Map[currentRoomX, newY] != null)
            {
                currentRoomY = newY;
                IRoom nextRoom = this.GetCurrentRoom();
                nextRoom.AssignPlayer(_player);

                if (nextRoom.SavedPlayerLocation != Rectangle.Empty)
                    _player.Location = nextRoom.SavedPlayerLocation;
                else
                    _player.Location = new Rectangle(
                        _player.Location.X,
                        10,
                        _player.Location.Width,
                        _player.Location.Height
                    );
            }
        }

        public void GotoRoomAbove()
        {
            IRoom currentRoom = this.GetCurrentRoom();
            currentRoom.SavedPlayerLocation = new Rectangle(
                _player.Location.X,
                _player.Location.Y + 20,
                _player.Location.Width,
                _player.Location.Height
            );
            currentRoom.IsOnScreen = false;

            newY = currentRoomY - 1;

            if (newY >= 0 && Map[currentRoomX, newY] != null)
            {
                currentRoomY = newY;
                IRoom nextRoom = this.GetCurrentRoom();
                nextRoom.AssignPlayer(_player);

                if (nextRoom.SavedPlayerLocation != Rectangle.Empty)
                    _player.Location = nextRoom.SavedPlayerLocation;
                else
                    _player.Location = new Rectangle(
                        _player.Location.X,
                        640,
                        _player.Location.Width,
                        _player.Location.Height
                    );
            }
        }

        public void GotoRoomToRight()
        {
            IRoom currentRoom = this.GetCurrentRoom();
            currentRoom.SavedPlayerLocation = new Rectangle(
                _player.Location.X - 20,
                _player.Location.Y,
                _player.Location.Width,
                _player.Location.Height
            );
            currentRoom.IsOnScreen = false;

            newX = currentRoomX + 1;

            if (newX < Map.GetLength(ROWS) && Map[newX, currentRoomY] != null)
            {
                currentRoomX = newX;
                IRoom nextRoom = this.GetCurrentRoom();
                nextRoom.AssignPlayer(_player);

                if (nextRoom.SavedPlayerLocation != Rectangle.Empty)
                    _player.Location = nextRoom.SavedPlayerLocation;
                else
                    _player.Location = new Rectangle(
                        10,
                        _player.Location.Y,
                        _player.Location.Width,
                        _player.Location.Height
                    );
            }
        }

        public void GotoRoomToLeft()
        {
            IRoom currentRoom = this.GetCurrentRoom();
            currentRoom.SavedPlayerLocation = new Rectangle(
                _player.Location.X + 20,
                _player.Location.Y,
                _player.Location.Width,
                _player.Location.Height
            );
            currentRoom.IsOnScreen = false;

            newX = currentRoomX - 1;

            if (newX >= 0 && Map[newX, currentRoomY] != null)
            {
                currentRoomX = newX;
                IRoom nextRoom = this.GetCurrentRoom();
                nextRoom.AssignPlayer(_player);

                if (nextRoom.SavedPlayerLocation != Rectangle.Empty)
                    _player.Location = nextRoom.SavedPlayerLocation;
                else
                    _player.Location = new Rectangle(
                        900,
                        _player.Location.Y,
                        _player.Location.Width,
                        _player.Location.Height
                    );
            }
        }

        public IRoom GetCurrentRoom()
        {
            return this.Map[this.currentRoomX, this.currentRoomY];
        }
    }
}
