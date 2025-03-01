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
             //_game = game;
             this.test =SolidBlockSpriteFactory.Instance.getSolidBlockSheet();
             this.src = SolidBlockSpriteFactory.Instance.boardersBrick();
           /* currentBlockIndex = 0;
            topBoarder = new List<SolidBlock>();
            bottomBoarder = new List<SolidBlock>();
            leftBoarder = new List<SolidBlock>();
            rightBoarder = new List<SolidBlock>();
            this.obstacleList = new List<SolidBlock>();*/



        }
        public SolidBlock boardersBrick(Rectangle destination){
            return new SolidBlock(_SpriteBatch,test,src,destination);
        }





       /* public List<SolidBlock> topBoarders(){
            for(int i=0;i<960;i+=32){
                SolidBlock holder = SolidBlockSpriteFactory.Instance.boardersBrick(_SpriteBatch,new Rectangle(i,0,32,32));
                topBoarder.Add(holder);
            }
            return topBoarder;
        }
         public List<SolidBlock> bottomBoarders(){
            for(int i=0;i<960;i+=32){
                SolidBlock holder = SolidBlockSpriteFactory.Instance.boardersBrick(_SpriteBatch,new Rectangle(i,_game.GraphicsDevice.Viewport.Height-32,32,32));
                bottomBoarder.Add(holder);
            }
            return bottomBoarder;
        }
          public List<SolidBlock> leftBoarders(){
            for(int i=0;i<960;i+=32){
                SolidBlock holder = SolidBlockSpriteFactory.Instance.boardersBrick(_SpriteBatch,new Rectangle(0,i,32,32));
                if(i == _game.GraphicsDevice.Viewport.Width/3){
                    holder = SolidBlockSpriteFactory.Instance.doorBlock(_SpriteBatch,new Rectangle(-32,i,64,64));
                    i+=32;
                }
                leftBoarder.Add(holder);
            }
            return leftBoarder;
        }
          public List<SolidBlock> rightBoarders(){
            for(int i=0;i<960;i+=32){
                SolidBlock holder = SolidBlockSpriteFactory.Instance.boardersBrick(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width-32,i,32,32));
                  if(i == _game.GraphicsDevice.Viewport.Width/3){
                    holder = SolidBlockSpriteFactory.Instance.doorBlock(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width-32,i,64,64));
                    i+=32;
                }
                rightBoarder.Add(holder);
            }
            return rightBoarder;
        }
        public List<SolidBlock> obstacles(){
            SolidBlock holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3,_game.GraphicsDevice.Viewport.Height/3,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+32,_game.GraphicsDevice.Viewport.Height/3,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+64,_game.GraphicsDevice.Viewport.Height/3,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+96,_game.GraphicsDevice.Viewport.Height/3,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+128,_game.GraphicsDevice.Viewport.Height/3,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+128,_game.GraphicsDevice.Viewport.Height/3-32,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+128,_game.GraphicsDevice.Viewport.Height/3-64,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+128,_game.GraphicsDevice.Viewport.Height/3-96,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+128,_game.GraphicsDevice.Viewport.Height/3-128,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+128,_game.GraphicsDevice.Viewport.Height/3-160,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+128,_game.GraphicsDevice.Viewport.Height/3-192,32,32));
            obstacleList.Add(holder);

            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3,_game.GraphicsDevice.Viewport.Height/3+32,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+32,_game.GraphicsDevice.Viewport.Height/3+64,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+64,_game.GraphicsDevice.Viewport.Height/3+96,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+96,_game.GraphicsDevice.Viewport.Height/3+128,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+128,_game.GraphicsDevice.Viewport.Height/3+160,32,32));
            obstacleList.Add(holder);

            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+128,_game.GraphicsDevice.Viewport.Height/3+192,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+96,_game.GraphicsDevice.Viewport.Height/3+224,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+64,_game.GraphicsDevice.Viewport.Height/3+256,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3+32,_game.GraphicsDevice.Viewport.Height/3+288,32,32));
            obstacleList.Add(holder);
            holder = SolidBlockSpriteFactory.Instance.obstacle(_SpriteBatch,new Rectangle(_game.GraphicsDevice.Viewport.Width/3,_game.GraphicsDevice.Viewport.Height/3+320,32,32));
            obstacleList.Add(holder);
            
           
            return obstacleList;

        }*/

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
