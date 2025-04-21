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
        private Color defaultColor = new Color(50, 50, 50);
        private Color hoverColor = new Color(70, 70, 120);
        private Color textColor = Color.White;

        public Button(string text, Rectangle bounds, GameOverAction action, SpriteFont font)
        {
            Text = text;
            Bounds = bounds;
            Action = action;
            this.font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            MouseState mouse = Mouse.GetState();
            Color background = Bounds.Contains(mouse.Position) ? hoverColor : defaultColor;

            Texture2D rectTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            rectTexture.SetData(new[] { Color.White });

            spriteBatch.Draw(rectTexture, Bounds, background);

            Vector2 textSize = font.MeasureString(Text);
            Vector2 textPos = new Vector2(
                Bounds.X + (Bounds.Width - textSize.X) / 2,
                Bounds.Y + (Bounds.Height - textSize.Y) / 2
            );

            spriteBatch.DrawString(font, Text, textPos, textColor);
        }

        public bool IsClicked(MouseState mouse)
        {
            return Bounds.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed;
        }
    }
}
