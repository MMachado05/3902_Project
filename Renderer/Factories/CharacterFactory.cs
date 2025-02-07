using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{

    public class CharacterFactory
    {
        // Character spritesheet
        private Texture2D _CharacterSpritesheet;

        // Create a singleton instance of CharacterFactory
        private static CharacterFactory instance = new CharacterFactory();

        public static CharacterFactory Instance
        {
            get
            {
                return instance;
            }
        }

        // Constructor to initialize the factory instance
        public CharacterFactory()
        {
            CharacterFactory.instance = this;
        }

        public void LoadContent(ContentManager content)
        {
            _CharacterSpritesheet = content.Load<Texture2D>("Character32x32TextureTemplate");
        }

        public void UnloadContent(ContentManager content)
        {
            _CharacterSpritesheet.Dispose();
        }

        public ISprite ReplaceCharacter(CharacterManager manager)
        {
            switch (manager.State)
            {
                case CharacterManager.CharacterState.Static:
                    switch (manager.Direction) {
                        case Movement.Direction.Down:
                            return new Renderer(_CharacterSpritesheet,
                                    new Rectangle(64, 0, 32, 32), manager.DestinationRectangle);
                        case Movement.Direction.Up:
                            return new Renderer(_CharacterSpritesheet,
                                new Rectangle(0, 0, 32, 32), manager.DestinationRectangle);
                        case Movement.Direction.Right:
                            return new Renderer(_CharacterSpritesheet,
                                new Rectangle(32, 0, 32, 32), manager.DestinationRectangle);
                        case Movement.Direction.Left:
                            // the way graphics is imported currently is risky/bad for code smell
                            return new Renderer(_CharacterSpritesheet,
                                new Rectangle(96, 0, 32, 32), manager.DestinationRectangle);
                        default: 
                            throw new ArgumentException("No direction in CharacterFactory");
                    }
                case CharacterManager.CharacterState.Animated:
                    switch (manager.Direction) {
                        case Movement.Direction.Down:
                            List<Rectangle> sourcesDown = [new Rectangle(64, 32, 32, 32), new Rectangle(64, 64, 32, 32), new Rectangle(64, 96, 32, 32), new Rectangle(64, 128, 32, 32)];
                            return new Renderer(_CharacterSpritesheet, sourcesDown, manager.DestinationRectangle);
                        case Movement.Direction.Up:
                            List<Rectangle> sourcesUp = [new Rectangle(0, 32, 32, 32), new Rectangle(0, 64, 32, 32), new Rectangle(0, 96, 32, 32), new Rectangle(0, 128, 32, 32)];
                            return new Renderer(_CharacterSpritesheet, sourcesUp, manager.DestinationRectangle);
                        case Movement.Direction.Right:
                            List<Rectangle> sourcesRight = [new Rectangle(32, 32, 32, 32), new Rectangle(32, 64, 32, 32), new Rectangle(32, 96, 32, 32), new Rectangle(32, 128, 32, 32)];
                            return new Renderer(_CharacterSpritesheet, sourcesRight, manager.DestinationRectangle);
                        case Movement.Direction.Left:
                            List<Rectangle> sourcesLeft = [new Rectangle(96, 32, 32, 32), new Rectangle(96, 64, 32, 32), new Rectangle(96, 96, 32, 32), new Rectangle(96, 128, 32, 32)];
                            return new Renderer(_CharacterSpritesheet, sourcesLeft, manager.DestinationRectangle);
                        default:
                            throw new ArgumentException("No direction in CharacterFactory");
                    }
                default: throw new ArgumentException("No direction in CharacterFactory");
            }
        }

    }

}