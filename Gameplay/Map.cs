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
        protected TmxMap map;
       //protected TmxTileset tileSet;
        protected Texture2D tileTexture;
        protected int tileWidth;
        protected int tileHeight;
        protected int tilesetTilesWide;
        protected int tilesetTilesHigh;
        public Queue<Vector2> path;
        public List<Rectangle> pathList;

        public object Tilesets { get; internal set; }
        public object Position { get; private set; }

        public Map(ContentManager content, string mapPath)
        {

            pathList = new List<Rectangle>();
            map = new TmxMap(mapPath);
            tileTexture = content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;

            tilesetTilesWide = tileTexture.Width / tileWidth;
            tilesetTilesHigh = tileTexture.Height / tileHeight;

            path = new Queue<Vector2>();


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

        public void AddCollisionPath()
        {

            foreach (var point in map.ObjectGroups["CollisionPath"].Objects)
            {
                pathList.Add(new Rectangle((int)point.X, (int)point.Y, (int)point.Width, (int)point.Height));
            }
        }

        public List<Rectangle> GetCollisionPath()
        {
            return pathList;
        }

        public bool IsCollision(Rectangle target)
        {
            foreach (Rectangle rec in pathList)
                if (rec.Intersects(target))
                {
                    return true;
                }
            return false;
        }

        

        public Queue<Vector2> GetPath()
        {
            int points = Convert.ToInt32(map.ObjectGroups["Objects"].Properties["Points"]);

            Queue<Vector2> path = new Queue<Vector2>();

            for (int i = 1; i <= points; i++)
            {
                path.Enqueue(new Vector2((float)map.ObjectGroups["Objects"].Objects["Point" + i].X, (float)map.ObjectGroups["Objects"].Objects["Point" + i].Y));
            }

            path.Enqueue(new Vector2((float)map.ObjectGroups["Objects"].Objects["End"].X, (float)map.ObjectGroups["Objects"].Objects["End"].Y));

            return path;
        }

        public Vector2 GetStartingPoint()
        {
            return new Vector2((float)map.ObjectGroups["Objects"].Objects["Start"].X, (float)map.ObjectGroups["Objects"].Objects["Start"].Y);
        }


        // get and set methods
        public void SetTmxMap(TmxMap map)
        {
            this.map = map;
        }

        public TmxMap GetMap()
        {
            return this.map;
        }

        public void SetTexture(Texture2D texture)
        {
            this.tileTexture = texture;
        }

        public Texture2D GetTexture()
        {
            return this.tileTexture;
        }

        public void SetTileWidth(int width)
        {
            this.tileWidth = width;
        }

        public int GetTileWidth()
        {
            return this.tileWidth;
        }

        public void SetTileHeight(int height)
        {
            this.tileHeight = height;
        }

        public int GetTileHeight()
        {
            return this.tileHeight;
        }

        public void SetTilesetTilesWidth(int width)
        {
            this.tilesetTilesWide = width;
        }

        public int GetTilesetTilesWidth()
        {
            return this.tilesetTilesWide;
        }

        public void SetTilesetTilesHeight(int height)
        {
            this.tilesetTilesHigh = height;
        }

        public int GetTilesetTilesHeight()
        {
            return this.tilesetTilesHigh;
        }

    }
}
