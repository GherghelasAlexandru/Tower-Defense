﻿using System;
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
    public class InstructionsState : State
    {
        private List<Button> _button;

        public InstructionsState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var font = _content.Load<SpriteFont>("Fonts/Font");



            var chooseBackButton = new Button(buttonTexture, font)
            {
                Position = new Vector2(550, 450),
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
        }
    }
}

