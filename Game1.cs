using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TiledSharp;

namespace PixelDefense
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TmxMap map;
        TmxTileset tileSet;
        Texture2D tileTexture;

        bool showCollisionGeometry;
        Texture2D collisionTexture;

        int tileWidth;
        int tileHeight;
        int tilesetTilesWide;
        int tilesetTilesHigh;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            /*graphics.ToggleFullScreen();*/
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            map = new TmxMap("Content/map.tmx");
            tileSet = map.Tilesets[0];
            tileTexture = Content.Load<Texture2D>(tileSet.Name);

            tileWidth = tileSet.TileWidth;
            tileHeight = tileSet.TileHeight;

            tilesetTilesWide = tileTexture.Width / tileWidth;
            tilesetTilesHigh = tileTexture.Height / tileHeight;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            int margin = tileSet.Margin;
            int spacing = tileSet.Spacing;

            spriteBatch.Begin();

            for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
            {
                var tile = map.Layers[0].Tiles[i];
                int gid = tile.Gid;

                // Empty tile, do nothing
                if (gid == 0)
                {

                }
                else
                {
                    int tileFrame = (int)gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(margin + (tileWidth + spacing) * column,
                                                         margin + (tileHeight + spacing) * row,
                                                         tileWidth,
                                                         tileHeight);

                    var effects = SpriteEffects.None;
                    if (tile.HorizontalFlip)
                    {
                        effects = SpriteEffects.FlipHorizontally;
                    }
                    if (tile.VerticalFlip)
                    {
                        effects = SpriteEffects.FlipVertically;
                    }

                    spriteBatch.Draw(tileTexture, new Rectangle((int)x, (int)y, 16, 16), tilesetRec, Color.White,
                                     0.0f, Vector2.Zero, effects, 0.0f);
#if DEBUG
                    if (showCollisionGeometry)
                    {
                        var tileSetLookup = map.Tilesets[0].Tiles;

                        // if the tile is not in the tile set, no collision is possible
                        if (tileFrame > tileSetLookup.Count - 1)
                            continue;


                        var groups = tileSetLookup[tileFrame].ObjectGroups;
                        // assume that the object groups on the tile represent collision geometry
                        if (groups.Count == 0)
                            continue;

                        var collObjects = groups[0];
                        foreach (var obj in collObjects.Objects)
                        {
                            // check if collision boundary is a rectangle
                            // Tiled editor does not set type, so check attr values
                            if (obj.Width > 0 && obj.Height > 0)
                            {
                                int width = (int)Math.Round(obj.Width);
                                int height = (int)Math.Round(obj.Height);
                                int xoffset = (tile.HorizontalFlip) ? map.TileWidth - (int)obj.X - width : (int)obj.X;
                                int yoffset = (tile.VerticalFlip) ? map.TileHeight - (int)obj.Y - height : (int)obj.Y;

                                effects = SpriteEffects.None;

                                // rectangle is in tile coordinates
                                var rect = new Rectangle(tile.X * map.TileWidth + xoffset,
                                                         tile.Y * map.TileHeight + yoffset,
                                                         width,
                                                         height);
                                spriteBatch.Draw(collisionTexture, rect, null, Color.White,
                                                 0.0f, Vector2.Zero, effects, 0.0f);
                            }
                        }
                    }
#endif
                }
            }

           

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
