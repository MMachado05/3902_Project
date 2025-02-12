using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public abstract class Item : IItem
    {
        public Vector2 Position { get; private set; }
        public float Speed { get; set; }

        

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void Update()
        {

        }

    
    }
}
