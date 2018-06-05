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
        float health;
        float attackSpeed;
        float attackTimer;
        bool invu;
        float powerAttackTimer;
        float powerAttackSpeed;
        bool powerAttack;
        //float score;

        public Player(Texture2D playerTexture, Vector2 startPosition, float playerSpeed, Vector2 playerScale, float playerRotation, Color playerColor, float playerHealth, float playerAttackSpeed)
        {
            texture = playerTexture;
            position = startPosition;
            scale = playerScale;
            speed = playerSpeed;
            offset = playerTexture.Bounds.Size.ToVector2() * .5f;
            moveDir = Vector2.Zero;
            rotation = 0;
            color = playerColor;
            health = playerHealth;
            rotation = playerRotation;
            health = playerHealth;
            rectangle = new Rectangle(position.ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());
            alive = true;
            attackSpeed = playerAttackSpeed;
            attackTimer = 0;
            powerAttack = false;
            powerAttackSpeed = 10;
        }

        public void Update(float deltaTime, KeyboardState keyboardState, MouseState mouseState, Point windowSize)
        {
            if (!UserInterface.GetPause())
            {
                if (alive)
                {
                    if (Shield.GetActive() == true)
                    {
                        invu = true;
                    }
                    else
                    {
                        invu = false;
                    }

                    //float pixelToMove = speed * deltaTime;

                    rectangle = new Rectangle(position.ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());

                    if (Keyboard.GetState().IsKeyDown(Keys.W) && position.Y >= 0)
                    {
                        position.Y -= 5;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S) && position.Y <= 940)
                    {
                        position.Y += 5;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D) && position.X <= 580)
                    {
                        position.X += 5;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.A) && position.X >= 0)
                    {
                        position.X -= 5;
                    }

                    attackTimer += deltaTime;
                    if (attackTimer <= attackSpeed)
                    {
                        attackTimer += deltaTime;
                    }

                    powerAttackTimer += deltaTime;
                    if (powerAttackTimer <= powerAttackSpeed)
                    {
                        powerAttackTimer += deltaTime;
                        powerAttack = false;
                    }
                    else
                    {
                        powerAttack = true;
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed && attackTimer >= attackSpeed && alive)
                    {
                        Vector2 bulletDir = mouseState.Position.ToVector2() - position;
                        BulletManager.AddBullet(TextureLibrary.GetTexture("white"), position, bulletDir, 400, new Vector2(.2f, .2f), Bullet.Owner.Player, Color.Blue);
                        attackTimer = 0;
                    }

                    if (mouseState.RightButton == ButtonState.Pressed && alive && powerAttackTimer >= powerAttackSpeed)
                    {
                        BulletManager.AddBullet(TextureLibrary.GetTexture("white"), position, new Vector2(0, 1), 400, new Vector2(.2f, .2f), Bullet.Owner.Player, Color.Blue);
                        BulletManager.AddBullet(TextureLibrary.GetTexture("white"), position, new Vector2(0, -1), 400, new Vector2(.2f, .2f), Bullet.Owner.Player, Color.Blue);
                        BulletManager.AddBullet(TextureLibrary.GetTexture("white"), position, new Vector2(1, 0), 400, new Vector2(.2f, .2f), Bullet.Owner.Player, Color.Blue);
                        BulletManager.AddBullet(TextureLibrary.GetTexture("white"), position, new Vector2(-1, 0), 400, new Vector2(.2f, .2f), Bullet.Owner.Player, Color.Blue);
                        BulletManager.AddBullet(TextureLibrary.GetTexture("white"), position, new Vector2(1, 1), 400, new Vector2(.2f, .2f), Bullet.Owner.Player, Color.Blue);
                        BulletManager.AddBullet(TextureLibrary.GetTexture("white"), position, new Vector2(1, -1), 400, new Vector2(.2f, .2f), Bullet.Owner.Player, Color.Blue);
                        BulletManager.AddBullet(TextureLibrary.GetTexture("white"), position, new Vector2(-1, -1), 400, new Vector2(.2f, .2f), Bullet.Owner.Player, Color.Blue);
                        BulletManager.AddBullet(TextureLibrary.GetTexture("white"), position, new Vector2(-1, 1), 400, new Vector2(.2f, .2f), Bullet.Owner.Player, Color.Blue);
                        powerAttackTimer = 0;
                    }

                    #region ye
                    //moveDir = mousePos - position;
                    //moveDir.Normalize();

                    //rotation = (float)Math.Atan2(moveDir.Y, moveDir.X);

                    //if (Vector2.Distance(mousePos, position) < pixelToMove)
                    //{
                    //    position = mousePos;
                    //}
                    //else
                    //{
                    //    position += moveDir * pixelToMove;
                    //}

                    //position = mousePos;

                    //position = new Vector2(10000, 10000);
                    #endregion
                    rectangle.Location = (position - offset * scale).ToPoint();
                }
                else
                {
                    color = Color.Black;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, offset, scale, SpriteEffects.None, 0);
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

        public void ChangeHealth(float healthMod)
        {
            if (invu == false)
            {
                health += healthMod;
                if (health <= 0)
                {
                    health = 0;
                    alive = false;
                }
            }
        }

        public Rectangle GetRectangle()
        {
            return rectangle;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public float GetHealth()
        {
            return health;
        }

        public bool GetAlive()
        {
            return alive;
        }

        public bool GetInvu()
        {
            return invu;
        }

        public bool GetPower()
        {
            return powerAttack;
        }
    }
}