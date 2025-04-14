using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters.Enums;

namespace Project.Sprites.ConcreteClasses
{
    public class AnimatedLoopSprite : AbstractSprite
    {
        private int maxFrames;
        private int currFrame;
        private int topLeftXInitial;
        private int topLeftYInitial;

        /// <summary>
        /// maxFrames is literal; if there are four frames of anmimation, pass in 4 as
        /// the constructor argument.
        /// </summary>
        public AnimatedLoopSprite(Texture2D texture, Rectangle sourceInitial,
            int maxFrames, CharacterState state) : base(texture, sourceInitial, state)
        {
            this.maxFrames = maxFrames - 1;
            this.currFrame = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.currFrame == this.maxFrames)
                this.currFrame = 0;
            else
                this.currFrame++;
            // All sprites are animated downwards
            base.source.Y = this.topLeftYInitial + (base.heightPixels * this.currFrame);
        }
    }
}
