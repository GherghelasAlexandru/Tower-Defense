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

        public bool notEnoughtMoney = false;

        public readonly List<Sprite> basicTowers;
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

            cannonTower = new Cannon(cannonTexture) {Bullet = new Grapeshot(_content.Load<Texture2D>("Tower/bullet")) , _position = new Vector2(190, 160) };

            machineGunTower = new MachineGun(machineGunTexture) { Bullet = new Grapeshot(_content.Load<Texture2D>("Tower/bullet")), _position = new Vector2(190, 280) };

            doubleCannonTower = new DoubleCannon(doubleCannonTexture) { Bullet = new Grapeshot(_content.Load<Texture2D>("Tower/bullet")), _position = new Vector2(190, 400)  };

            rocketLauncherTower = new RocketLauncher(rocketLauncherTexture) { Bullet = new Grapeshot(_content.Load<Texture2D>("Tower/bullet")), _position = new Vector2(190, 520) };

            brokenGunTower = new BrokenGun(brokenGunTexture) { Bullet = new Grapeshot(_content.Load<Texture2D>("Tower/bullet")), _position = new Vector2(190, 640) };

            AddBasicTower(cannonTower);
            AddBasicTower(brokenGunTower);
            AddBasicTower(rocketLauncherTower);
            AddBasicTower(doubleCannonTower);
            AddBasicTower(machineGunTower);
          
            bkg = content.Load<Texture2D>("Controls/bkg");

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
            
            if (buyCannonButton.Clicked)
            {
                if(cannonTower.GetPrice() <= _game.gameState.GetGold())
                {
                    _game.ChangeState(_game.gameState);
                    _game.gameState.AddTower(cannonTower.Clone()  as Cannon );
                    buyCannonButton.Clicked = false;
                    _game.gameState.SetGold(-cannonTower.GetPrice());
                }
                else
                {
                    notEnoughtMoney = true;
                }
            }
            else if(buyBrokenGunButton.Clicked)
            {
                if (brokenGunTower.GetPrice() <= _game.gameState.GetGold())
                {
                    _game.ChangeState(_game.gameState);
                    _game.gameState.AddTower(brokenGunTower.Clone() as BrokenGun);
                    buyBrokenGunButton.Clicked = false;
                    _game.gameState.SetGold(-45);
                }
                else
                {
                    notEnoughtMoney = true;
                }
            }
            if(buyRocketLauncherButton.Clicked)
            {
                if (rocketLauncherTower.GetPrice() <= _game.gameState.GetGold()) 
                {
                    _game.ChangeState(_game.gameState);
                    _game.gameState.AddTower(rocketLauncherTower.Clone() as RocketLauncher);
                    buyRocketLauncherButton.Clicked = false;
                    _game.gameState.SetGold(-30);
                }
                else
                {
                    notEnoughtMoney = true;
                }
            }
            else if (buyDoubleCannonButton.Clicked)
            {
                if (doubleCannonTower.GetPrice() <= _game.gameState.GetGold())
                {
                    _game.ChangeState(_game.gameState);
                    _game.gameState.AddTower(doubleCannonTower.Clone() as DoubleCannon);
                    buyDoubleCannonButton.Clicked = false;
                    _game.gameState.SetGold(-25);
                }
                else
                {
                    notEnoughtMoney = true;
                }    
            }
            else if (buyMachineGunButton.Clicked)
            {
                if (machineGunTower.GetPrice() <= _game.gameState.GetGold())
                {
                    _game.ChangeState(_game.gameState);
                    _game.gameState.AddTower(machineGunTower.Clone() as MachineGun);
                    buyMachineGunButton.Clicked = false;
                    _game.gameState.SetGold(-20);
                }
                else 
                {
                    notEnoughtMoney = true;
                }      
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // to be modified to change back to the gameState
            
            _game.ChangeState(_game.gameState);
            notEnoughtMoney = false;
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
            if(notEnoughtMoney)
            {
                spriteBatch.DrawString(textFont, "Not enought gold!! Go back & play some more..", new Vector2(150, 10), Color.Black);
            }

            spriteBatch.Draw(bkg, new Vector2(140, 50), Color.Gray);
            
            spriteBatch.DrawString(textFont, "Gold = " + _game.gameState.GetGold(), new Vector2(10, 10), Color.Black);

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