using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Packages.Sounds;
using Project.Sprites;

namespace Project.Items
{
    public class Key : Item
    {
        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }
        public Key(Rectangle position, ISprite sprite) : base(sprite)
        {
            Location = position;
        }

        public override void Update(GameTime gameTime) { }

        public override void CollideWith(IGameObject collider)
        {
            if (collider is Player)
            {
                SoundEffectManager.Instance.playKeys();
                ToBeDeleted = true;
            }
        }
    }
}
