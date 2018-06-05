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
    class Shield
    {
        Vector2 position;
        Rectangle rectangle;
        Texture2D texture;
        Vector2 scale;
        float time;
        float timeLeft;
        static bool active;

        public Shield(Texture2D shieldTex, Vector2 shieldPos, float timeAlive, Vector2 shieldScale)
        {
            texture = shieldTex;
            position = shieldPos;
            rectangle = new Rectangle(position.ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());
            timeLeft = timeAlive;
            active = false;
            scale = shieldScale;
        }

        public void Update(float deltaTime, Player player)
        {
            if (active == true)
            {
                timeLeft -= deltaTime;

                if (timeLeft >= time)
                {
                    timeLeft -= deltaTime;
                }
                if (timeLeft <= time)
                {
                    active = false;
                }
            }
            

            rectangle = new Rectangle(position.ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());

            DetectCollision(player);
        }

        public void DetectCollision(Player player)
        {
            if (rectangle.Intersects(player.GetRectangle()))
            {
                active = true;

                position = new Vector2(1000, 1000);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White);
        }

        public void SetPosition(Vector2 vector)
        {
            position = vector;
        }

        public static bool GetActive()
        {
            return active;
        }
    }
}
