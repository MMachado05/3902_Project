using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;
using Project.Rooms.Blocks.ConcreteClasses;
using Project.Sprites;

namespace Project.Items
{
    public class Arrow : ProjectileItem
    {
        public override Rectangle Location { get; set; }
        public Arrow(Rectangle position, Vector2 vectorDirection, float speed, ISprite sprite, bool damagesPlayer, bool damagesEnemies)
          : base(position, vectorDirection, sprite, speed, 600f, damagesPlayer, damagesEnemies)
        {
            Location = position;
        }

        public override void CollideWith(IGameObject collider, Vector2 from)
        {
            if (collider is Enemy || collider is SolidBlock)
            {
                ToBeDeleted = true;
            }
        }
    }
}
