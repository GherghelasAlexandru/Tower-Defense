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
    public class ShopState : State
    {
        List<Button> _button;

        public Texture2D bkg;

        BasicTower basicTower1;
        BasicTower basicTower2;
        BasicTower basicTower3;
        BasicTower basicTower4;
        BasicTower basicTower5;

        public SpriteFont textFont;


        private readonly List<Sprite> basicTowers;
        public ShopState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {

            basicTowers = new List<Sprite>();

            basicTower1 = new BasicTower(content.Load<Texture2D>("Tower/T1"),10) { _position = new Vector2(120, 75), Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")) };
            basicTower2 = new BasicTower(content.Load<Texture2D>("Tower/T2"),15) { _position = new Vector2(120, 150), Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")) };
            basicTower3 = new BasicTower(content.Load<Texture2D>("Tower/T3"),20) { _position = new Vector2(120, 225), Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")) };
            basicTower4 = new BasicTower(content.Load<Texture2D>("Tower/T4"),25) { _position = new Vector2(120, 300), Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")) };
            basicTower5 = new BasicTower(content.Load<Texture2D>("Tower/T5"),30) { _position = new Vector2(120, 375), Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")) };

            AddBasicTower(basicTower1);
            AddBasicTower(basicTower2);
            AddBasicTower(basicTower3);
            AddBasicTower(basicTower4);
            AddBasicTower(basicTower5);

            bkg = content.Load<Texture2D>("Controls/bkg");
            textFont = _content.Load<SpriteFont>("Fonts/Font");

            var basicTowerTexture = content.Load<Texture2D>("Tower/tower");
            var buttonTexture = _content.Load<Texture2D>("Controls/xxButton");
            var buttonTexture4 = _content.Load<Texture2D>("Controls/button4");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            
            var buyButton = new Button(buttonTexture4, buttonFont)
            {
                Position = new Vector2(120, 75),
            };

            buyButton.Click += BuyButton_Click;

            var chooseBackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(490, 5),
            };

            chooseBackButton.Click += BackButton_Click;

            _button = new List<Button>()
            {
            buyButton,
            chooseBackButton,
            };
        }

        public void AddBasicTower(BasicTower basicTower)
        {
            this.basicTowers.Add(basicTower);
        }

        private void BuyButton_Click(object sender, EventArgs e)
        {
            // to be modified to change back to the gameState
            _game.ChangeState(_game.gameState);
           _game.gameState.AddTower(basicTower1);
         
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // to be modified to change back to the gameState
            _game.ChangeState(_game.gameState);
        }

        public override void PostUpdate(GameTime gameTime)
        {
           
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var button in _button)
                button.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(bkg, new Vector2(100, 0), Color.Gray);

            string deployable = "Deployable";
            string gold = "Gold";
            spriteBatch.DrawString(textFont, deployable, new Vector2(120, 40), Color.White);
            spriteBatch.DrawString(textFont, gold, new Vector2(300, 40), Color.White);

            

            foreach (var button in _button)
                button.Draw(gameTime, spriteBatch);

            foreach (var tower in basicTowers)
            {
                tower.Draw(spriteBatch);
            }
        }
    }
}