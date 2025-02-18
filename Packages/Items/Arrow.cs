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
            Position = new Vector2(100, 50);
            Speed = 5;
        }
    }
}
