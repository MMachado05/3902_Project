using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public class Bomb : Item
    {
        public Bomb(ISprite isprite) : base(isprite)
        {
            Position = new Vector2(400, 100);
        }
    }   
}
