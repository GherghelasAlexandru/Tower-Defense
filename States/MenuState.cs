﻿using System;
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
        private List<IActor> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(250, 200),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var instructionsButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(250, 250),
                Text = "Instructions",
            };

            instructionsButton.Click += IntructionsButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(250, 300),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<IActor>()
      {
        newGameButton,
        instructionsButton,
        quitGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
          

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

          
        }

        private void IntructionsButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new InstructionsState(_game, _graphicsDevice, _content));
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MapSelectionState(_game, _graphicsDevice, _content));
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

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
