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

        public InstructionsState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var font = _content.Load<SpriteFont>("Fonts/Font");
             this.firstSlide = _content.Load<Texture2D>("Controls/firstSlide");
             this.secondSlide = _content.Load<Texture2D>("Controls/secondSlide");
            this.thirdSlide = _content.Load<Texture2D>("Controls/thirdSlide");
            this.fourthSlide = _content.Load<Texture2D>("Controls/fourthSlide");


            var chooseBackButton = new Button(buttonTexture, font)
            {
                Position = new Vector2(550, 750),
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var button in _button)
                button.Draw(gameTime, spriteBatch);

            spriteBatch.Draw(firstSlide, new Vector2( 65,10));

            spriteBatch.Draw(secondSlide, new Vector2(800, 10));

            spriteBatch.Draw(thirdSlide, new Vector2(65, 400));

            spriteBatch.Draw(fourthSlide, new Vector2(800, 400));


        }
    }
}

