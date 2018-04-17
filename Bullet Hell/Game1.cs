﻿using Microsoft.Xna.Framework;
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

        SpriteFont scoreFont;
        string scoreSting;
        float score;
        int scoreInt;

        float health;
        bool show;

        List<Enemy> enemies;
        //Dictionary<string, Texture2D> textures;

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
            base.Initialize();

            IsMouseVisible = false;

            score = 0;
            health = 1;
            show = true;

            enemies = new List<Enemy>();
            numEnemies = 5;

            for (int i = 0; i < numEnemies; i++)
            {
                startPos = new Vector2(rnd.Next(0, 500), 0);
                enemies.Add(new Enemy(TextureLibrary.GetTexture("bad"), new Vector2(5f, 5f), 0f, 100f, startPos));
            }

            player = new Player(TextureLibrary.GetTexture("player"), playerPos, new Vector2(5, 5), 200);
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
            scoreFont = Content.Load<SpriteFont>("Score");
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

            player.Update(deltaTime, playerPos, enemies);

            score += deltaTime * 2.3f;

            scoreInt = Convert.ToInt32(score);

            scoreSting = scoreInt.ToString();

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime, deltaTime);

                player.DetectCollision(enemies);
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
            spriteBatch.DrawString(scoreFont, "Score: " + scoreSting, new Vector2( 10 , 10), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
