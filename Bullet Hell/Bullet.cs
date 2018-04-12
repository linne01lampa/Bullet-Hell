using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bullet_Hell
{
    class Bullet
    {
        float damage;
        float speed;
        Texture2D texture;
        Rectangle rectangle;
        Vector2 dir;
        Vector2 position;

        public Bullet(float bulletSpeed, Texture2D bulletTexture, float bulletDamage, Vector2 startDir, Vector2 startPosition)
        {
            speed = bulletSpeed;
            texture = bulletTexture;
            damage = bulletDamage;
            dir = startDir;
            position = startPosition;
        }

        public void Update(float deltaTime)
        {
            position += dir * speed * deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
