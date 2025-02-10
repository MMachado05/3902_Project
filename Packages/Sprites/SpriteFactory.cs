using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project;

public class SpriteFactory
{
    // TODO: Might be good to have multiple factories for different sprite types (e.g.
    // enemies, characters, items, etc.)

    private Texture2D playerSpriteSheet;

    private static SpriteFactory instance = new SpriteFactory();

    public static SpriteFactory Instance
    {
        get
        {
            return instance;
        }
    }

    private SpriteFactory()
    {
    }

    public void LoadAllTextures(ContentManager content)
    {
        playerSpriteSheet = content.Load<Texture2D>("Character32x32TextureTemplate"); // TODO: Replace with actual
    }

    public ISprite CreatePlayerSprite()
    {

    }
}
