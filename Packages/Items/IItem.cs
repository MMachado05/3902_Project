namespace Project
{  // TODO: Better namespace when project matures
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IItem
    {
        Vector2 Position { get; set; }
        float Speed { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}