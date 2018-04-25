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
        public enum Owner { Player, Enemy };
        Owner owner;
        float damage;
        float speed;
        bool alive;
        float rotation;
        Texture2D texture;
        Rectangle rectangle;
        Vector2 dir;
        Vector2 position;
        Vector2 scale;
        Vector2 offest;
        Color color;

        public Bullet(Texture2D bulletTexture, Vector2 startPosition, Vector2 bulletDir, float bulletSpeed, Vector2 bulletScale, Owner bulletOwner, Color bulletColor)
        {
            speed = bulletSpeed;
            texture = bulletTexture;
            dir = bulletDir;
            dir.Normalize();
            scale = bulletScale;
            offest = bulletTexture.Bounds.Size.ToVector2() * .5f;
            position = startPosition;
            rectangle = new Rectangle((startPosition - offest * scale).ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());
            alive = true;
            rotation = (float)Math.Atan2(dir.X, dir.Y);
            color = bulletColor;
            damage = 10;
            owner = bulletOwner;
        }

        public void Update(float deltaTime)
        {
            position += dir * speed * deltaTime;
            rectangle.Location = (position - offest * scale).ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, offest, scale, SpriteEffects.None, 0);
        }

        public float Damage(Rectangle otherRectangle)
        {
            float damageToDeal = 0;

            if (rectangle.Intersects(otherRectangle))
            {
                damageToDeal = damage;
                alive = false;
            }
            return damageToDeal;
        }

        public bool GetIsAlive()
        {
            return alive;
        }

        public Owner GetOwner()
        {
            return owner;
        }
    }
}
