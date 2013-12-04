using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Chess
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ChessGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Camera camera;

        private SpriteFont spriteFont;

        public ChessGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            camera = new Camera();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //chessPawn = Content.Load<Texture2D>("pawn");

            spriteFont = Content.Load<SpriteFont>("Times");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            //Visar del 1
            //DrawCordinatesWhite(new Vector2(10f, 10f), 0, 0);
            //DrawCordinatesWhite(new Vector2(10f, 110f), 7, 0);
            //DrawCordinatesWhite(new Vector2(10f, 210f), 1, 7);
            //DrawCordinatesWhite(new Vector2(10f, 310f), 7, 7);

            //Visar del 2
            DrawCordinatesBlack(new Vector2(10f, 10f), 0, 0);
            DrawCordinatesBlack(new Vector2(10f, 110f), 6, 0);
            DrawCordinatesBlack(new Vector2(10f, 210f), 2, 7);
            DrawCordinatesBlack(new Vector2(10f, 310f), 7, 7);

            //Visar del 3 
            //DrawCordinatesResize(320, 240, new Vector2(10f, 10f), 0, 0);
            //DrawCordinatesResize(320, 240, new Vector2(10f, 110f), 7, 0);
            //DrawCordinatesResize(320, 240, new Vector2(10f, 210f), 1, 7);
            //DrawCordinatesResize(320, 240, new Vector2(10f, 310f), 7, 7);

            //DrawCordinatesResizeCeepRatio(320, 240, new Vector2(10f, 10f), 0, 0);
            //DrawCordinatesResizeCeepRatio(320, 240, new Vector2(10f, 110f), 7, 0, false);
            //DrawCordinatesResizeCeepRatio(320, 240, new Vector2(10f, 210f), 1, 7);
            //DrawCordinatesResizeCeepRatio(320, 240, new Vector2(10f, 310f), 7, 7, false);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //Visar visuella kordinater för rutorna med ny storlek på fönstret där marginaler skapas för skillnad mellan bredd/höjd
        public void DrawCordinatesResizeCeepRatio(int screenX, int screenY, Vector2 vector, int logicX, int logicY, bool white = true)
        {
            KeyValuePair<int, int> cordinatPair = camera.getVisualCordinatesNewScaleCeepRatio(320, 240, logicX, logicY, white);

            string answer = "Logisk Kordinat: X:" + logicX + ", Y:" + logicY
                + "\nVisuell kordinat: X:" + cordinatPair.Key + ", Y:" + cordinatPair.Value;
            spriteBatch.DrawString(spriteFont, answer, vector, Color.Black);
        }

        //Visar visuella kordinater för rutorna med ny storlek på fönstret 
        //(Skalar inte om så bilden blir förvrängd om det är skillnad mellan höjd/bredd)
        public void DrawCordinatesResize(int screenX, int screenY, Vector2 vector, int logicX, int logicY)
        {
            KeyValuePair<int, int> cordinatPair = camera.getVisualCordinatesNewScale(320, 240, logicX, logicY);

            string answer = "Logisk Kordinat: X:" + logicX + ", Y:" + logicY
                + "\nVisuell kordinat: X:" + cordinatPair.Key + ", Y:" + cordinatPair.Value;
            spriteBatch.DrawString(spriteFont, answer, vector, Color.Black);
        }

        //Visar visuella kordinater för chackrutorna
        public void DrawCordinatesWhite(Vector2 vector, int logicX, int logicY)
        {
            KeyValuePair<int, int> cordinatPair = camera.getVisualCordinatesWhite(logicX, logicY);

            string answer = "Logisk Kordinat: X:" + logicX + ", Y:" + logicY
                + "\nVisuell kordinat: X:" + cordinatPair.Key + ", Y:" + cordinatPair.Value;
            spriteBatch.DrawString(spriteFont, answer, vector, Color.Black);
        }

        //Visar visuella kordinater för chackrutorna sett ur svart's synvinkel (Omvänd spelplan)
        public void DrawCordinatesBlack(Vector2 vector, int logicX, int logicY)
        {
            KeyValuePair<int, int> cordinatPair = camera.getVisualCordinatesBlack(logicX, logicY);

            string answer = "Logisk Kordinat:  X:" + logicX + ", Y:" + logicY
                + "\nVisuell kordinat: X:" + cordinatPair.Key + ", Y:" + cordinatPair.Value;
            spriteBatch.DrawString(spriteFont, answer, vector, Color.Black);
        }
    }
}
