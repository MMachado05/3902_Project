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
    public class Arrow : Item
    {
        public override Rectangle Location { get; set; }
        public Arrow(Rectangle position, ISprite sprite) : base(sprite)
        {
            Location = position;
        }

        public override void Update(GameTime gameTime) { 
            Location = new Rectangle(Location.X + 5, Location.Y, Location.Width, Location.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public override void CollideWith(IGameObject collider)
        {
            if (collider is Enemy || collider is SolidBlock)
            {
                ToBeDeleted = true;
            }
        }
    }
}
