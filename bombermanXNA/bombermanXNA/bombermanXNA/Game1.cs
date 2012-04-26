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

using bomberman.Main;
using bombermanXNA.Menus;

namespace bombermanXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 

    enum KeyState
    {
        PRESSED,
        FREE
    };

    struct KBState
    {
        public KeyState left;
        public KeyState right;
        public KeyState up;
        public KeyState down;
        public KeyState space;
        public KeyState escape;
        public KeyState enter;
    };

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        MainMenu menu;
        public bool MenuActive = true;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Texture2D pacman;
        public Texture2D korytarz;
        public Texture2D mur;
        public Texture2D point;
        public Texture2D heart;
        public Texture2D usmiech;
        public Texture2D moneta;
        public Texture2D bomba;
        public Texture2D ogien;
        public Texture2D wrotki;
        public Texture2D zegar;
        public Texture2D balon;
        public Texture2D drzwi;
        public Texture2D duch;
        public Texture2D gabka;
        public Texture2D kropla;
        public Texture2D penguins;
        public SpriteFont FontMenu;

        KBState kb;
        int x;
        int y;
        long LastTicks;
        game g;

        public Game1() 
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
            // TODO: Add your initialization logic here
            x = 0;
            y = 0;

            kb.down = KeyState.FREE;
            kb.up = KeyState.FREE;
            kb.left = KeyState.FREE;
            kb.right = KeyState.FREE;
            kb.space = KeyState.FREE;

            
            LastTicks = 0;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
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

            pacman = Content.Load<Texture2D>("pacman");
            korytarz = Content.Load<Texture2D>("korytarz");
            mur = Content.Load<Texture2D>("mur");
            point = Content.Load<Texture2D>("point");
            heart = Content.Load<Texture2D>("heart");
            usmiech = Content.Load<Texture2D>("usmiech");
            moneta = Content.Load<Texture2D>("moneta");
            bomba = Content.Load<Texture2D>("bomba");
            balon = Content.Load<Texture2D>("balon");
            drzwi = Content.Load<Texture2D>("drzwi");
            duch = Content.Load<Texture2D>("duch");
            gabka = Content.Load<Texture2D>("gabka");
            kropla = Content.Load<Texture2D>("kropla");
            ogien = Content.Load<Texture2D>("ogien");
            wrotki = Content.Load<Texture2D>("wrotki");
            zegar = Content.Load<Texture2D>("zegar");
            FontMenu = Content.Load<SpriteFont>("FontMenu");
            penguins = Content.Load<Texture2D>("Penguins");
            

            g = new game(this);
            g.GameFinished += Finished_handler;
            menu = new MainMenu(this, spriteBatch);
            g.pause();

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
        /// 

        void Finished_handler(object sender, EventArgs e)
        {
            MenuActive = true;
            g = new game(this);
        }

        
        protected void HandleEvents()
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
             //   this.Exit();

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
                kb.down = KeyState.PRESSED;
            else
                kb.down = KeyState.FREE;

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
                kb.up = KeyState.PRESSED;
            else
                kb.up = KeyState.FREE;

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
                kb.left = KeyState.PRESSED;
            else
                kb.left = KeyState.FREE;

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
                kb.right = KeyState.PRESSED;
            else
                kb.right = KeyState.FREE;

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
                kb.space = KeyState.PRESSED;
            else
                kb.space = KeyState.FREE;

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                kb.escape = KeyState.PRESSED;
            else
                kb.escape = KeyState.FREE;

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter))
                kb.enter = KeyState.PRESSED;
            else
                kb.enter = KeyState.FREE;

        }

        public GameTime gameTime;

        protected override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;    
            // TODO: Add your update logic here
            HandleEvents();

            if (MenuActive)
                menu.Update(kb);
            else
                g.Update(kb, LastTicks);
            
            LastTicks = DateTime.Now.Ticks;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Red);
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            if (MenuActive)
                menu.Draw();
            else
                g.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
