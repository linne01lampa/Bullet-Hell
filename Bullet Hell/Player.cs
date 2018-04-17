using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bullet_Hell
{
    public class Player
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        Vector2 scale;
        Vector2 offset;
        Vector2 moveDir;
        float rotation;
        float speed;
        Color color;
        bool alive;

        public Player(Texture2D playerTexture, Vector2 startPosition, Vector2 playerScale, float playerSpeed)
        {
            texture = playerTexture;
            position = startPosition;
            scale = playerScale;
            speed = playerSpeed;
            offset = playerTexture.Bounds.Size.ToVector2() * .5f;
            moveDir = Vector2.Zero;
            rotation = 0;
            color = Color.Green;
            rectangle = new Rectangle(position.ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());
            alive = true;
        }

        public void Update(float deltaTime, Vector2 mousePos, List<Enemy> enemies)
        {
            float pixelToMove = speed * deltaTime;

            rectangle = new Rectangle(position.ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());

            moveDir = mousePos - position;
            moveDir.Normalize();

            //rotation = (float)Math.Atan2(moveDir.Y, moveDir.X);

            //if (Vector2.Distance(mousePos, position) < pixelToMove)
            //{
            //    position = mousePos;
            //}
            //else
            //{
            //    position += moveDir * pixelToMove;
            //}
            if (alive)
            {
                position = mousePos;
            }
            else
            {
                position = new Vector2(10000, 10000);
            }

            rectangle.Location = (position - offset * scale).ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, offset, scale, SpriteEffects.None, 0);
        }

        public Rectangle GetRectangle()
        {
            return rectangle;
        }

        public void DetectCollision(List<Enemy> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (rectangle.Intersects(enemies[i].GetRectangle()))
                {
                    alive = false;
                }
            }
        }
    }
}