using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public class Arrow : Item
    {
        public Arrow(ISprite isprite) : base(isprite)
        {
            Position = new Vector2(100, 10);
            Speed = 3;
            //ItemFactory itemFactory = new ItemFactory();
            //isprite = itemFactory.arrowSprite;
        }
        
        public void Draw(ISprite isprite, SpriteBatch spriteBatch)
        {
            isprite.Draw(spriteBatch, Position);
        }

    }
}
