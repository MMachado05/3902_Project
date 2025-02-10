using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project;

public class StationarySprite : AbstractSprite
{
    public StationarySprite(Texture2D texture, Rectangle source, int scaleFactor)
      : base(texture, source, scaleFactor)
    {
    }

    public override void Update()
    {
        // Empty method, stationary sprite
    }
}
