using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Bullet_Hell
{
    static class UserInterface
    {
        static bool pause;
        static SpriteFont font;
        static KeyboardState prevKeyboardState = Keyboard.GetState();
        static List<Button> buttons = new List<Button>();
        static List<Button> endButtons = new List<Button>();
        static MouseState prevMouseState;
        
        public enum GameState { play, exit, restart };



        public static void AddButton(Texture2D texture, string text, Vector2 scale, Vector2 textScale, Vector2 screenSize)
        {
            buttons.Add(new Button(texture, Vector2.Zero, text, scale, textScale, font));
            PlaceButtons(screenSize);
        }

        public static void LoadSpriteFont(ContentManager content, string fontName)
        {
            font = content.Load<SpriteFont>(fontName);
        }

        public static GameState Update(KeyboardState keyboardState, MouseState mouseState, Player player)
        {
            bool exit = false;
            if (keyboardState.IsKeyDown(Keys.Escape) && prevKeyboardState.IsKeyUp(Keys.Escape))
            {
                pause = !pause;
            }

            if (buttons.Count >= 3)
            {
                buttons.RemoveAt(0);
            }

            string clickedButton = "";

            if (pause)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    bool mouseOverButton = buttons[i].Update(mouseState);
                    if (mouseOverButton && mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        clickedButton = buttons[i].GetButtonText();
                    }

                }
            }
            prevMouseState = mouseState;
            prevKeyboardState = keyboardState;
            switch (clickedButton)
            {
                case "Continue":
                    pause = false;
                    return GameState.play;
                case "Retry":
                    pause = false;
                    return GameState.restart;
                case "Exit":
                    exit = true;
                    return GameState.exit;
                default:
                    return GameState.play;
            }

        }

        public static void Draw(SpriteBatch spriteBatch, Player player)
        {
            string playerHealth = "Health: " + player.GetHealth().ToString();
            //string spacing = "   ";
            //string playerScore = "Score" + player.GetScore().ToString();
            spriteBatch.DrawString(font, playerHealth, new Vector2(400, 10), Color.White);
            if (pause)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Draw(spriteBatch);
                }
            }
        }

        public static bool GetPause()
        {
            return pause;
        }

        private static void PlaceButtons(Vector2 screenSize)
        {
            float factor = (float)buttons.Count + 3;
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Place(new Vector2(screenSize.X * 0.5f, screenSize.Y / factor * (i + 2)));
            }
        }
    }
}
