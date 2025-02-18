using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public class Heart : Item
    {
        public Heart(ISprite isprite) : base(isprite)
        {
            Position = new Vector2(50, 50);
            Speed = 0;
        }
    }
}
