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

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this._game = game;

            this._graphicsDevice = graphicsDevice;

            this._content = content; 
            
            this.textFont = _content.Load<SpriteFont>("Fonts/Font");


        }

        //get and set methods
        public void SetContent(ContentManager content)
        {
            this._content = content;
        }

        public ContentManager GetContent()
        {
            return this._content;
        }

        public void SetGame(Game1 game)
        {
            this._game = game;
        }

        public Game1 GetGame()
        {
            return this._game;
        }

       public void SetGraphicsDevice(GraphicsDevice graphics)
        {
            this._graphicsDevice = graphics;
        }

        public GraphicsDevice GetGraphicsDevice()
        {
            return this._graphicsDevice;
        }



        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public abstract void Update(GameTime gameTime);

        #endregion
    }
}
