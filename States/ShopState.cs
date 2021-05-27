using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelDefense.Controls;
using PixelDefense.Engine;

namespace PixelDefense.States
{
    public class ShopState : State
    {
        private List<IActor> _components;
        GameState gameState;
        private ShopState shop;

        public ShopState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            gameState = new GameState(game, graphicsDevice, content, shop);

            var chooseBackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 0),
                Text = "Back",
            };

            chooseBackButton.Click += BackButton_Click;

            _components = new List<IActor>()
            {
            chooseBackButton,
            };

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // to be modified to change back to the gameState
            _game.ChangeState(gameState);
            

        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
