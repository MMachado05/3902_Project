using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters.Enums;
using Project.Items;
using Project.Sprites;
using Project.Sprites.ConcreteClasses;

namespace Project.Factories
{

    public class InventoryGUISpriteFactory
    {

        private Texture2D inventoryGUISpriteSheet;

        private int scale;
        private int widthPixels;
        private int heightPixels;

        private List<IItem> inventoryItems;

        private static InventoryGUISpriteFactory instance = new InventoryGUISpriteFactory();

        public static InventoryGUISpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private InventoryGUISpriteFactory()
        {
            this.scale = 2;
            this.widthPixels = 80;
            this.heightPixels = 16;
        }

        public void LoadAllTextures(ContentManager content)
        {
            this.inventoryGUISpriteSheet = content.Load<Texture2D>("InventoryGUI");
        }

        public ISprite InventoryGUISprite(int index, List<IItem> inventoryItems)
        {
            int xOrigin = 0;
            int yOrigin = 0;

            switch (index)
            {
                case 4:
                    yOrigin = 68;
                    break;
                case 3:
                    yOrigin = 51;
                    break;
                case 2:
                    yOrigin = 34;
                    break;
                case 1:
                    yOrigin = 17;
                    break;
                case 0:
                    yOrigin = 0;
                    break;
                default:
                    yOrigin = 0;
                    break;
            }

            return new StationarySprite(this.inventoryGUISpriteSheet,
                new Rectangle(xOrigin, yOrigin, this.widthPixels,
                  this.heightPixels), CharacterState.Stopped);
        }

        
    }
}
