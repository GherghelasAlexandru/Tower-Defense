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
using PixelDefense.Gameplay;

namespace PixelDefense.States
{
    public class MapSelectionState : State
    {
        private List<Button> _button;

        public Button chooseFirstMapButton;
        public Button chooseSecondMapButton;

        public SpriteFont textFontTitle;

        private Texture2D Background;

        public MapSelectionState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            Background = _content.Load<Texture2D>("Controls/Background2");
            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var firstMapTexture = _content.Load<Texture2D>("Controls/FirstMap");
            var secondMapTexture= _content.Load<Texture2D>("Controls/SecondMap");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            textFontTitle = _content.Load<SpriteFont>("Fonts/TextFont");

            chooseFirstMapButton = new Button(firstMapTexture, buttonFont)
            {
                Position = new Vector2(400, 300),
                Text = "1",
            };

            chooseFirstMapButton.Click += MapButton_Click;

           chooseSecondMapButton = new Button(secondMapTexture, buttonFont)
            {
                Position = new Vector2(650, 300),
                Text = "2", 
            };

            chooseSecondMapButton.Click += MapButton_Click;

            var chooseBackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 500),
                Text = "Back",
            };

            chooseBackButton.Click += BackButton_Click;

            _button = new List<Button>()
            {
            chooseFirstMapButton,
            chooseSecondMapButton,
            chooseBackButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Vector2(0, 0));
            foreach (var button in _button)
                button.Draw(gameTime, spriteBatch);

            string tempStr = "Choose your map";
            spriteBatch.DrawString(textFontTitle, tempStr, new Vector2(420, 120), Color.Black);
        }

        private void MapButton_Click(object sender, EventArgs e)
        {
            Globals.soundControl.PlaySound("click");
            _game.ChangeState(_game.gameState);

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Globals.soundControl.PlaySound("click");
            _game.ChangeState(_game.menuState);
        }

     /*   public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }*/

        public override void Update(GameTime gameTime)
        {
            foreach (var button in _button)
                button.Update(gameTime);
        }
    }
}
