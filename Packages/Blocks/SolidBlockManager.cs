using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Blocks
{
    public class SolidBlockManager
    {
        private List<SolidBlock> Blocks;
        private int currentBlockIndex;

        public SolidBlockManager(SpriteBatch spriteBatch)
        {
            Blocks = new List<SolidBlock>
            {
             SolidBlockSpriteFactory.Instance.grayBrick(spriteBatch),
             SolidBlockSpriteFactory.Instance.grayGrassBrick(spriteBatch),
             SolidBlockSpriteFactory.Instance.orangeBrick(spriteBatch),
             SolidBlockSpriteFactory.Instance.whiteBrick(spriteBatch)
            };
            currentBlockIndex = 0;
        }

        public void SwitchToPreviousBlock()
        {
            if (Blocks.Count == 0) return;
            if (currentBlockIndex <= 0)
                currentBlockIndex = Blocks.Count - 1;
            else
                currentBlockIndex--;
        }

        public void SwitchToNextBlock()
        {
            if (Blocks.Count == 0) return;
            if (currentBlockIndex >= Blocks.Count - 1)
                currentBlockIndex = 0;
            else
                currentBlockIndex++;
        }

        public SolidBlock GetCurrentBlock()
        {
            return Blocks.Count > 0 ? Blocks[currentBlockIndex] : null;
        }

        // Added in order to allow game1.cs to iterate over all blocks, rather than just the current one.
        public List<SolidBlock> GetAllBlocks()
        {
            return Blocks;
        }

    }
}
