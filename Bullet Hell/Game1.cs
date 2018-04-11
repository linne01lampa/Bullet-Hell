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

        float health;
        bool show;

        List<Enemy> enemies;
        Dictionary<string, Texture2D> textures;

        Vector2 startPos;
        Random rnd = new Random();
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 580;
            graphics.PreferredBackBufferHeight = 940;
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
            textures = new Dictionary<string, Texture2D>();
            base.Initialize();

            IsMouseVisible = false;

            health = 1;
            show = true;

            enemies = new List<Enemy>();
            numEnemies = 5;

            for (int i = 0; i < numEnemies; i++)
            {
                startPos = new Vector2(rnd.Next(0, 500), 0);
                enemies.Add(new Enemy(textures["enemy"], new Vector2(5f, 5f), 0f, 100f, startPos));
            }

            player = new Player(textures["player"], playerPos, new Vector2(5f, 5f), 200);
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
            textures.Add("player", Content.Load<Texture2D>("player"));
            textures.Add("enemy", Content.Load<Texture2D>("bad"));
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseState mouseState = Mouse.GetState();

            playerPos = mouseState.Position.ToVector2();

            player.Update(deltaTime, playerPos);

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime);

                if (player.DetectCollision(enemies[i]) == true)
                {
                    health--;
                    if (health <= 0)
                    {
                        show = false;
                    }
                }
            }

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
            if (show)
            {
                player.Draw(spriteBatch);
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
