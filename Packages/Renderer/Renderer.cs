using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        int prevRoomIndex;

        float elapsedTime;
        EnemyManager enemyManager;
        RoomManager roomManager;
        CollisionManager CollisionManager;

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
            // Boggus notes: IOn draw, just call Draw() in everything in the item list
            // If you *don't* want to draw something, just have an empty implementation
            // of Draw()
            enemyCollisionManager = new EnemyCollisionManager();
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
            // CollisionManager.UpdateCollisions(Player1, roomsManager);

            Player1.Update(gameTime);

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25)
            {
                enemyManager.GetCurrentEnemy().UpdateAnimation(gameTime);
                elapsedTime = 0f;
            }

            enemyManager.GetCurrentEnemy().UpdateState(gameTime);
            itemList = roomManager.GetCurrentRoom().roomMap();
            CollisionManager.UpdateCollisions(Player1, roomManager.GetCurrentRoom().BlocksList);

            enemyCollisionManager.UpdateEnemyCollisions(enemyManager.GetCurrentEnemy(), roomManager.GetCurrentRoom().BlocksList); // Handle enemy collisions
        }
    }
}
