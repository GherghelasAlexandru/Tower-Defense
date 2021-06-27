using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Controls;
using PixelDefense.Engine;

namespace PixelDefense.States
{
    public class MenuState : State
    {
        private List<Button> _button;

        public SpriteFont textFontTitle;

        private Texture2D Background;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            Background = _content.Load<Texture2D>("Controls/Background");
            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            this.textFontTitle = _content.Load<SpriteFont>("Fonts/TextFont");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(570, 400),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var instructionsButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(570, 450),
                Text = "Instructions",
            };

            instructionsButton.Click += IntructionsButton_Click;

            var settingsButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(570, 500),
                Text = "Settings",
            };

            settingsButton.Click += SettingsButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(570, 550),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _button = new List<Button>()
      {
        newGameButton,
        instructionsButton,
        settingsButton,
        quitGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            string tempStr = "Pixel Defense";
            spriteBatch.DrawString(textFontTitle, tempStr, new Vector2(450, 200), Color.Black);
            spriteBatch.Draw(Background, new Vector2(0, 0));
            foreach (var button in _button)
                button.Draw(gameTime, spriteBatch);


        }

        private void IntructionsButton_Click(object sender, EventArgs e)
        {
            Globals.soundControl.PlaySound("click");
            _game.ChangeState(_game.instructionsState);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Globals.soundControl.PlaySound("click");
            _game.ChangeState(_game.settingsState);
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            Globals.soundControl.PlaySound("click");
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
            _game.Exit();
        }
    }
}
