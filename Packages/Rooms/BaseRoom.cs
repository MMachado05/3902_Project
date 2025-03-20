using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Enemies;
using Project.rooms;
using Project.Blocks;
using Project.Enemies.EnemyClasses;
namespace Project.Packages
{
    public class BaseRoom : IRoom
    {
        // Per-room entity managers
        private SolidBlockManager manager;
        private EnemyManager _enemyManager;

        // Logistic fields
        private Dictionary<Vector2, String> internalRep;
        Game1 game;
        List<object> result;
        public List<object> BlocksList { get; set; }
        int playerIndex;
        int counter = 0;

        public BaseRoom(SolidBlockManager manager, EnemyManager enemyManager, Game1 game,
            Dictionary<Vector2, String> internalRep)
        {
            _enemyManager = enemyManager;
            this.game = game;
            this.manager = manager;
            this.internalRep = internalRep;
        }
        public List<Object> roomMap()
        {
            result = new List<object>();
            BlocksList = new List<object>();
            foreach (var item in internalRep)
            {
                Rectangle dest;
                SolidBlock block;

                switch (item.Value)
                {
                    case "bl":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        block = manager.boardersBrick(dest);
                        result.Add(block);
                        BlocksList.Add(block);
                        break;
                    case "ob":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        block = manager.obstacleBlock(dest);
                        result.Add(block);
                        BlocksList.Add(block);

                        break;
                    case "dr":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        block = manager.doorBlock(dest);
                        result.Add(block);
                        BlocksList.Add(block);

                        break;
                    case "en":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        List<Enemy> enemyList = new List<Enemy> { new Aquamentus(new Vector2(dest.X, dest.Y)), new RedGoriya(new Vector2(dest.X, dest.Y)), new Stalfos(new Vector2(dest.X, dest.Y)) };
                        _enemyManager.addEnemy(enemyList);
                        result.Add(enemyList);
                        break;
                    case "pl":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        game.player.PositionRect = dest;
                        game.player.PositionVector = new Vector2(dest.X, dest.Y);
                        result.Add(game.player);
                        playerIndex = counter;
                        break;
                }
                counter++;

                // NOTE: From Boggus, we need magic strings and lots of loops in order
                // to pull off parsers, so while it does have code smells, we won't
                // concern ourselves too much
            }
            return result;
        }
        public int getPlayerIndex()
        {
            return playerIndex;
        }


    }
}
