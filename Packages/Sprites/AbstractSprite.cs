using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters.Enums;

namespace Project.Sprites
{
    public abstract class AbstractSprite : ISprite
    {
        protected Texture2D texture;
        protected Rectangle source;
        protected CharacterState state;
        protected int widthPixels;
        protected int heightPixels;

        public AbstractSprite(Texture2D texture, Rectangle source,
            CharacterState state)
        {
            this.texture = texture;
            this.source = source;
            this.widthPixels = source.Width;
            this.heightPixels = source.Height;
            this.state = state;
        }

        public CharacterState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        public Rectangle Source
        {
            get
            {
                return this.source;
            }
            set
            {
                this.source = value;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.Draw(this.texture, destination, this.source, Color.White);
        }
        public abstract void Update(GameTime gameTime);
    }

}
