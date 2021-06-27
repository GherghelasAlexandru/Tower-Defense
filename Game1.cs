using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelDefense.Gameplay;
using System;
using TiledSharp;
using PixelDefense.States;
using System.Collections.Generic;
using PixelDefense.Engine;
using PixelDefense.Controls;

namespace PixelDefense
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //MouseState mouse;


        //States
        public GameState gameState;
        public InstructionsState instructionsState;
        public SettingsState settingsState;
        public MapSelectionState mapSelection;
        public ShopState shopState;
        public GameOverState gameOverState;
        public MenuState menuState;
        public MouseState mouseState;
        public SoundControl soundControl;

        public int defaultWidth = 1280;
        public int defaultHeight = 830;

        private State _currentState;
        private State _nextState;

        
       
       
        //shooting sprites
        //private List<Sprite> _sprites;
        //private GameModel _gameModel;

        public static int ScreenWidth { get; internal set; }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = defaultWidth,
                PreferredBackBufferHeight = defaultHeight
            };
            Content.RootDirectory = "Content";
            Globals.appDataFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            Console.WriteLine(Globals.appDataFilePath);
           // graphics.ToggleFullScreen();
            graphics.ApplyChanges();

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

            mouseState = Mouse.GetState();
            Globals.save = new Save("PixelDefense");
            menuState = new MenuState(this, graphics.GraphicsDevice, Content);
            instructionsState = new InstructionsState(this, graphics.GraphicsDevice, Content);
            settingsState = new SettingsState(this, graphics.GraphicsDevice, Content);
            settingsState.ApplyOptions(ApplyOptions);
           
            mapSelection = new MapSelectionState(this, graphics.GraphicsDevice, Content);
            gameState = new GameState(this, graphics.GraphicsDevice, Content);
            shopState = new ShopState(this, graphics.GraphicsDevice, Content);
            gameOverState = new GameOverState(this, graphics.GraphicsDevice, Content);
            
            IsMouseVisible = true;
            base.Initialize();
        }



        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Globals.content = this.Content;
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.soundControl = new SoundControl("Sounds/bgMusic2", Content, settingsState);
            Globals.soundControl.AddSound("game over", "Sounds/gameOver", .10f);
            Globals.soundControl.AddSound("click", "Sounds/button-click", .10f);
            Globals.soundControl.AddSound("shop", "Sounds/shop_open", .25f);
            Globals.soundControl.AddSound("negative", "Sounds/wrong", .25f);
            Globals.soundControl.AddSound("shoot", "Sounds/shoot", .25f);

            _currentState = menuState;


            // TODO: use this.Content to load your game content here
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
            /*mouse = Mouse.GetState();
            Console.WriteLine("X:{0} Y:{1}.", mouse.X, mouse.Y);*/
            if(gameOverState.IsRestarted == true)
            {
                Initialize();
            }
            
            if (_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public virtual void ApplyOptions(object option)
        {
            FormOption musicVolume = settingsState.GetOptionValue("Bg Music");
            float musicVolumePercent = 1.0f;
            if (musicVolume != null)
            {
                musicVolumePercent = (float)Convert.ToDecimal(musicVolume.GetValue()) / 30.0f;
            }
            Globals.soundControl.AdjustVolume(musicVolumePercent);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
        
            graphics.GraphicsDevice.Clear(Color.Beige);
            
            spriteBatch.Begin();

            _currentState.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
