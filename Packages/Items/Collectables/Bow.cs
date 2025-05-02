using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Factories;
using Project.Packages.Sounds;
using Project.Sprites;

namespace Project.Items
{
    public class Bow : Item
    {
        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }

        public Bow(Rectangle position, ISprite sprite) : base(sprite)
        {
            Location = position;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void CollideWith(IGameObject collider, Vector2 from)
        {
            if (collider is Player && !Equipped)
            {
                ToBeDeleted = true;
            }
        }

        public override void Use(ItemManager itemManager, Player player)
        {
            System.Console.WriteLine("Use called!");
            var inventory = player._inventory;

            // Find the ArrowItem instance in the inventory
            ArrowItem arrowRef = null;

            foreach (var entry in inventory.Items.Keys)
            {
                if (entry is ArrowItem)
                {
                    arrowRef = (ArrowItem)entry;
                    break;
                }
            }

            // No arrows? Don't fire.
            if (arrowRef == null || inventory.Items[arrowRef] <= 0)
            {
                System.Console.WriteLine("No arrows left!");
                return;
            }

            // Decrease arrow count
            inventory.Items[arrowRef]--;
            if (inventory.Items[arrowRef] == 0)
            {
                inventory.Remove(arrowRef);
            }

            SoundEffectManager.Instance.playFireBow();

            // Shoot the arrow in the current direction
            switch (Direction)
            {
                case Direction.Up:
                    itemManager.AddProjectile(new Arrow(Location, new Vector2(0, -2), 2f, ItemFactory.Instance.CreateUpArrowSprite(), false, true));
                    break;
                case Direction.Right:
                    itemManager.AddProjectile(new Arrow(Location, new Vector2(2, 0), 2f, ItemFactory.Instance.CreateRightArrowSprite(), false, true));
                    break;
                case Direction.Left:
                    itemManager.AddProjectile(new Arrow(Location, new Vector2(-2, 0), 2f, ItemFactory.Instance.CreateLeftArrowSprite(), false, true));
                    break;
                case Direction.Down:
                    itemManager.AddProjectile(new Arrow(Location, new Vector2(0, 2), 2f, ItemFactory.Instance.CreateDownArrowSprite(), false, true));
                    break;
            }
        }


    }
}
