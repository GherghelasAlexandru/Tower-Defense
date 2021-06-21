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
    public class GameOverState : State
    {
        public SpriteFont textFontTitle;
        public SpriteFont buttonFont;
        private List<Button> _button;
        public bool IsRestarted = false;
        public GameOverState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            
            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var font = _content.Load<SpriteFont>("Fonts/Font");
            textFontTitle = _content.Load<SpriteFont>("Fonts/TextFont");
            buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var chooseBackButton = new Button(buttonTexture, font)
            {
                Position = new Vector2(550, 460),
                Text = "Go to main menu",
            };

            chooseBackButton.Click += BackButton_Click;

            _button = new List<Button>()
            {
            chooseBackButton,
            };
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            IsRestarted = true;
            _game.ChangeState(_game.menuState);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
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

            string tempStr = "Game over!";
            spriteBatch.DrawString(textFontTitle, tempStr, new Vector2(490, 250), Color.Black);
            spriteBatch.DrawString(buttonFont, "You made it to wave " + _game.gameState.wave.GetWaveNumber(), new Vector2(530, 340), Color.Black);
            spriteBatch.DrawString(buttonFont, "Your score is " + _game.gameState.score, new Vector2(550, 380), Color.Black);
        }
    }
}

