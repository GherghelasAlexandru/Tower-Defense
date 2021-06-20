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
    public class InstructionsState : State
    {
        private List<Button> _button;
        private Texture2D firstSlide;
        private Texture2D secondSlide;
        private Texture2D thirdSlide;
        private Texture2D fourthSlide;
        public Texture2D debugColor;
        public SpriteFont font;
        public Rectangle textBox;
        public InstructionsState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
             font = _content.Load<SpriteFont>("Fonts/Font");
             this.firstSlide = _content.Load<Texture2D>("Controls/firstSlide");
             this.secondSlide = _content.Load<Texture2D>("Controls/secondSlide");
            this.thirdSlide = _content.Load<Texture2D>("Controls/thirdSlide");
            this.fourthSlide = _content.Load<Texture2D>("Controls/fourthSlide");
            this.debugColor = _content.Load<Texture2D>("Controls/textBg");


            var chooseBackButton = new Button(buttonTexture, font)
            {
                Position = new Vector2(550, 760),
                Text = "Got it!",
            };

            chooseBackButton.Click += BackButton_Click;

            _button = new List<Button>()
            {
            chooseBackButton,
            };

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Globals.soundControl.playSound("click");
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

        public Rectangle bgCoordinates(int X, int Y)
        {
            textBox = new Rectangle(X, Y, 480, 170);
            return textBox;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var button in _button)
                button.Draw(gameTime, spriteBatch);
            
            spriteBatch.Draw(firstSlide, new Vector2( 65,20));
            spriteBatch.Draw(debugColor, bgCoordinates(600, 20), Color.White);
            spriteBatch.DrawString(font, parseText("The main goal of the game is to defend the base against the wawes of enemies"), new Vector2(10 + textBox.X, textBox.Y + 10), Color.White);

            spriteBatch.Draw(secondSlide, new Vector2(65, 200));
            spriteBatch.Draw(debugColor, bgCoordinates(600, 200), Color.White);
            spriteBatch.DrawString(font, parseText("To do so you will need to buy and place various towers. To earn money for towers you will need to kill enemies. Some of the towers are fast but weak and some of them are slow but deal a lot of damage!"), new Vector2(10 + textBox.X, textBox.Y + 10), Color.White);

            spriteBatch.Draw(thirdSlide, new Vector2(65, 390));
            spriteBatch.Draw(debugColor, bgCoordinates(600, 390), Color.White);
            spriteBatch.DrawString(font, parseText("With each wave, the amount of enemies will increase, Remember to put new towers when it is possible"), new Vector2(10 + textBox.X, textBox.Y + 10), Color.White);

            spriteBatch.Draw(fourthSlide, new Vector2(65, 580));
            spriteBatch.Draw(debugColor, bgCoordinates(600, 580), Color.White);
            spriteBatch.DrawString(font, parseText("If enemies will reach the base, they will attack it! After the base health reaches zero you will lose"), new Vector2(10+textBox.X, textBox.Y + 10), Color.White);
        }

        private String parseText(String text)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            foreach (String word in wordArray)
            {
                if (font.MeasureString(line + word).Length() > textBox.Width - 20)
                {
                    returnString = returnString + line + '\n';
                    line = String.Empty;
                }

                line = line + word + ' ';
            }

            return returnString + line;
        }
    }
}

