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
    public class MapSelectionState : State
    {
        private List<Button> _button;

        public bool IsFirstMapChosen;
        public bool IsSecondMapChosen;

      
        public MapSelectionState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            IsFirstMapChosen = false;
            IsSecondMapChosen = false;
        
            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
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
        }

      

        private void SecondMapButton_Click(object sender, EventArgs e)
        {
            
           
            _game.ChangeState(_game.gameState);

            _game.gameState.AddMap(map2);
            _game.gameState.RemoveMap(map1);
        }

        private void FirstMapButton_Click(object sender, EventArgs e)
        {

            _game.ChangeState(_game.gameState);

            _game.gameState.AddMap(map1);
            _game.gameState.RemoveMap(map2);
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
