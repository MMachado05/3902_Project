using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Blocks;
using Project.Enemies;
using Project.Enemies.EnemyClasses;
using Project.Packages.Characters;
using Project.rooms;

namespace Project.renderer
{
    public class Renderer : IRenderer
    {
        List<object> itemList;

        float elapsedTime;
        EnemyManager enemyManager;
        RoomManager roomManager;
        CollisionManager CollisionManager;

        private int[] currentRoomCoordinates;
        private int[] previousRoomCoordinates;

        Player Player1;
        int playerIndex;

        private EnemyCollisionManager enemyCollisionManager;
        public Renderer(RoomManager roomsManager, EnemyManager enemyManager, CollisionManager collisionManager)
        {
            itemList = roomsManager.GetCurrentRoom().roomMap();
            playerIndex = roomsManager.GetCurrentRoom().getPlayerIndex();
            Player1 = (Player)itemList[playerIndex];
            this.roomManager = roomsManager;
            this.enemyManager = enemyManager;
            this.CollisionManager = collisionManager;
            // NOTE: Boggus notes: In draw, just call Draw() in everything in the item list
            // If you *don't* want to draw something, just have an empty implementation
            // of Draw()
            enemyCollisionManager = new EnemyCollisionManager();
            this.currentRoomCoordinates = new int[2] { 0, 0 };
            this.previousRoomCoordinates = new int[2] { 0, 0 };
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (var items in itemList)
            {
                switch (items)
                {
                    case SolidBlock solidBlock:
                        solidBlock.Draw();
                        break;
                    case Player player:

                        player.Draw(spriteBatch);
                        break;
                    case IItem item:
                        Console.WriteLine("I");

                        // nothing 
                        break;
                    case List<Enemy> enemy:
                        //nothing for now, draw enemey in here in future

                        break;
                    default:

                        break;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            this.currentRoomCoordinates[0] = roomManager.CurrentRoomColumn;
            this.currentRoomCoordinates[1] = roomManager.CurrentRoomRow;

            Player1.Update(gameTime);

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25)
            {
                enemyManager.GetCurrentEnemy().UpdateAnimation(gameTime);
                elapsedTime = 0f;
            }

            if (!this.currentRoomCoordinates.Equals(this.previousRoomCoordinates))
            {
                itemList = roomManager.GetCurrentRoom().roomMap();
                Array.Copy(currentRoomCoordinates, previousRoomCoordinates, 2);
            }

            enemyManager.GetCurrentEnemy().UpdateState(gameTime);
            CollisionManager.UpdateCollisions(Player1, roomManager.GetCurrentRoom().BlocksList);

            enemyCollisionManager.UpdateEnemyCollisions(enemyManager.GetCurrentEnemy(), roomManager.GetCurrentRoom().BlocksList); // Handle enemy collisions
        }
    }
}
