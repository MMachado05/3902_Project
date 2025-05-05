using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Project.Characters;
using Project.Commands;
using Project.Factories;
using Project.Packages.Sounds;
using Project.Rooms;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Project.Packages;

namespace Project.Renderer
{
    public class GameRenderer : IRenderer
    {
        private Player _playerCharacter;
        public Player PlayerCharacter { set => _playerCharacter = value; }

        private RoomManager _roomManager;
        public RoomManager RoomManager { set => _roomManager = value; }

        private bool _fieldsSatisfied;
        private GameStateMachine _gameState;

        private int _screenWidth;
        private int _screenHeight;
        private int tileWidth;
        private int tileHeight;

        public int TileWidth => tileWidth;
        public int TileHeight => tileHeight;

        private MapRenderer _mapRenderer;
        private Texture2D _pixelTex;

        public GameRenderer(int screenWidth, int screenHeight, int tileWidth, int tileHeight, GameStateMachine gameState)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            _fieldsSatisfied = false;
            _gameState = gameState;
            InitializePauseMenuUI();
        }

        public void SetMapRenderer(MapRenderer mapRenderer) => _mapRenderer = mapRenderer;
        public void SetMapRoomIndex(int index) => _mapRenderer?.SetRoomIndex(index);

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_fieldsSatisfied)
            {
                if (_playerCharacter == null || _roomManager == null)
                    return;
                SatisfyFields();
            }

            var camera = _roomManager.Camera;
            var offset = camera.Position;
            var target = camera.Target;

            if (camera.IsTransitioning && _roomManager.PreviousRoom != null)
            {
                Vector2 transitionOffset = Vector2.Lerp(Vector2.Zero, camera.Target, camera.TransitionProgress);

                // Slide previous room OUT (in direction of movement)
                spriteBatch.Begin(samplerState: SamplerState.PointClamp,
                                  transformMatrix: Matrix.CreateTranslation(transitionOffset.X, transitionOffset.Y + 96, 0));
                (_roomManager.PreviousRoom as BaseRoom)?.Draw(spriteBatch, Matrix.Identity);
                spriteBatch.End();

                // Slide pending room IN (from opposite direction)
                spriteBatch.Begin(samplerState: SamplerState.PointClamp,
                                  transformMatrix: Matrix.CreateTranslation(transitionOffset.X - camera.Target.X,
                                                                            transitionOffset.Y - camera.Target.Y + 96, 0));
                (_roomManager.PendingRoom as BaseRoom)?.Draw(spriteBatch, Matrix.Identity);
                spriteBatch.End();
            }




            else
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(0, 96, 0));
                (_roomManager.GetCurrentRoom() as BaseRoom)?.Draw(spriteBatch, Matrix.Identity);
                spriteBatch.End();
            }
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            var healthBar = HealthBarSpriteFactory.Instance.HealthBarSprite(_playerCharacter.health);
            healthBar.Draw(spriteBatch, new Rectangle(64, 8, 256, 64));

            var inventory = _playerCharacter._inventory;
            if (inventory?.Items != null && inventory.Items.Count > 0)
            {
                int itemSize = 48;
                int startX = 450;
                int startY = 16;
                int index = 0;
                foreach (var kvp in inventory.Items)
                {
                    if (index == inventory.ActiveSlot)
                    {
                        var item = kvp.Key;
                        var drawRect = new Rectangle(startX, startY, itemSize, itemSize);

                        if (_pixelTex == null)
                        {
                            _pixelTex = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                            _pixelTex.SetData(new[] { Color.White });
                        }

                        Color borderColor = Color.CornflowerBlue;
                        int thickness = 2;

                        spriteBatch.Draw(_pixelTex, new Rectangle(drawRect.X, drawRect.Y, drawRect.Width, thickness), borderColor);
                        spriteBatch.Draw(_pixelTex, new Rectangle(drawRect.X, drawRect.Y + drawRect.Height - thickness, drawRect.Width, thickness), borderColor);
                        spriteBatch.Draw(_pixelTex, new Rectangle(drawRect.X, drawRect.Y, thickness, drawRect.Height), borderColor);
                        spriteBatch.Draw(_pixelTex, new Rectangle(drawRect.X + drawRect.Width - thickness, drawRect.Y, thickness, drawRect.Height), borderColor);

                        var originalLocation = item.Location;
                        item.Location = drawRect;
                        item.Draw(spriteBatch);
                        item.Location = originalLocation;

                        Vector2 center = new Vector2(650, 40);
                        float size = 20f;

                        Vector2 p1, p2, p3;
                        switch (_playerCharacter.LastActiveDirection)
                        {
                            case Direction.Up:
                                p1 = center + new Vector2(0, -size);
                                p2 = center + new Vector2(-size, size);
                                p3 = center + new Vector2(size, size);
                                break;
                            case Direction.Down:
                                p1 = center + new Vector2(0, size);
                                p2 = center + new Vector2(-size, -size);
                                p3 = center + new Vector2(size, -size);
                                break;
                            case Direction.Left:
                                p1 = center + new Vector2(-size, 0);
                                p2 = center + new Vector2(size, -size);
                                p3 = center + new Vector2(size, size);
                                break;
                            case Direction.Right:
                                p1 = center + new Vector2(size, 0);
                                p2 = center + new Vector2(-size, -size);
                                p3 = center + new Vector2(-size, size);
                                break;
                            default:
                                p1 = center;
                                p2 = center;
                                p3 = center;
                                break;
                        }

                        DrawTriangle(spriteBatch, p1, p2, p3, Color.Red);
                        break;
                    }
                    index++;
                }
            }

            _mapRenderer?.Draw(spriteBatch);

            spriteBatch.End();
        }


        private void DrawTriangle(SpriteBatch spriteBatch, Vector2 p1, Vector2 p2, Vector2 p3, Color color)
        {
            void DrawLine(Vector2 a, Vector2 b)
            {
                Vector2 edge = b - a;
                float angle = (float)System.Math.Atan2(edge.Y, edge.X);
                spriteBatch.Draw(_pixelTex, a, null, color, angle, Vector2.Zero, new Vector2(edge.Length(), 2f), SpriteEffects.None, 0);
            }

            DrawLine(p1, p2);
            DrawLine(p2, p3);
            DrawLine(p3, p1);
        }

        public void Update(GameTime gameTime) => _mapRenderer?.SetRoomIndex(_roomManager?.GetCurrentRoomIndex() ?? 0);

        public void LoadContent(ContentManager content) { }

        private void SatisfyFields()
        {
            if (_playerCharacter == null)
                throw new System.NullReferenceException("Player property must be set.");
            if (_roomManager == null)
                throw new System.NullReferenceException("RoomManager property must be set.");
            _fieldsSatisfied = true;
        }

        private void InitializePauseMenuUI() { }
    }
}
