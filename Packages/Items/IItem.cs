namespace Project; // TODO: Better namespace when project matures
using Microsoft.Xna.Framework;
public interface IItem
{
    Vector2 Position { get; }
    float Speed { get; set; }
    void SetPosition(Vector2 newPosition);
    void Update();
}
