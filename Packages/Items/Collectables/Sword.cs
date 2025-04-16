using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Sprites;

namespace Project.Items
{
    public class Sword : Item
    {
        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }
        public Sword(Rectangle position, ISprite sprite) : base(sprite)
        {
            Location = position;
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public override void CollideWith(IGameObject collider)
        {
            if (collider is Player)
            {
                ToBeDeleted = true;
            }
        }
    }
}
