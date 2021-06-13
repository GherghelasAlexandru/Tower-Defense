using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Controls;

namespace PixelDefense.States
{
    public class MenuState : State
    {
        private List<Button> _button;

        public SpriteFont textFontTitle;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            textFontTitle = _content.Load<SpriteFont>("Fonts/TextFont");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 350),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var instructionsButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 400),
                Text = "Instructions",
            };

            instructionsButton.Click += IntructionsButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 450),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _button = new List<Button>()
      {
        newGameButton,
        instructionsButton,
        quitGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
          

            foreach (var button in _button)
                button.Draw(gameTime, spriteBatch);

            string tempStr = "Pixel Defense";
            spriteBatch.DrawString(textFontTitle, tempStr, new Vector2(450, 200), Color.Black);

        }

        private void IntructionsButton_Click(object sender, EventArgs e)
        {
            _game.click.playSound();
            _game.ChangeState(_game.instructionsState);
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.click.playSound();
            _game.ChangeState(_game.mapSelection);
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

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.click.playSound();
            _game.Exit();
        }
    }
}
