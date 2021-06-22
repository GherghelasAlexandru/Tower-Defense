using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelDefense.Controls;
using PixelDefense.Engine;
using PixelDefense.Gameplay;
namespace PixelDefense.States
{
    public class SettingsState : State
    {
        private List<Button> _button;
        public List<ArrowSelector> arrowSelector = new List<ArrowSelector>();
        PassObject applyOptions;
        private Texture2D Background;

        public SettingsState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
         : base(game, graphicsDevice, content)
        {
            Background = _content.Load<Texture2D>("Controls/Background2");
            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var font = _content.Load<SpriteFont>("Fonts/Font");
            var arrowBtnLeftTexture = _content.Load<Texture2D>("Controls/arrowBtn");
            var arrowBtnRightTexture = _content.Load<Texture2D>("Controls/arrowBtn-Right");
            arrowSelector.Add(new ArrowSelector(550, 320, "Sound", content));
            for (int i = 0; i < 31; i++)
            {
                arrowSelector[arrowSelector.Count - 1].AddOption(new FormOption("" + i, i));
            }
            arrowSelector[arrowSelector.Count - 1].selected = (int)(arrowSelector[arrowSelector.Count - 1].options.Count / 2);

            arrowSelector.Add(new ArrowSelector(550, 380, "Bg Music", content));
            for (int i = 0; i < 30; i++)
            {
                arrowSelector[arrowSelector.Count - 1].AddOption(new FormOption("" + i, i));
            }
            arrowSelector[arrowSelector.Count - 1].selected = (int)(arrowSelector[arrowSelector.Count - 1].options.Count/4);

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
            if (Globals.save.CheckIfFileExists("XML\\Settings.xml"))
            {
                XDocument xml = XDocument.Load(Globals.appDataFilePath + "\\" + "PixelDefense" + "\\" + "XML" + "\\" + "Settings.xml");
                LoadSaveFile(xml);
            }
        }

        public virtual void ApplyOptions(PassObject applyOptions)
        {
            this.applyOptions = applyOptions;
        }

        public virtual FormOption GetOptionValue(string name)
        {
            for(int i = 0; i< arrowSelector.Count; i++)
            {
                if(arrowSelector[i].title == name)
                {
                    return arrowSelector[i].GetCurrentOption();
                }
            }
            return null;
        }

        public virtual void LoadSaveFile(XDocument saveData)
        {
            if(saveData != null)
            {
                List<string> allSettings = new List<string>();
                for (int i = 0; i < arrowSelector.Count; i++)
                {
                    allSettings.Add(arrowSelector[i].title);
                }

                for (int i = 0; i < allSettings.Count; i++)
                {
                    List<XElement> optionList = (from t in saveData.Element("Root").Element("Settings").Descendants("settings")
                                                 where t.Element("name").Value == allSettings[i]
                                                 select t).ToList<XElement>();

                    if (optionList.Count > 0)
                    {
                        for (int j = 0; j < arrowSelector.Count; j++)
                        {
                            if (arrowSelector[j].title == allSettings[i])
                            {
                                arrowSelector[j].selected = Convert.ToInt32(optionList[0].Element("selected").Value);
                            }
                        }
                    }
                }

            }
        }
        public void SaveSettings()
        {
            XDocument xml = new XDocument(new XElement("Root", new XElement("Settings", "")));

            foreach (var arrow in arrowSelector)
                xml.Element("Root").Element("Settings").Add(arrow.Returnxml());

            Globals.save.HandleSaveFormates(xml, "Settings.xml");
            _game.ApplyOptions(applyOptions);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Vector2(0, 0));

            foreach (var button in _button)
                button.Draw(gameTime, spriteBatch);

            foreach (var arrow in arrowSelector)
                arrow.Draw(gameTime, spriteBatch);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
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

            foreach (var arrow in arrowSelector)
                arrow.Update(gameTime);
        }
    }
}
