using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters.Enums;

namespace Project.Sprites.ConcreteClasses
{
    public class SingleAnimationSprite : AbstractSprite
    {
        private int maxFrames;
        private int currFrame;
        private CharacterState complete;

        /// <summary>
        /// maxFrames is literal; if there are four frames of anmimation, pass in 4 as
        /// the constructor argument.
        /// </summary>
        public SingleAnimationSprite(Texture2D texture, Rectangle sourceInitial,
            int maxFrames, CharacterState active, CharacterState complete) :
          base(texture, sourceInitial, active)
        {
            this.maxFrames = maxFrames - 1;
            this.currFrame = 0;
            this.complete = complete;
        }

        // TODO: This logic should use gameTime as a way to know when the animation is
        // over. So, we could set the number of frames, and how long the animation should
        // actually last *in seconds*.
        public override void Update(GameTime gameTime)
        {
            if (this.currFrame < this.maxFrames)
            {
                this.currFrame++;
                base.source.Y += base.source.Height;
            }
            else
            {
                base.State = this.complete;
            }
        }
    }
}
