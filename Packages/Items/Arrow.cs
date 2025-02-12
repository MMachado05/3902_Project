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
        Arrow()
        {
            SetPosition(new Vector2(100, 10));
            Speed = 0;
        }   
    }
}
