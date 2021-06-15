using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PixelDefense.Controls
{
    public class ArrowSelector
    {

        public int selected;
        public string title;
        private List<Button> _arrowbutton;
        public int posx;
        public int posy;
        public SpriteFont textFont;
        public ArrowSelector(int posx, int posy, string title, ContentManager content)
        {
            textFont = content.Load<SpriteFont>("Fonts/Font");
            var arrowBtnLeftTexture = content.Load<Texture2D>("Controls/arrowBtn");
            var arrowBtnRightTexture = content.Load<Texture2D>("Controls/arrowBtn-Right");
            this.title = title;
            this.posx = posx;
            this.posy = posy;
            selected = 20;


            var arrowBtnLeft = new Button(arrowBtnLeftTexture, textFont)
            {
                Position = new Vector2(posx, posy),
            };

            arrowBtnLeft.Click += ArrowLeftClick;

            var arrowBtnRight = new Button(arrowBtnRightTexture, textFont)
            {
                Position = new Vector2(posx + 115, posy),
            };

            arrowBtnRight.Click += ArrowRightClick;

            _arrowbutton = new List<Button>()
            {
                arrowBtnLeft,
                arrowBtnRight,
            };

            
        }
        public virtual XElement Returnxml()
        {
            XElement xml = new XElement("settings",
                                        new XElement("name", title),
                                        new XElement("selected", selected));
            return xml;
                                               
        }


        public void Update(GameTime gameTime)
        {
            foreach (var arrow in _arrowbutton)
                arrow.Update(gameTime);
        }
        public virtual void ArrowLeftClick(object sender, EventArgs e)
        {
            selected--;
            if (selected < 0)
            {
                selected = 0;
            }
        }

        public virtual void ArrowRightClick(object sender, EventArgs e)
        {
            selected++;
            if (selected >= 100)
            {
                selected = 100;
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var arrow in _arrowbutton)
                arrow.Draw( gameTime, spriteBatch);

            spriteBatch.DrawString(textFont, selected.ToString(), new Vector2(posx + 65, posy+10), Color.Black);
            spriteBatch.DrawString(textFont, title + ": ", new Vector2(posx - 100, posy+10), Color.Black);
        }

    }
}
