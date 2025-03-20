using Microsoft.Xna.Framework;

namespace Project
{
    public interface IBlock
    {
        /// <summary>
        /// Draws the Block object to the game screen.
        /// </summary>
        [System.Obsolete("Should be providing destination as an argument here.")]
        public void Draw();

        public void Draw(Rectangle dest);
    }
}
