using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Packages.Characters;
using Project.Packages.Sounds;
using Project.Renderer;
using System.Collections.Generic;
using Project.Packages;
using Project;

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
        private int pendingRoomX;
        private int pendingRoomY;
        private IRoom pendingRoom;

        public Player player;
        private bool NoEnimes;
        public int CurrentRoomRow { get { return currentRoomX; } }
        public int CurrentRoomColumn { get { return currentRoomY; } }

        private Camera2D _camera = new Camera2D();

        private const int roomWidth = 960;
        private const int roomHeight = 720;

        private IRoom previousRoom;
        public IRoom PreviousRoom => previousRoom;
        public IRoom PendingRoom => pendingRoom;

        public RoomManager()
        {
            currentRoomX = currentRoomY = 0;
            pendingRoomX = currentRoomX;
            pendingRoomY = currentRoomY;
            this.roomParser = new RoomParser();
            this.collisionManager = new CollisionManager();
            NoEnimes = false;
        }

        public Matrix CameraTransform => _camera.Transform;
        public Camera2D Camera => _camera;

        public void LoadRoomsFromContent(ContentManager content, GameRenderer gr)
        {
            string pathPrefix = Environment.CurrentDirectory + "/Rooms/";
            string roomListPath = pathPrefix + "rooms.csv";
            if (!File.Exists(roomListPath))
            {
                pathPrefix = Environment.CurrentDirectory + "../../../../Rooms/";
                roomListPath = pathPrefix + "rooms.csv";
            }
            StreamReader roomListReader = new StreamReader(roomListPath);

            string roomLine;
            int mapWidth = 0;
            int mapHeight = 0;
            while ((roomLine = roomListReader.ReadLine()) != null)
            {
                mapHeight++;
                mapWidth = Math.Max(mapWidth, roomLine.Split(",").Length - 1);
            }

            roomListReader.DiscardBufferedData();
            roomListReader.BaseStream.Seek(0, SeekOrigin.Begin);

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
                    if (roomRow[i] != "-")
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
            this.player = player;
            for (int i = 0; i < this.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Map.GetLength(1); j++)
                {
                    if (this.Map[i, j] != null)
                        this.Map[i, j].AssignPlayer(player);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            _camera.Update(gameTime);

            if (!_camera.IsTransitioning && (pendingRoomX != currentRoomX || pendingRoomY != currentRoomY))
            {
                currentRoomX = pendingRoomX;
                currentRoomY = pendingRoomY;
                player.Location = pendingRoom.SavedPlayerLocation != Rectangle.Empty
                    ? pendingRoom.SavedPlayerLocation
                    : new Rectangle(10, 10, player.Location.Width, player.Location.Height);
            }

            IRoom Room = this.GetCurrentRoom();
            Room.Update(gameTime);

            if (this.currentRoomX == 2 && this.currentRoomY == 4)
                SoundEffectManager.Instance.playBossMusic();
            else
                SoundEffectManager.Instance.playDungeonMusic();

            if (Room.GetRoomName().Equals("boss") && Room.GetAllCurrentEnimeies().Count == 0)
                NoEnimes = true;
        }

        public bool IsThereEnmey() => NoEnimes;

        public void GotoRoomBelow()
        {
            var currentRoom = GetCurrentRoom();
            currentRoom.SavedPlayerLocation = new Rectangle(player.Location.X, player.Location.Y - 20, player.Location.Width, player.Location.Height);
            currentRoom.IsOnScreen = false;

            newY = currentRoomY + 1;
            if (newY < Map.GetLength(COLS) && Map[currentRoomX, newY] != null)
            {
                previousRoom = currentRoom;
                pendingRoomX = currentRoomX;
                pendingRoomY = newY;
                pendingRoom = Map[pendingRoomX, pendingRoomY];
                pendingRoom.AssignPlayer(player);
                player.Location = new Rectangle(player.Location.X, 10, player.Location.Width, player.Location.Height);
                _camera.StartTransition(new Vector2(0, -roomHeight));
            }
        }

        public void GotoRoomAbove()
        {
            var currentRoom = GetCurrentRoom();
            currentRoom.SavedPlayerLocation = new Rectangle(player.Location.X, player.Location.Y + 20, player.Location.Width, player.Location.Height);
            currentRoom.IsOnScreen = false;

            newY = currentRoomY - 1;
            if (newY >= 0 && Map[currentRoomX, newY] != null)
            {
                previousRoom = currentRoom;
                pendingRoomX = currentRoomX;
                pendingRoomY = newY;
                pendingRoom = Map[pendingRoomX, pendingRoomY];
                pendingRoom.AssignPlayer(player);
                player.Location = new Rectangle(player.Location.X, 640, player.Location.Width, player.Location.Height);
                _camera.StartTransition(new Vector2(0, roomHeight));
            }
        }

        public void GotoRoomToRight()
        {
            var currentRoom = GetCurrentRoom();
            currentRoom.SavedPlayerLocation = new Rectangle(player.Location.X - 20, player.Location.Y, player.Location.Width, player.Location.Height);
            currentRoom.IsOnScreen = false;

            newX = currentRoomX + 1;
            if (newX < Map.GetLength(ROWS) && Map[newX, currentRoomY] != null)
            {
                previousRoom = currentRoom;
                pendingRoomX = newX;
                pendingRoomY = currentRoomY;
                pendingRoom = Map[pendingRoomX, pendingRoomY];
                pendingRoom.AssignPlayer(player);
                player.Location = new Rectangle(10, player.Location.Y, player.Location.Width, player.Location.Height);
                _camera.StartTransition(new Vector2(-roomWidth, 0));
            }
        }

        public void GotoRoomToLeft()
        {
            var currentRoom = GetCurrentRoom();
            currentRoom.SavedPlayerLocation = new Rectangle(player.Location.X + 20, player.Location.Y, player.Location.Width, player.Location.Height);
            currentRoom.IsOnScreen = false;

            newX = currentRoomX - 1;
            if (newX >= 0 && Map[newX, currentRoomY] != null)
            {
                previousRoom = currentRoom;
                pendingRoomX = newX;
                pendingRoomY = currentRoomY;
                pendingRoom = Map[pendingRoomX, pendingRoomY];
                pendingRoom.AssignPlayer(player);
                player.Location = new Rectangle(900, player.Location.Y, player.Location.Width, player.Location.Height);
                _camera.StartTransition(new Vector2(roomWidth, 0));
            }
        }

        public IRoom GetCurrentRoom() => this.Map[this.currentRoomX, this.currentRoomY];
        public int GetCurrentRoomIndex() => (GetCurrentRoom() as BaseRoom)?.GetRoomIndex() ?? -1;
    }
}