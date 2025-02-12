using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public abstract class Item : IItem
    {
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        
        public void Update()
        {
            Position = new Vector2(Position.X + Speed, Position.Y);
        }

        public void Draw(ISprite isprite, SpriteBatch spriteBatch) { }

    }
}
