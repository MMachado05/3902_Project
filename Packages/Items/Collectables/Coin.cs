using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Sprites;

namespace Project.Items
{
    public class Coin : Item
    {
        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }
        public override int PlayerHealthEffect { get => 2; }
        public Coin(Rectangle position, ISprite sprite) : base(sprite)
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
