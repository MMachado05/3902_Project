using System.Collections.Generic;
using Microsoft.Xna.Framework;

public interface ICustomCollidable
{
    List<Rectangle> GetCollisionBoxes();
}
