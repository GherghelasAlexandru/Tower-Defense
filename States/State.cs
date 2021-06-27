using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.States
{
    public abstract class State
    {
        #region Fields

        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected Game1 _game;
       
        protected SpriteFont textFont;

        #endregion

        #region Methods

        public State(Game1 _game, GraphicsDevice _graphicsDevice, ContentManager _content)
        {
            this._game = _game;

            this._graphicsDevice = _graphicsDevice;

            this._content = _content; 
            
            textFont = _content.Load<SpriteFont>("Fonts/Font");


        }

        //get and set methods
        public void SetContent(ContentManager _content)
        {
            this._content = _content;
        }

        public ContentManager GetContent()
        {
            return _content;
        }

        public void SetGame(Game1 _game)
        {
            this._game = _game;
        }

        public Game1 GetGame()
        {
            return _game;
        }

       public void SetGraphicsDevice(GraphicsDevice graphics)
        {
            _graphicsDevice = graphics;
        }

        public GraphicsDevice GetGraphicsDevice()
        {
            return _graphicsDevice;
        }



        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        //public abstract void PostUpdate(GameTime gameTime);

        public abstract void Update(GameTime gameTime);

        #endregion
    }
}
