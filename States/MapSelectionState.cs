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
using PixelDefense.Engine;

namespace PixelDefense.States
{
    public class MapSelectionState : State
    {
        private List<IActor> _components;

        public MapSelectionState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var firstMapTexture = _content.Load<Texture2D>("Controls/FirstMap");
            var secondMapTexture= _content.Load<Texture2D>("Controls/SecondMap");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var chooseFirstMapButton = new Button(firstMapTexture, buttonFont)
            {
                Position = new Vector2(100, 150),
                Text = "1",

            };

            chooseFirstMapButton.Click += FirstMapButton_Click;

            var chooseSecondMapButton = new Button(secondMapTexture, buttonFont)
            {
                Position = new Vector2(350, 150),
                Text = "2",
            };

            chooseSecondMapButton.Click += SecondMapButton_Click;

            var chooseBackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(250, 350),
                Text = "Back",
            };

            chooseBackButton.Click += BackButton_Click;

            _components = new List<IActor>()
            {
            chooseFirstMapButton,
            chooseSecondMapButton,
            chooseBackButton,
            };

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
        }

        private void SecondMapButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new SecondMapState(_game, _graphicsDevice, _content));
        }

        private void FirstMapButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new FirstMapState(_game, _graphicsDevice, _content));
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
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
    }
}
