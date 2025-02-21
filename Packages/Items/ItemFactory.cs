using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public class ItemFactory
    {
        private Texture2D heartTexture;
        private Texture2D arrowTexture;
        private Texture2D bombTexture;
        private Texture2D swordTexture;
        private Texture2D bowTexture;

        private static ItemFactory instance = new ItemFactory();

        
        public static ItemFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public void LoadContent(ContentManager content)
        {
            this.heartTexture = content.Load<Texture2D>("heart");
            this.arrowTexture = content.Load<Texture2D>("arrow");
            this.bombTexture = content.Load<Texture2D>("bomb");
            this.swordTexture = content.Load<Texture2D>("sword");
            this.bowTexture = content.Load<Texture2D>("bow");

        }
        public Item createArrow()
        { 
            ISprite arrowSprite = new StationarySprite(this.arrowTexture, new Rectangle(0, 0, 32, 32), 3, new SpriteState());
            Item arrow = new Item(arrowSprite);
            arrow.Speed = 5;
            arrow.Position = new Vector2(500, 50);
            return arrow;
        }
        public Item createHeart() {
            ISprite heartSprite = new AnimatedLoopSprite(this.heartTexture, new Rectangle(0, 0, 13, 13), 3, 1, new SpriteState());
            Item heart = new Item(heartSprite);
            heart.Speed = 0;
            heart.Position = new Vector2(200, 300);
            return heart;
        }
        public Item createBomb()
        {
            ISprite bombSprite = new StationarySprite(this.bombTexture, new Rectangle(0, 0, 16, 16), 3, new SpriteState());
            Item bomb = new Item(bombSprite);
            bomb.Speed = 0;
            //replace later with player position
            bomb.Position = new Vector2(100, 100);
            return bomb;
        }
        public Item createSword()
        {
            ISprite swordSprite = new StationarySprite(this.swordTexture, new Rectangle(0, 0, 16, 16), 3, new SpriteState());
            Item sword = new Item(swordSprite);
            sword.Speed = 0;
            //replace later with player position
            sword.Position = new Vector2(100, 100);
            return sword;
        }
        public Item createBow()
        {
            ISprite bowSprite = new StationarySprite(this.bowTexture, new Rectangle(0, 0, 16, 16), 3, new SpriteState());
            Item bow = new Item(bowSprite);
            bow.Speed = 0;
            //replace later with player position
            bow.Position = new Vector2(100, 100);
            return bow;
        }
    }
}
