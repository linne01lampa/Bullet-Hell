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
        Vector2 position;
        Texture2D texture;
        Rectangle rectangle;
        Vector2 scale;
        float rotation;
        Color color;
        float health;
        float attackTimer;
        float attackSpeed;

        float time;
        float timeTurn = 1;
        int turney = 1;
        bool turn;

        Random rnd;

        List<Bullet> bullets;

        bool started = false;
        Vector2 startPos;

        public Rectangle GetRectangle()
        {
            return rectangle;
        }

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
            bullets = new List<Bullet>();
            attackSpeed = 1;
            attackTimer = 0;
        }

        public void Update(GameTime gameTime, float deltaTime)
        {
            attackTimer += deltaTime;
            if (attackTimer >= attackSpeed)
            {
                bullets.Add(new Bullet(500, TextureLibrary.GetTexture("player"), 10, new Vector2(0,1), position));
                attackTimer = 0;
            }

            rectangle = new Rectangle(position.ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(deltaTime);
            }

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
                    turn = true;
                }
                else if (turney == -1)
                {
                    turn = false;
                }
            }
            else if (time > 0)
            {
                time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (turn == true)
            {
                position.X += 1;
                position.Y += 1;
            }
            else if (turn == false)
            {
                position.X += -1;
                position.Y += 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, Vector2.Zero, scale, SpriteEffects.None, 0);

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(spriteBatch);
            }
        }
    }
}