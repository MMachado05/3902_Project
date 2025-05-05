using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Factories;
using Project.Packages.Sounds;
using Project.Sprites;

namespace Project.Items
{
    public class Bomb : Item
    {
        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }
        public Item ExplodingBomb;
        float TimeAlive;
        bool PlacedBomb;
        Explosion Explosion;
        public Bomb(Rectangle position, ISprite sprite) : base(sprite)
        {
            Location = position;
            TimeAlive = 4;
            PlacedBomb = false;
            Explosion = new Explosion(Location, ItemFactory.Instance.CreateExplosionSprite());
        }

        public override void Update(GameTime gameTime)
        {
            if (PlacedBomb)
            {
                TimeAlive -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (TimeAlive == 1)
                {
                    SoundEffectManager.Instance.playExplosion();
                    ExplodingBomb = Explosion;
                }
                if (TimeAlive <= 0)
                {
                    ExplodingBomb.ToBeDeleted = true;
                    Equipped = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (PlacedBomb)
            {
                ExplodingBomb.Draw(spriteBatch);
            }
        }
        public override void CollideWith(IGameObject collider, Vector2 from)
        {
            if (collider is Player)
            {
                Equipped = true;
            }
        }
    }
}
