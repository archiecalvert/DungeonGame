using System;
using Dungeon_Game_MonoGame.GameStates;
using Dungeon_Game_MonoGame.Handlers;
using Dungeon_Game_MonoGame.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Dungeon_Game_MonoGame
{
    public class DungeonGame : Game
    {
        //GAME HANDLERS AND BACKBONE VARIABLES
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static ContentManager _content;
        public static GameTime _gameTime;
        public static ObjectHandler _objectHandler;
        public static CoordinateHandler CoordHandler;
        public static CameraHandler Camera = new CameraHandler();
        //GAME WINDOW AND USER INTERFACE

        public static float HorizontalRes = 1920;
        public static float VerticalRes = 1080;
        
        public static float GameScale = settings.GameScale - 0.5f;
        public static Color WindowColor = settings.WindowColour;
        public static float VSyncRate = 144; //sets the maximum framerate
        RenderTarget2D renderTarget; //used to scale the window depending on the resolution
        public static float scale = 10; //the amount the window has been scaled
        //GAME VARIABLES
        public float TimePassed;

        public static CollisionHandler CollisionSystem = new CollisionHandler();
        public DungeonGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferMultiSampling = false;
            _graphics.SynchronizeWithVerticalRetrace = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            TargetElapsedTime = TimeSpan.FromSeconds(1.0f / VSyncRate); //Sets the Max FPS
            IsFixedTimeStep = true;
            base.Initialize();
            _content = Content;
            _graphics.PreferredBackBufferWidth = (int)HorizontalRes; //Sets the window width
            _graphics.PreferredBackBufferHeight = (int)VerticalRes; //Sets the window height
            _graphics.IsFullScreen = false; //Makes the window fullscreen
             //Target Resolution Height
            _graphics.ApplyChanges();
            Window.Title = "Dungeon Game";
        }
        public void ChangeFPS()
        {
            TargetElapsedTime = TimeSpan.FromSeconds(1f / VSyncRate);
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _objectHandler = new ObjectHandler();
            CoordHandler = new CoordinateHandler();
            
            GameStateHandler.currentState = "Main Menu"; //sets the current game state
            GameStateHandler.Paused = true; //makes the game paused
            renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, 1920, 1080); //sets the games resolution
        }

        protected override void Update(GameTime gameTime)
        {
            TargetElapsedTime = TimeSpan.FromSeconds(1.0f / VSyncRate);
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
             //   Exit();
            _gameTime = gameTime;
            base.Update(gameTime);
            InputHandler.Update();
            GameStateHandler.Update();
            CollisionSystem.Update();
            _objectHandler.Update();

        }

        protected override void Draw(GameTime gameTime)
        {
            scale = 1F / (1080F / _graphics.GraphicsDevice.Viewport.Height); //calculates how much the window needs to be scaled in order to fit the content on screen
            GraphicsDevice.SetRenderTarget(renderTarget); //used for scaling
            GraphicsDevice.Clear(WindowColor);
            base.Draw(gameTime);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null); //used to make sure that textures can be upscaled without being blurry
            GameStateHandler.Draw();     
            
            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(WindowColor);
            _spriteBatch.Begin();
            _spriteBatch.Draw(renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f); //draws the scaled image to the screen
            _spriteBatch.End();
            
        }

    }
}