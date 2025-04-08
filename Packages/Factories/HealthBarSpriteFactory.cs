using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Characters.Enums;
using Project.Sprites;
using Project.Sprites.ConcreteClasses;

namespace Project.Factories
{

    public class HealthBarSpriteFactory
    {

        private Texture2D healthBarSpriteSheet;

        private int scale;
        private int widthPixels;
        private int heightPixels;

        private static HealthBarSpriteFactory instance = new HealthBarSpriteFactory();

        public static HealthBarSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private HealthBarSpriteFactory()
        {
            this.scale = 2;
            this.widthPixels = 64;
            this.heightPixels = 16;
        }

        public void LoadAllTextures(ContentManager content)
        {
            this.healthBarSpriteSheet = content.Load<Texture2D>("healthbar");
        }

        public ISprite HealthBarSprite(int health)
        {
            int xOrigin = 0;
            int yOrigin = 0;

            switch (health)
            {
                case 1:
                    yOrigin = 68;
                    break;
                case 2:
                    yOrigin = 51;
                    break;
                case 3:
                    yOrigin = 34;
                    break;
                case 4:
                    yOrigin = 17;
                    break;
                case 5:
                    yOrigin = 0;
                    break;
                default:
                    yOrigin = 0;
                    break;
            }

            return new StationarySprite(this.healthBarSpriteSheet,
                new Rectangle(xOrigin, yOrigin, this.widthPixels,
                  this.heightPixels), CharacterState.Stopped);
        }

        
    }
}
