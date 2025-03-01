using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Blocks
{
    public class SolidBlockManager
    {
        private List<SolidBlock> topBoarder;
        private List<SolidBlock> bottomBoarder;
        private List<SolidBlock> leftBoarder;
        private List<SolidBlock> rightBoarder;
        private List<SolidBlock> obstacleList;




        private int currentBlockIndex;
        SpriteBatch _SpriteBatch;
        Game1 _game;
        ContentManager content;
        Texture2D test;
        Rectangle src;
        
        

        public SolidBlockManager(SpriteBatch spriteBatch)
        {
            _SpriteBatch = spriteBatch;
             this.test =SolidBlockSpriteFactory.Instance.getSolidBlockSheet();
       



        }
        public SolidBlock boardersBrick(Rectangle destination){
            return new SolidBlock(_SpriteBatch,test,SolidBlockSpriteFactory.Instance.boardersBrick(),destination);
        }
         public SolidBlock obstacleBlock(Rectangle destination){
            return new SolidBlock(_SpriteBatch,test,SolidBlockSpriteFactory.Instance.obstacle(),destination);
        }
         public SolidBlock doorBlock(Rectangle destination){
            return new SolidBlock(_SpriteBatch,test,SolidBlockSpriteFactory.Instance.doorBlock(),destination);
        }





     

        public void SwitchToPreviousBlock()
        {
            if (bottomBoarder.Count == 0) return;
            if (currentBlockIndex <= 0)
                currentBlockIndex = bottomBoarder.Count - 1;
            else
                currentBlockIndex--;
        }

        public void SwitchToNextBlock()
        {
            if (bottomBoarder.Count == 0) return;
            if (currentBlockIndex >= bottomBoarder.Count - 1)
                currentBlockIndex = 0;
            else
                currentBlockIndex++;
        }

        public SolidBlock GetCurrentBlock()
        {
            return bottomBoarder.Count > 0 ? bottomBoarder[currentBlockIndex] : null;
        }


    }
    
}
