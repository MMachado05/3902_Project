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
        int currentRoomIndex;
        int prevRoomIndex;


        float elapsedTime;
        EnemyManager enemyManager;
        RoomsManager roomsManager;
        CollisionManager CollisionManager;

        Player Player1;
        KeyboardState input;
        int playerIndex;

        private EnemyCollisionManager enemyCollisionManager;
        public Renderer(RoomsManager roomsManager, EnemyManager enemyManager, CollisionManager collisionManager)
        {
            itemList = roomsManager.GetCurrentRoom().roomMap();
            playerIndex = roomsManager.GetCurrentRoom().getPlayerIndex();
            Player1 = (Player)itemList[playerIndex];
            this.roomsManager = roomsManager;
            currentRoomIndex = roomsManager.currentRoomIndex;
            prevRoomIndex = currentRoomIndex;
            this.enemyManager = enemyManager;
            this.CollisionManager = collisionManager;
            // Boggus notes: IOn draw, just call Draw() in everything in the item list
            // If you *don't* want to draw something, just have an empty implementation
            // of Draw()
            enemyCollisionManager = new EnemyCollisionManager();
        }
        // NOTE: From Boggus: consider using 2D Array or Graph data structure
        // to store rooms,  so as to have built-in adjacency

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

            input = Keyboard.GetState();
            if (!(input.IsKeyDown(Keys.W) || input.IsKeyDown(Keys.A) || input.IsKeyDown(Keys.S) || input.IsKeyDown(Keys.D)) && Player1.Sprite.State != SpriteState.Attacking)
            {
                Player1.SetStaticSprite(); // Set idle sprite
            }

            Player1.Update(gameTime);

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25)
            {
                enemyManager.GetCurrentEnemy().UpdateAnimation(gameTime);
                elapsedTime = 0f;
            }

            enemyManager.GetCurrentEnemy().UpdateState(gameTime);
            currentRoomIndex = roomsManager.currentRoomIndex;

            if (currentRoomIndex != prevRoomIndex)
            {
                itemList = roomsManager.GetCurrentRoom().roomMap();

                prevRoomIndex = currentRoomIndex;
            }
            CollisionManager.UpdateCollisions(Player1, roomsManager.GetCurrentRoom().BlocksList);

            enemyCollisionManager.UpdateEnemyCollisions(enemyManager.GetCurrentEnemy(), roomsManager.GetCurrentRoom().BlocksList); // Handle enemy collisions

        }
    }
}
