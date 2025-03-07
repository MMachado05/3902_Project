using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Blocks
{
    public class SolidBlockManager
    {

        SpriteBatch _SpriteBatch;
        Texture2D test;



        public SolidBlockManager(SpriteBatch spriteBatch)
        {
            _SpriteBatch = spriteBatch;
            this.test = SolidBlockSpriteFactory.Instance.getSolidBlockSheet();




        }
        public SolidBlock boardersBrick(Rectangle destination)
        {
            return new SolidBlock(_SpriteBatch, test, SolidBlockSpriteFactory.Instance.boardersBrick(), destination);
        }
        public SolidBlock obstacleBlock(Rectangle destination)
        {
            return new SolidBlock(_SpriteBatch, test, SolidBlockSpriteFactory.Instance.obstacle(), destination);
        }
        public SolidBlock doorBlock(Rectangle destination)
        {
            return new SolidBlock(_SpriteBatch, test, SolidBlockSpriteFactory.Instance.doorBlock(), destination);
        }

        // Added in order to allow game1.cs to iterate over all blocks, rather than just the current one.
       /* public List<SolidBlock> GetAllBlocks()
        {
            return Blocks;
        }*/

    }
}
