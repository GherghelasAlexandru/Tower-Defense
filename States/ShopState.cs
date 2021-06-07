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
using PixelDefense.Gameplay.Bullets;
using PixelDefense.Gameplay.Towers;

namespace PixelDefense.States
{
    public class ShopState : State
    {
        List<Button> _button;

        public Texture2D bkg;

        Cannon cannonTower;
        RocketLauncher rocketLauncherTower;
        BrokenGun brokenGunTower;
        DoubleCannon doubleCannonTower;
        MachineGun machineGunTower;

        public Texture2D cannonTexture;
        public Texture2D rocketLauncherTexture;
        public Texture2D doubleCannonTexture;
        public Texture2D machineGunTexture;
        public Texture2D brokenGunTexture;

        readonly Button buyCannonButton;
        readonly Button buyBrokenGunButton;
        readonly Button buyRocketLauncherButton;
        readonly Button buyDoubleCannonButton;
        readonly Button buyMachineGunButton;


        public SpriteFont textFont;


        private readonly List<Sprite> basicTowers;
        public ShopState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {

            var buttonTexture = _content.Load<Texture2D>("Controls/xxButton");
            var buttonTexture4 = _content.Load<Texture2D>("Controls/button4");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            cannonTexture = content.Load<Texture2D>("Tower/T1");
            rocketLauncherTexture = content.Load<Texture2D>("Tower/T5");
            doubleCannonTexture = content.Load<Texture2D>("Tower/T4");
            machineGunTexture = content.Load<Texture2D>("Tower/T3");
            brokenGunTexture = content.Load<Texture2D>("Tower/T2");

            buyCannonButton = new Button(buttonTexture4, buttonFont)
            {
                Position = new Vector2(930, 160),
            };

            buyMachineGunButton = new Button(buttonTexture4, buttonFont)
            {
                Position = new Vector2(930, 280),
            };

            buyDoubleCannonButton = new Button(buttonTexture4, buttonFont)
            {
                Position = new Vector2(930, 400),
            };

            buyRocketLauncherButton = new Button(buttonTexture4, buttonFont)
            {
                Position = new Vector2(930, 520),
            };

            buyBrokenGunButton = new Button(buttonTexture4, buttonFont)
            {
                Position = new Vector2(930, 640),
            };

            basicTowers = new List<Sprite>();

            cannonTower = new Cannon(cannonTexture) { _position = new Vector2(190, 160) };

            machineGunTower = new MachineGun(machineGunTexture) { _position = new Vector2(190, 280) };

            doubleCannonTower = new DoubleCannon(doubleCannonTexture) { _position = new Vector2(190, 400) };

            rocketLauncherTower = new RocketLauncher(rocketLauncherTexture) { _position = new Vector2(190, 520) };

            brokenGunTower = new BrokenGun(brokenGunTexture) { _position = new Vector2(190, 640) };


            /* basicTower2 = new BasicTower(content.Load<Texture2D>("Tower/T2"),15) { _position = new Vector2(120, 150), Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")) };
             basicTower3 = new BasicTower(content.Load<Texture2D>("Tower/T3"),20) { _position = new Vector2(120, 225), Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")) };
             basicTower4 = new BasicTower(content.Load<Texture2D>("Tower/T4"),25) { _position = new Vector2(120, 300), Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")) };
             basicTower5 = new BasicTower(content.Load<Texture2D>("Tower/T5"),30) { _position = new Vector2(120, 375), Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")) };
 */
            AddBasicTower(cannonTower);
            AddBasicTower(brokenGunTower);
            AddBasicTower(rocketLauncherTower);
            AddBasicTower(doubleCannonTower);
            AddBasicTower(machineGunTower);
          

            bkg = content.Load<Texture2D>("Controls/bkg");
            textFont = _content.Load<SpriteFont>("Fonts/Font");


            buyMachineGunButton.Click += BuyButton_Click;
            buyDoubleCannonButton.Click += BuyButton_Click;
            buyRocketLauncherButton.Click += BuyButton_Click;
            buyBrokenGunButton.Click += BuyButton_Click;
            buyCannonButton.Click += BuyButton_Click;

            var chooseBackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(1085, 50),
            };

            chooseBackButton.Click += BackButton_Click;

            _button = new List<Button>()
            {
            buyCannonButton,
            buyBrokenGunButton,
            buyRocketLauncherButton,
            buyDoubleCannonButton,
            buyMachineGunButton,
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
            if (buyCannonButton.Clicked)
            {
                _game.gameState.AddTower(new Cannon(cannonTexture) { Bullet = new Grapeshot(_content.Load<Texture2D>("Tower/bullet")) });
                buyCannonButton.Clicked = false;
            }
            else if(buyBrokenGunButton.Clicked)
            {
                _game.gameState.AddTower(new BrokenGun(brokenGunTexture) { Bullet = new Brokenshot(_content.Load<Texture2D>("Tower/bullet")) });
                buyBrokenGunButton.Clicked = false;
            }
            if(buyRocketLauncherButton.Clicked)
            {
                _game.gameState.AddTower(new RocketLauncher(rocketLauncherTexture) { Bullet = new Brokenshot(_content.Load<Texture2D>("Tower/bullet")) });
                buyRocketLauncherButton.Clicked = false;
            }
            else if (buyDoubleCannonButton.Clicked)
            {
                _game.gameState.AddTower(new DoubleCannon(doubleCannonTexture) { Bullet = new Brokenshot(_content.Load<Texture2D>("Tower/bullet")) });
                buyDoubleCannonButton.Clicked = false;
            }
            else if (buyMachineGunButton.Clicked)
            {
                _game.gameState.AddTower(new MachineGun(machineGunTexture) { Bullet = new Brokenshot(_content.Load<Texture2D>("Tower/bullet")) });
                buyMachineGunButton.Clicked = false;
            }

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
            
            spriteBatch.Draw(bkg, new Vector2(140, 50), Color.Gray);

            string deployable = "Deployable";
            string gold = "Pirce";
            string buy = "Buy";
            spriteBatch.DrawString(textFont, deployable, new Vector2(270, 80), Color.White);
            spriteBatch.DrawString(textFont, gold, new Vector2(700, 80), Color.White);
            spriteBatch.DrawString(textFont, buy, new Vector2(950, 80), Color.White);

            string cannon = "Cannon";
            string cannonText = "This is a very good tower";
            string price1 = "10";
            spriteBatch.DrawString(textFont, cannon, new Vector2(270, 175), Color.White);
            spriteBatch.DrawString(textFont, cannonText, new Vector2(270, 190), Color.White);
            spriteBatch.DrawString(textFont, price1, new Vector2(720, 190), Color.Yellow);

            string machineGun = "Machine Gun";
            string machineGunText = "This is a very good tower";
            string price5 = "20";
            spriteBatch.DrawString(textFont, machineGun, new Vector2(270, 295), Color.White);
            spriteBatch.DrawString(textFont, machineGunText, new Vector2(270, 310), Color.White);
            spriteBatch.DrawString(textFont, price5, new Vector2(720, 310), Color.Yellow);

            string doubleCannon = "Double Cannon";
            string doubleCannonText = "This is a very good tower";
            string price4 = "25";
            spriteBatch.DrawString(textFont, doubleCannon, new Vector2(270, 415), Color.White);
            spriteBatch.DrawString(textFont, doubleCannonText, new Vector2(270, 430), Color.White);
            spriteBatch.DrawString(textFont, price4, new Vector2(720, 430), Color.Yellow);

            string rocketLauncher = "Rocket Launcher";
            string rocketLauncherText = "This is a very good tower";
            string price3 = "30";
            spriteBatch.DrawString(textFont, rocketLauncher, new Vector2(270, 530), Color.White);
            spriteBatch.DrawString(textFont, rocketLauncherText, new Vector2(270, 550), Color.White);
            spriteBatch.DrawString(textFont, price3, new Vector2(720, 550), Color.Yellow);

            string brokenGun = "Broken Gun";
            string brokenGunText = "This is a very good tower";
            string price2 = "45";
            spriteBatch.DrawString(textFont, brokenGun, new Vector2(270, 650), Color.White);
            spriteBatch.DrawString(textFont, brokenGunText, new Vector2(270, 670), Color.White);
            spriteBatch.DrawString(textFont, price2, new Vector2(720, 670), Color.Yellow);

            




            foreach (var button in _button)
                button.Draw(gameTime, spriteBatch);

            foreach (var tower in basicTowers)
            {
                tower.Draw(spriteBatch);
            }
        }
    }
}