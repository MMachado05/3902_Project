using System.Collections.Generic;
using System.Collections;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Project
{
    public class Renderer : ISprite
    {
        private readonly Texture2D _spriteSheet;

        private List<Rectangle> _sourceRectangles;
        private Rectangle _sourceRectangle;
        private Rectangle _destinationRectangle;

        private Rendering _rendering;
        public enum Rendering
        {
            Static, Animated
        }

        // Static Sprites
        public Renderer(Texture2D texture, Rectangle source, Rectangle destination)
        {
            _rendering = Rendering.Static;
            _spriteSheet = texture;
            _sourceRectangle = source;
            _destinationRectangle = destination;

        }

        // Animated via multiple source rectangles
        public Renderer(Texture2D texture, List<Rectangle> sources, Rectangle destination)
        {
            _rendering = Rendering.Animated;
            _spriteSheet = texture;
            _sourceRectangles = sources;
            _destinationRectangle = destination;

        }

        public Rectangle DestinationRectangle
        {        
            get { return _destinationRectangle; }
            set { _destinationRectangle = value; }
        }

        private int currentFrame = 0;
        public void Update(GameTime gameTime)
        {
            if (_rendering.Equals(Rendering.Animated))
            {
                currentFrame++;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_rendering.Equals(Rendering.Animated))
            {
                spriteBatch.Draw(_spriteSheet, _destinationRectangle, _sourceRectangles[currentFrame % _sourceRectangles.Count], Color.White);
            }
            else if (_rendering.Equals(Rendering.Static))
            {
                spriteBatch.Draw(_spriteSheet, _destinationRectangle, _sourceRectangle, Color.White);
            }
        }
    }
}
