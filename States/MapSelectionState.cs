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
    public class MapSelectionState : State
    {
        private List<Button> _button;

        Button chooseFirstMapButton;
        Button chooseSecondMapButton;

        public SpriteFont textFontTitle;

        public MapSelectionState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {

            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var firstMapTexture = _content.Load<Texture2D>("Controls/FirstMap");
            var secondMapTexture= _content.Load<Texture2D>("Controls/SecondMap");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            textFontTitle = _content.Load<SpriteFont>("Fonts/TextFont");

            chooseFirstMapButton = new Button(firstMapTexture, buttonFont)
            {
                Position = new Vector2(400, 250),
                Text = "1",
            };

            chooseFirstMapButton.Click += MapButton_Click;

           chooseSecondMapButton = new Button(secondMapTexture, buttonFont)
            {
                Position = new Vector2(650, 250),
                Text = "2", 
            };

            chooseSecondMapButton.Click += MapButton_Click;

            var chooseBackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(550, 450),
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
            foreach (var button in _button)
                button.Draw(gameTime, spriteBatch);

            string tempStr = "Choose your map";
            spriteBatch.DrawString(textFontTitle, tempStr, new Vector2(420, 150), Color.Black);
        }

        private void MapButton_Click(object sender, EventArgs e)
        {
            
            _game.ChangeState(_game.gameState);
            if (chooseFirstMapButton.Clicked)
            {
                _game.gameState.AddMap(_game.gameState.map1);


                _game.gameState.AddEnemy(_game.gameState.crab);
            }

            else if(chooseSecondMapButton.Clicked)
            {
                _game.gameState.AddMap(_game.gameState.map2);
                
                   _game.gameState.AddEnemy(_game.gameState.crab);
            }

            
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
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
    }
}
