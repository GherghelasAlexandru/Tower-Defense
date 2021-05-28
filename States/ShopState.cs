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
using PixelDefense.Gameplay;

namespace PixelDefense.States
{
    public class ShopState : State
    {
        List<Button> _button;

        GameState gameState;

        public ShopState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            gameState = new GameState(game, graphicsDevice, content);

            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var chooseBackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(480, 0),
                Text = "Back",
            };

            chooseBackButton.Click += BackButton_Click;

            _button = new List<Button>()
            {
            chooseBackButton,
            };

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // to be modified to change back to the gameState
            _game.ChangeState(gameState);
        }

        public override void PostUpdate(GameTime gameTime)
        {
           
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var button in _button)
                button.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            foreach (var button in _button)
                button.Draw(gameTime, spriteBatch);

            spriteBatch.Draw(_content.Load<Texture2D>("Tower/T1"), new Vector2(10, 10), Color.White);
            spriteBatch.Draw(_content.Load<Texture2D>("Tower/T2"), new Vector2(10, 100), Color.White);
            spriteBatch.Draw(_content.Load<Texture2D>("Tower/T3"), new Vector2(10, 180), Color.White);
            spriteBatch.Draw(_content.Load<Texture2D>("Tower/T4"), new Vector2(10, 250), Color.White);
            spriteBatch.Draw(_content.Load<Texture2D>("Tower/T5"), new Vector2(10, 340), Color.White);
        }

    }
}