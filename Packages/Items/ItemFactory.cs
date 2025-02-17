using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public class ItemFactory
    {
        private Texture2D heartTexture;
        private Texture2D arrowTexture;

        public static ItemFactory instance = new ItemFactory();

        
        public static ItemFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public void LoadContent(ContentManager content)
        {
            this.heartTexture = content.Load<Texture2D>("test_item");
            this.arrowTexture = content.Load<Texture2D>("arrow");
            Console.WriteLine("it loaded");
        }
        public Arrow createArrow()
        {
            Console.WriteLine("it created arrow");
            ISprite arrowSprite = new StationarySprite(this.arrowTexture, new Rectangle(0, 0, 32, 32), 3, new SpriteState());
            return new Arrow(arrowSprite);
        }
        public HeartItem createHeart() {
            ISprite heartSprite = new AnimatedLoopSprite(this.heartTexture, new Rectangle(0, 0, 64, 64), 3, 4, new SpriteState());
            return new HeartItem(heartSprite);
        }
    }
}
