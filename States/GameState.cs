﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelDefense.Controls;
using PixelDefense.Engine;

namespace PixelDefense.States
{
    class GameState : State
    {
        private List<IActor> _components;
        List<Map> maps;
        //shooting sprites
        private List<Sprite> _sprites;

        //ShopState shop;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            maps = new List<Map>();

            //shop = new ShopState(game, graphicsDevice, content);

            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var basicTowerTexture = content.Load<Texture2D>("Tower/tower");

            _sprites = new List<Sprite>()
            {
                new BasicTower(basicTowerTexture)
                {
                    Position = new Vector2(200, 200),
                    Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")),
                },
            };

            var chooseBackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(480, 0),
                Text = "Main menu",
            };

            chooseBackButton.Click += ChooseBackButton_Click;

            var shopButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(320, 0),
                Text = "Shop",
            };

            shopButton.Click += shopButton_click;

            _components = new List<IActor>()
            {
            chooseBackButton,
            shopButton,
            };

        }

        private void ChooseBackButton_Click(object sender, EventArgs e)
        {
            // to be modified to change back to the gameState
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));

        }

        private void shopButton_click(object sender, EventArgs e)
        {
            // to be modified to change back to the gameState
            _game.ChangeState(new ShopState(_game, _graphicsDevice, _content));

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public void postupdate()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);
           

            foreach (var component in _components)
                component.Update(gameTime);

            postupdate();
        }

        public void AddMap(Map map)
        {
            maps.Add(map);
        }

        public void DrawMap(SpriteBatch spriteBatch)
        {
            foreach (var map in maps)
            {

                map.DrawGrass(spriteBatch);

                map.DrawPath(spriteBatch);

                map.DrawShadow(spriteBatch);

                map.DrawBase(spriteBatch);

                map.DrawDecorations(spriteBatch);

            }
        }

        public void DrawSprites(SpriteBatch spriteBatch)

        {
            foreach (var sprite in _sprites.ToArray())
                sprite.Draw(spriteBatch);

        }

        public void DrawButtons(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            MapSelectionState mapSelection = new MapSelectionState(_game, _graphicsDevice, _content);

            DrawMap(spriteBatch);
            DrawButtons(gameTime, spriteBatch);
            DrawSprites(spriteBatch);



        }
    }
}
