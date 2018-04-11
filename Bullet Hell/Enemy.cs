using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bullet_Hell
{
    public class Enemy
    {
        public static Vector2 position;
        Texture2D texture;
        Rectangle rectangle;
        Vector2 scale;
        float rotation;
        Color color;
        float health;

        float time;
        float timeTurn = 1;
        int turney = 1;
        bool turn;

        Random rnd;

        bool started = false;
        Vector2 startPos;

        public Enemy(Texture2D enemyTexture, Vector2 enemyScale, float enemyRotation, float enemyHealth, Vector2 enemyStartPos)
        {
            texture = enemyTexture;
            scale = enemyScale;
            rotation = enemyRotation;
            health = enemyHealth;
            color = Color.White;
            rectangle = new Rectangle(position.ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());
            rnd = new Random();
            startPos = enemyStartPos;
        }

        public void Update(GameTime gameTime)
        {
            //timer.Update(gameTime, 2f);
            if (started == false)
            {
                position = startPos;
                started = true;
            }

            if (position.Y >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - texture.Bounds.Size.Y)
            {
                started = false;
            }

            if (time <= 0)
            {
                time = timeTurn;
                turney *= -1;
                if (turney == 1)
                {
                    //position = new Vector2(10, 25);
                    turn = true;
                }
                else if (turney == -1)
                {
                    //position = new Vector2(-10, 25);
                    turn = false;
                }
            }
            else if (time > 0)
            {
                time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (turn == true)
            {
                //position = new Vector2(10, 25);
                position.X += 1;
                position.Y += 3;
            }
            else if (turn == false)
            {
                //position = new Vector2(-10, 25);

                position.X += -1;
                position.Y += 3;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}