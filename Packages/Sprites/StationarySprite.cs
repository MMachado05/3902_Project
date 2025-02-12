using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project;

public class StationarySprite : AbstractSprite
{
  public StationarySprite(Texture2D texture, Rectangle source, int scaleFactor, SpriteState state)
    : base(texture, source, scaleFactor, state)
  {
  }

  public override void Update()
  {
    // Empty method, stationary sprite
  }
}