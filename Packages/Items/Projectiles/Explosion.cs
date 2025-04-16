using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Enemies.EnemyClasses;
using Project.Factories;
using Project.Rooms.Blocks.ConcreteClasses;
using Project.Sprites;

namespace Project.Items
{
    public class Explosion : Item
    {
        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }
        private int TimeAlive; 
        public Explosion(Rectangle position, ISprite sprite) : base(sprite)
        {
            int width = position.Width;
            int height = position.Height;
            Location = new Rectangle(position.X, position.Y, width * 4, height * 4);
            TimeAlive = 1000;
        }

        public override void Update(GameTime gameTime) {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public override void CollideWith(IGameObject collider)
        { 
        }
    }
}
