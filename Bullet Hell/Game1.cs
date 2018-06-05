using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace Bullet_Hell
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;

        Vector2 playerPos;

        int numEnemies;
        Rectangle mainFrame;
        SpriteFont scoreFont;
        string scoreSting;
        float score;
        int scoreInt;

        bool powerAttack;

        Shield shield;

        Vector2 bgPos;
        Vector2 bgPos2;
        bool once;
        //float health;
        bool show;

        List<Enemy> enemies;
        //Dictionary<string, Texture2D> textures;

        Vector2 startPos;
        Random rnd = new Random();

        float spawnEnemyTimer;
        float spawnEnemy;
        Vector2 shieldPos;

        float spawnShieldTimer;
        float spawnShield;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 580;
            graphics.PreferredBackBufferHeight = 940;
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            spawnShieldTimer = 5;
            spawnShield = 0;
            //shieldPos = new Vector2(rnd.Next(0, 500), rnd.Next(0, 900));
            shield = new Shield(TextureLibrary.GetTexture("shield"), Vector2.One, 5, new Vector2(1, 1));
            Reload();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //textures.Add("player", Content.Load<Texture2D>("player"));
            //textures.Add("enemy", Content.Load<Texture2D>("bad"));
            TextureLibrary.LoadTexture(Content, "player");
            TextureLibrary.LoadTexture(Content, "bad");
            TextureLibrary.LoadTexture(Content, "Button");
            scoreFont = Content.Load<SpriteFont>("Score");
            UserInterface.LoadSpriteFont(Content, "Score");
            TextureLibrary.LoadTexture(Content, "bg");
            TextureLibrary.LoadTexture(Content, "white");
            TextureLibrary.LoadTexture(Content, "shield");
            //UserInterface.AddButton(TextureLibrary.GetTexture("Button"), "Try Again", new Vector2(.5f, .5f), Vector2.One, Window.ClientBounds.Size.ToVector2(), 2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.P))
                Exit();

            // TODO: Add your update logic here
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();
            playerPos = mouseState.Position.ToVector2();

            player.Update(deltaTime, Keyboard.GetState(), Mouse.GetState(), Window.ClientBounds.Size);

            if (!player.GetAlive() && once == false)
            {
                once = true;
                UserInterface.AddButton(TextureLibrary.GetTexture("Button"), "Retry", new Vector2(.5f, .5f), Vector2.One, Window.ClientBounds.Size.ToVector2(), 1);
            }

            powerAttack = player.GetPower();

            shieldPos = new Vector2(rnd.Next(0, 500), rnd.Next(0, 900));

            if (!UserInterface.GetPause())
            {
                bgPos2.Y += 1;
                bgPos.Y += 1;

                if (bgPos.Y >= 1080)
                {
                    bgPos.Y = -1080;
                }
                if (bgPos2.Y >= 1080)
                {
                    bgPos2.Y = -1080;
                }
            }
            UserInterface.GameState exit = UserInterface.Update(keyboardState, mouseState, player);
            switch (exit)
            {
                case UserInterface.GameState.play:
                    break;
                case UserInterface.GameState.exit:
                    Exit();
                    break;
                case UserInterface.GameState.restart:
                    Reload();
                    break;
                case UserInterface.GameState.dead:
                    break;
                default:
                    break;
            }

            if (!UserInterface.GetPause())
            {
                player.Update(deltaTime, keyboardState, mouseState, Window.ClientBounds.Size);
            }

            if (player.GetAlive())
            {
                spawnEnemyTimer += deltaTime;
                if (spawnEnemyTimer <= spawnEnemy)
                {
                    spawnEnemyTimer += deltaTime;
                }
                if (spawnEnemyTimer >= spawnEnemy)
                {
                    startPos = new Vector2(rnd.Next(0, 500), 0);

                    enemies.Add(new Enemy(TextureLibrary.GetTexture("bad"), new Vector2(.4f, .4f), 0, 50, startPos, 500, 1));

                    spawnEnemyTimer = 0;
                }

                //shield.Update(deltaTime, player);

                //spawnShieldTimer -= deltaTime;
                //if (spawnShieldTimer >= spawnShield)
                //{
                //    spawnShieldTimer -= deltaTime;
                //}
                //if (spawnShieldTimer <= spawnShield)
                //{
                //    shield.SetPosition(shieldPos);

                //    spawnShieldTimer = 5;
                //}
            }

            if (!UserInterface.GetPause() && player.GetAlive() == true)
            {
                score += deltaTime * 2.3f;
            }

            scoreInt = Convert.ToInt32(score);

            scoreSting = scoreInt.ToString();

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetAlive())
                {
                    enemies[i].Update(gameTime, deltaTime, player, Window.ClientBounds.Height);
                }
                //player.DetectCollision(enemies);
                else
                {
                    enemies.RemoveAt(i);
                }
            }

            BulletManager.Update(deltaTime, player, enemies);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            spriteBatch.Begin();
            spriteBatch.Draw(TextureLibrary.GetTexture("bg"), bgPos, mainFrame, Color.White);
            spriteBatch.Draw(TextureLibrary.GetTexture("bg"), bgPos2, mainFrame, Color.White);
            if (show)
            {
                player.Draw(spriteBatch);
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }
            spriteBatch.DrawString(scoreFont, "Score: " + scoreSting, new Vector2(10, 10), Color.White);
            BulletManager.Draw(spriteBatch);
            UserInterface.Draw(spriteBatch, player);

            if (spawnShieldTimer <= spawnEnemy)
            {
                shield.Draw(spriteBatch);
            }

            if (powerAttack == true)
            {
                spriteBatch.DrawString(scoreFont, "Power = active", new Vector2(180, 10), Color.White);
            }
            else if (powerAttack == false)
            {
                spriteBatch.DrawString(scoreFont, "Power = down", new Vector2(180, 10), Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
            
        }

        private void Reload()
        {
            IsMouseVisible = true;

            once = false;
            score = 0;
            //health = 1;
            show = true;

            enemies = new List<Enemy>();
            numEnemies = 3;

            spawnEnemy = 3f;
            spawnEnemyTimer = 0;

            bgPos2 = new Vector2(0, -1080);
            bgPos = new Vector2(0, 0);

            //mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            mainFrame = new Rectangle(0, 0, 1920, 1080);


            playerPos = new Vector2(230, 900);

            for (int i = 0; i < numEnemies; i++)
            {
                startPos = new Vector2(rnd.Next(0, 500), 0);
                //enemies.Add(new Enemy(TextureLibrary.GetTexture("bad"), new Vector2(5f, 5f), 0f, 100f, startPos));
                enemies.Add(new Enemy(TextureLibrary.GetTexture("bad"), new Vector2(.4f, .4f), 0, 50, startPos, 500, 1));
            }

            player = new Player(TextureLibrary.GetTexture("player"), playerPos, 200, new Vector2(.5f, .5f), 0, Color.White, 1000, 1);

            UserInterface.AddButton(TextureLibrary.GetTexture("Button"), "Continue", new Vector2(.5f, .5f), Vector2.One, Window.ClientBounds.Size.ToVector2(), 1);
            UserInterface.AddButton(TextureLibrary.GetTexture("Button"), "Exit", new Vector2(.5f, .5f), Vector2.One, Window.ClientBounds.Size.ToVector2(), 1);
        }
    }
}
