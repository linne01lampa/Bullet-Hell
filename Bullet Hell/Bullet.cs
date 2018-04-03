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
        Vector2 position;
        Texture2D texture;
        Rectangle rectangle;
        float speed;
        Color color;
        Vector2 scale;
        Vector2 moveDir;
        Vector2 offset;


        public Bullet(Texture2D bulletTexture, Vector2 startPosition, Vector2 bulletScale, float bulletSpeed)
        {
            texture = bulletTexture;
            position = startPosition;
            scale = bulletScale;
            speed = bulletSpeed;
            offset = bulletTexture.Bounds.Size.ToVector2() * .5f;
            moveDir = Vector2.Zero;
            color = Color.White;
            rectangle = new Rectangle(position.ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());
        }
    }
}
