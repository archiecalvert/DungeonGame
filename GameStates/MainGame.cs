using System;
using Dungeon_Game_MonoGame.Abilities;
using Dungeon_Game_MonoGame.Handlers;
using Dungeon_Game_MonoGame.Objects.game;
using Dungeon_Game_MonoGame.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Game_MonoGame.GameStates
{
    public class MainGame
    {
        static SpriteBatch _spriteBatch = DungeonGame._spriteBatch;
        static ContentManager Content = DungeonGame._content;
        static GraphicsDeviceManager _graphics = DungeonGame._graphics;
        static Player character;
        static TilesetHandler groundTiles; //creates the tilemap for the ground
        static TilesetHandler wallTilesFront; //creates the tilemap for the walls drawn above the player
        static TilesetHandler wallTilesBack; //creates the tilemap for the walls drawn behind the player
        static TilesetHandler topTilesFront; //creates the tilemap for the tops of the walls drawn above the player
        static TilesetHandler topTilesBack; //creates the tilemap for the tops of the walls drawn behind the player
        static Texture2D dashText, healthText;
        static SpriteFont debugFont;
        static AnimatedObject fire;
        public static Hitbox fireHitBox;
        public static Hitbox PlayerHitBox;
        public static Texture2D RectangleTexture = new Texture2D(DungeonGame._graphics.GraphicsDevice, 1, 1);
        static Hitbox PlaceHolder;
        public static void LoadMap()
        {
            groundTiles = new TilesetHandler("assets/data/map/ground.txt", 1); //creates the tilemap for the ground
            wallTilesFront = new TilesetHandler("assets/data/map/walls-front.txt", 1); //creates the tilemap for the walls drawn above the player
            wallTilesBack = new TilesetHandler("assets/data/map/walls-back.txt", 1); //creates the tilemap for the walls drawn behind the player
            topTilesFront = new TilesetHandler("assets/data/map/tops-front.txt", 1); //creates the tilemap for the tops of the walls drawn above the player
            topTilesBack = new TilesetHandler("assets/data/map/tops-back.txt", 1); //creates the tilemap for the tops of the walls drawn behind the player
            groundTiles.LoadContent();
            wallTilesFront.LoadContent();
            topTilesFront.LoadContent();
            wallTilesBack.LoadContent();
            topTilesBack.LoadContent();
        }
        public static void LoadUI()
        {
            RectangleTexture.SetData(new[] { Color.White });
            dashText = Content.Load<Texture2D>("assets/UI/dash");
            healthText = Content.Load<Texture2D>("assets/UI/health");
        }
        public static void LoadCharacterAssets()
        {
            character = new Player(position: new(1920 / 2, 1080 / 2)); //creates a player object
            DungeonGame.CoordHandler.CoordinateDict.Add("Player", DungeonGame.CoordHandler.PlayerCoords * settings.GameScale); //adds the player to the coordinate dictionary
            DungeonGame._objectHandler.AddObject(character);
            PlayerHitBox = new Hitbox(new Vector2(47, 57), DungeonGame.CoordHandler.PlayerCoords * settings.GameScale, true); //creates the players hitbox
            PlayerHitBox.AttachToObject("Player"); //attaches the players hitbox to the player
            PlayerHitBox.LoadContent();
            character.LoadContent();
        }
        public static void LoadContent()
        {        
            DungeonGame.CoordHandler.CoordinateDict.Add("Fireball", DungeonGame.CoordHandler.CoordsToScreenPos(new Vector2(4, 0))); //adds the fireball to the coordinate dictionary
            fire = new AnimatedObject(Content.Load<Texture2D>("assets/projectiles/fireball/fireball"), 16, 24, new Vector2(4,0), 200, 4); //creates the test fire object at spawn
            fireHitBox = new Hitbox(new Vector2(16, 24) * 4, new Vector2(4,0), false); //creates a hitbox for the fire at spawn
            DungeonGame.CoordHandler.CoordinateDict["Fireball"] = DungeonGame.CoordHandler.SetCoordinates(new Vector2(16,24));
            fireHitBox.AttachToObject("Fireball");
            fire.LoadContent();
            debugFont = Content.Load<SpriteFont>("assets/fonts/debugfont");
            fireHitBox.LoadContent();
            LoadCharacterAssets();
            LoadMap();
            LoadUI();
        }
        public static void UnloadContent()
        {
            character = null;
            DungeonGame.CoordHandler.CoordinateDict.Clear();
            fire = null;
            fireHitBox = null;
            groundTiles = null;
            wallTilesFront = null;
            topTilesFront = null;
            wallTilesBack= null;
            topTilesBack= null;
            debugFont= null;
            PlayerHitBox = null;
            DungeonGame._objectHandler.gameObjects.Clear();
            DungeonGame._objectHandler.magicList.Clear();
            Dash.cooldownDuration = 50;

        }
        public static void Update()
        {
            if (!GameStateHandler.Paused)
            {
                character.Update();
                Dash.Update();
                DungeonGame.CoordHandler.CoordinateDict["Player"] = DungeonGame.CoordHandler.PlayerCoords;
                DungeonGame.CoordHandler.CoordinateDict["Fireball"] = DungeonGame.CoordHandler.SetCoordinates(new Vector2(4, 0));
            }
        }
        public static void Draw()
        {
            groundTiles.DrawTilemap();
            wallTilesBack.DrawTilemap();
            character.Draw(); 
            wallTilesFront.DrawTilemap();
            topTilesFront.DrawTilemap();
            topTilesBack.DrawTilemap();
            fire.Draw();
            if (settings.Debugger) //draws the debug text if enabled
            {
                fireHitBox.Draw();
                PlayerHitBox.Draw();
                fireHitBox.Draw();
                _spriteBatch.DrawString(debugFont, "--------DEBUGGER--------", new Vector2(15, 15), Color.White);
                _spriteBatch.DrawString(debugFont, "X Coordinate:" + -DungeonGame.CoordHandler.PlayerCoords.X, new Vector2(15, 45), Color.White);
                _spriteBatch.DrawString(debugFont, "Y Coordinate:" + DungeonGame.CoordHandler.PlayerCoords.Y, new Vector2(15, 75), Color.White);
                _spriteBatch.DrawString(debugFont, "No. Of Objects: " + DungeonGame._objectHandler.magicList.Count, new Vector2(15, 105), Color.White);
                _spriteBatch.DrawString(debugFont, "Camera Attached to Player?: " + DungeonGame.Camera.isAttached, new Vector2(15, 135), Color.White);
                _spriteBatch.DrawString(debugFont, "FPS: " + (int)(1 /DungeonGame._gameTime.ElapsedGameTime.TotalSeconds) , new Vector2(15, 165), Color.White);
                _spriteBatch.DrawString(debugFont, "Colliding: " + DungeonGame.CollisionSystem.isD, new Vector2(15, 195), Color.White);
            }
        }
        public static void DrawUI()
        {
            //DRAWN IN THE GAMESTATEHANDLER CLASS SO IT RENDERS ABOVE ALL ELSE
            _spriteBatch.Draw(texture: dashText, position: new Vector2(1920 / 4, 1080 - 85), null, Color.White, 0f, origin: new Vector2(dashText.Width, dashText.Height / 2), 0.3f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(texture: healthText, position: new Vector2(1920 * 3/ 4 - 250, 1080 - 85), null, Color.White, 0f, origin: new Vector2(healthText.Width, healthText.Height / 2), 0.3f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(texture: RectangleTexture, new Rectangle(1920 / 4 + 25, 1000 - (int)(dashText.Height * 0.3f/2), 250, (int)(dashText.Height * 0.3f) - 10), Color.Black);
            _spriteBatch.Draw(texture: RectangleTexture, new Rectangle(1920 / 4 + 30, 1000 - (int)(dashText.Height * 0.3f/2) + 5, (int)(240 * (Dash.cooldownDuration/50)), (int)(dashText.Height * 0.3f) - 20), new Color(77, 189, 207, 256));
            _spriteBatch.Draw(texture: RectangleTexture, new Rectangle(1920 * 3/ 4 - 235, 1000 - (int)(dashText.Height * 0.3f / 2), 250, (int)(dashText.Height * 0.3f) - 10), Color.Black);
            _spriteBatch.Draw(texture: RectangleTexture, new Rectangle(1920 * 3/ 4 - 230, 1000 - (int)(dashText.Height * 0.3f / 2) + 5, 240, (int)(dashText.Height * 0.3f) - 20), new Color(205, 0, 3, 256));
        }
    }
    
}
