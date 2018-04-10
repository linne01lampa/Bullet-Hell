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

        Random rnd = new Random();

        public Enemy(Texture2D enemyTexture, Vector2 enemyScale, float enemyRotation, float enemyHealth, Vector2 enemyPosition)
        {
            texture = enemyTexture;
            position = enemyPosition;
            scale = enemyScale;
            rotation = enemyRotation;
            health = enemyHealth;
            color = Color.White;
            rectangle = new Rectangle(position.ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());
        }

        public void Update(Enemy enemy)
        {
            int random = rnd.Next(0, 4);

            //Ner-höger
            if (random == 0)
            {
                position.X = 3f;
                position.Y = 3f;
            }
            //Ner-vänster
            if (random == 1)
            {
                position.X = -3f;
                position.Y = 3f;
            }
            //Upp-vänster
            if (random == 2)
            {
                position.X = -3f;
                position.Y = -3f;
            }
            //Upp-höger
            if (random == 3)
            {
                position.X = 3f;
                position.Y = -3f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Vector2.Zero, null, color, rotation, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}