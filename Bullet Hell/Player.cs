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
        }

        public void Update(float deltaTime, Vector2 mousePos)
        {
            float pixelToMove = speed * deltaTime;

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

            position = mousePos;


            rectangle.Location = (position - offset * scale).ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, offset, scale, SpriteEffects.None, 0);
        }

        public bool DetectCollision(Enemy enemy)
        {
            if (position.Y >= Bullet_Hell.Enemy.position.Y && position.X > Bullet_Hell.Enemy.position.X && position.X < (Bullet_Hell.Enemy.position.X + texture.Width))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}