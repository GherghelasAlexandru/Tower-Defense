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

            this.cannonTexture = content.Load<Texture2D>("Tower/T1");
            this.rocketLauncherTexture = content.Load<Texture2D>("Tower/T5");
            this.doubleCannonTexture = content.Load<Texture2D>("Tower/T4");
            this.machineGunTexture = content.Load<Texture2D>("Tower/T3");
            this.brokenGunTexture = content.Load<Texture2D>("Tower/T2");

            this.buyCannonButton = new Button(buttonTexture4, buttonFont)
            {
                Position = new Vector2(930, 160),
            };

            this.buyMachineGunButton = new Button(buttonTexture4, buttonFont)
            {
                Position = new Vector2(930, 280),
            };

            this.buyDoubleCannonButton = new Button(buttonTexture4, buttonFont)
            {
                Position = new Vector2(930, 400),
            };

            this.buyRocketLauncherButton = new Button(buttonTexture4, buttonFont)
            {
                Position = new Vector2(930, 520),
            };

            this.buyBrokenGunButton = new Button(buttonTexture4, buttonFont)
            {
                Position = new Vector2(930, 640),
            };

            this.basicTowers = new List<Sprite>();

            this.cannonTower = new Cannon(cannonTexture) {Bullet = new Grapeshot(_content.Load<Texture2D>("Tower/bullet")) , _position = new Vector2(190, 160) };

            this.machineGunTower = new MachineGun(machineGunTexture) { Bullet = new The556(_content.Load<Texture2D>("Tower/MG")), _position = new Vector2(190, 280) };

            this.doubleCannonTower = new DoubleCannon(doubleCannonTexture) { Bullet = new Grapeshot(_content.Load<Texture2D>("Tower/GrapeShot")), _position = new Vector2(190, 400)  };

            this.rocketLauncherTower = new RocketLauncher(rocketLauncherTexture) { Bullet = new Rocket(_content.Load<Texture2D>("Tower/Rocket")), _position = new Vector2(190, 520) };

            this.brokenGunTower = new BrokenGun(brokenGunTexture) { Bullet = new Brokenshot(_content.Load<Texture2D>("Tower/BrokenShot")), _position = new Vector2(190, 640) };

            AddBasicTower(cannonTower);
            AddBasicTower(brokenGunTower);
            AddBasicTower(rocketLauncherTower);
            AddBasicTower(doubleCannonTower);
            AddBasicTower(machineGunTower);

            this.bkg = content.Load<Texture2D>("Controls/bkg");

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
                    Globals.soundControl.PlaySound("negative");
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
                    _game.gameState.SetGold(-brokenGunTower.GetPrice());
                }
                else
                {
                    Globals.soundControl.PlaySound("negative");
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
                    _game.gameState.SetGold(-rocketLauncherTower.GetPrice());
                }
                else
                {
                    Globals.soundControl.PlaySound("negative");
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
                    _game.gameState.SetGold(-doubleCannonTower.GetPrice());
                }
                else
                {
                    Globals.soundControl.PlaySound("negative");
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
                    _game.gameState.SetGold(-machineGunTower.GetPrice());
                }
                else 
                {
                    Globals.soundControl.PlaySound("negative");
                    notEnoughtMoney = true;
                }      
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // to be modified to change back to the gameState
            Globals.soundControl.PlaySound("click");
            _game.ChangeState(_game.gameState);
            notEnoughtMoney = false;
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
            string cannonText = "* A bit rusty but trusty";
            string cannonDescription = "FIRE RATE 3   |   DMG 1   |   RANGE 300 ";
            string price1 = "50";
            spriteBatch.DrawString(textFont, cannon, new Vector2(270, 170), Color.LightGray);
            spriteBatch.DrawString(textFont, cannonText, new Vector2(270, 190), Color.White);
            spriteBatch.DrawString(textFont, cannonDescription, new Vector2(270, 210), Color.LightSkyBlue);
            spriteBatch.DrawString(textFont, price1, new Vector2(720, 190), Color.Yellow);

            string machineGun = "Machine Gun";
            string machineGunText = "* Nothing can go wrong with brrrrrr";
            string machineGunDescription = "FIRE RATE 2   |   DMG 1   |   RANGE 250 ";
            string price5 = "100";
            spriteBatch.DrawString(textFont, machineGun, new Vector2(270, 290), Color.LightGray);
            spriteBatch.DrawString(textFont, machineGunText, new Vector2(270, 310), Color.White);
            spriteBatch.DrawString(textFont, machineGunDescription, new Vector2(270, 330), Color.LightSkyBlue);
            spriteBatch.DrawString(textFont, price5, new Vector2(720, 310), Color.Yellow);

            string doubleCannon = "Double Cannon";
            string doubleCannonText = "* Double rusty double trusty";
            string doubleCannonDescription = "FIRE RATE 3   |   DMG 2   |   RANGE 300 ";
            string price4 = "150";
            spriteBatch.DrawString(textFont, doubleCannon, new Vector2(270, 410), Color.LightGray);
            spriteBatch.DrawString(textFont, doubleCannonText, new Vector2(270, 430), Color.White);
            spriteBatch.DrawString(textFont, doubleCannonDescription, new Vector2(270, 450), Color.LightSkyBlue);
            spriteBatch.DrawString(textFont, price4, new Vector2(720, 430), Color.Yellow);

            string rocketLauncher = "Rocket Launcher";
            string rocketLauncherText = "* Stolen technology from aliens ";
            string rocketLauncherDescription = "FIRE RATE 4   |   DMG 3   |   RANGE 400 ";
            string price3 = "300";
            spriteBatch.DrawString(textFont, rocketLauncher, new Vector2(270, 530), Color.LightGray);
            spriteBatch.DrawString(textFont, rocketLauncherText, new Vector2(270, 550), Color.White);
            spriteBatch.DrawString(textFont, rocketLauncherDescription, new Vector2(270, 570), Color.LightSkyBlue);
            spriteBatch.DrawString(textFont, price3, new Vector2(720, 550), Color.Yellow);

            string brokenGun = "Broken Gun";
            string brokenGunText = "* This is crazy bruh";
            string brokenGunDescription = "FIRE RATE 1.5   |   DMG 1   |   RANGE 250 ";
            string price2 = "450";
            spriteBatch.DrawString(textFont, brokenGun, new Vector2(270, 650), Color.LightGray);
            spriteBatch.DrawString(textFont, brokenGunText, new Vector2(270, 670), Color.White);
            spriteBatch.DrawString(textFont, brokenGunDescription, new Vector2(270, 690), Color.LightSkyBlue);
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