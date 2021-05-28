using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace PixelDefense.Gameplay
{
    public class Map
    {
        TmxMap map;
        TmxTileset tileSet;
        Texture2D tileTexture;
        int tileWidth;
        int tileHeight;
        int tilesetTilesWide;
        int tilesetTilesHigh;

        private readonly int mapID;
        public object Tilesets { get; internal set; }
        public object Position { get; private set; }

        public Map(ContentManager content, string mapPath,int mapID)
        {
            this.mapID = mapID;
            map = new TmxMap(mapPath);
            tileTexture = content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;

            tilesetTilesWide = tileTexture.Width / tileWidth;
            tilesetTilesHigh = tileTexture.Height / tileHeight;
        }

        public int GetMapID()
        {
            return mapID;
        }
        public void DrawLayer(int index, SpriteBatch batch)
        {

            for (var i = 0; i < map.Layers[index].Tiles.Count; i++)
            {
                
                //Get the identification of the tile
                int gid = map.Layers[index].Tiles[i].Gid;

                // Empty tile, do nothing
                if (gid == 0) { }
                else
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    //Put all the data together in a new rectangle
                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                    //Draw the tile that is within the tilesetRec
                    batch.Draw(tileTexture, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                }
               
            }
        }

        
        public void DrawGrass(SpriteBatch spritebatch)
        {
            DrawLayer(0, spritebatch);
        }

        public void DrawPath(SpriteBatch spritebatch)
        {
            DrawLayer(1, spritebatch);
        }

        public void DrawShadow(SpriteBatch spritebatch)
        {
            DrawLayer(2, spritebatch);
        }

        public void DrawBase(SpriteBatch spritebatch)
        {
            DrawLayer(3, spritebatch);
        }

        public void DrawDecorations(SpriteBatch spritebatch)
        {
            DrawLayer(4, spritebatch);
        }
    }
}
