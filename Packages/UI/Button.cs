using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project.UI
{
    public class Button
    {
        public string Text { get; set; }
        public Rectangle Bounds { get; set; }
        public GameOverAction Action { get; set; }

        private SpriteFont font;

        public Button(string text, Rectangle bounds, GameOverAction action, SpriteFont font)
        {
            Text = text;
            Bounds = bounds;
            Action = action;
            this.font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, Text, new Vector2(Bounds.X + 10, Bounds.Y + 10), Color.White);
        }

        public bool IsClicked(MouseState mouse)
        {
            return Bounds.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed;
        }
    }
}
