using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dungeon_Game_MonoGame.Objects.UI;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Dungeon_Game_MonoGame.Handlers;
using System.Net;

namespace Dungeon_Game_MonoGame.GameStates.Menus
{
    public class OptionsMenu : Game
    {
        static Button BackButton;
        static Button ResolutionButton;
        static Button FPSButton;
        static Button FullscreenButton;
        static SpriteBatch _spriteBatch = DungeonGame._spriteBatch;
        static ContentManager Content = DungeonGame._content;
        static GraphicsDeviceManager _graphics = DungeonGame._graphics;
        //TITLE GRAPHICS
        static Texture2D Title;
        static Texture2D Fullscreen;
        static Texture2D Resolution;
        static Texture2D Enabled;
        static Texture2D Disabled;
        static Texture2D MaxFPS;
        //RESOLUTION GRAPHICS
        static Texture2D Res1080p;
        static Texture2D Res720p;
        static Texture2D Res4k;
        //FPS GRAPHICS
        static Texture2D FPS60;
        static Texture2D FPS144;
        
        static List<Vector2> AvailableResolutions = new List<Vector2>();
        public static void LoadContent()
        {
            CheckResolutions();
            BackButton = new Button(Content.Load<Texture2D>("assets/mainMenu/back"), new Color(255, 172, 0, 256), new Vector2(1920F / 2, 50 + (1080F * 3 / 4)), new Vector2(0.4f));
            FullscreenButton = new Button(null, new Color(255, 172, 0, 256), new Vector2(1920 * 5/8, 245), new Vector2(0.4f));
            ResolutionButton = new Button(null, new Color(255, 172, 0, 256), new Vector2(1920 * 5 / 8, 315), new Vector2(0.4f));
            FPSButton = new Button(null, new Color(255, 172, 0, 256), new Vector2(1920 * 5 / 8, 385), new Vector2(0.4f));
            FPS60 = Content.Load<Texture2D>("assets/mainMenu/Options/60");
            FPS144 = Content.Load<Texture2D>("assets/mainMenu/Options/144hz");
            Title = Content.Load<Texture2D>("assets/mainMenu/options");
            Fullscreen = Content.Load<Texture2D>("assets/mainMenu/Options/fullscreen");
            Resolution = Content.Load<Texture2D>("assets/mainMenu/Options/resolution");
            MaxFPS = Content.Load<Texture2D>("assets/mainMenu/Options/maximumfps");
            Enabled = Content.Load<Texture2D>("assets/mainMenu/enabled");
            Disabled = Content.Load<Texture2D>("assets/mainMenu/disabled");
            Res1080p = Content.Load<Texture2D>("assets/mainMenu/Options/1080p");
            Res720p = Content.Load<Texture2D>("assets/mainMenu/Options/720p");
            Res4k = Content.Load<Texture2D>("assets/mainMenu/Options/4k");
            if (_graphics.IsFullScreen)
            {
                FullscreenButton._texture = Enabled;
            }
            else
            {
                FullscreenButton._texture = Disabled;
            }
            if (_graphics.PreferredBackBufferHeight == 1080)
            {
                ResolutionButton._texture = Res1080p;
            }
            else if(_graphics.PreferredBackBufferHeight == 720)
            {
                ResolutionButton._texture = Res720p;
            }
            else if(_graphics.PreferredBackBufferHeight == 2160)
            {
                ResolutionButton._texture = Res4k;
            }
            else
            {
                ResolutionButton._texture = Res1080p;
            }

            if (DungeonGame.VSyncRate == 60)
            {
                FPSButton._texture = FPS60;
            }
            else
            {
                FPSButton._texture = FPS144;
            }
        }
        public static void Update()
        {
            BackButton.Update();
            FullscreenButton.Update();
            if(AvailableResolutions.Count != 1)
            {
                ResolutionButton.Update();
            }
            FPSButton.Update();
            if (BackButton.isClicked)
            {
                if(PauseMenu.currentMenu == "Options")
                {
                    PauseMenu.currentMenu = null;                   
                }
                else
                {
                    MainMenu.LoadContent();
                    GameStateHandler.hasSwitchedState = true;
                    GameStateHandler.currentState = "Main Menu";
                }
                BackButton.isClicked= false;
            }
            if (FullscreenButton.isClicked)
            {
                if (_graphics.IsFullScreen)
                {
                    FullscreenButton._texture = Disabled;
                    _graphics.IsFullScreen = false;
                }
                else
                {
                    FullscreenButton._texture = Enabled;
                    _graphics.IsFullScreen = true;
                    
                }
                _graphics.ApplyChanges();
            
            }
            if (ResolutionButton.isClicked)
            {
                if(AvailableResolutions.Count!=1)
                {
                    for(int i = 0; i <= AvailableResolutions.Count; i++)
                    {
                        if (AvailableResolutions[i].Y == _graphics.PreferredBackBufferHeight)
                        {
                            if(i == AvailableResolutions.Count - 1)
                            {
                                _graphics.PreferredBackBufferWidth = (int)AvailableResolutions[0].X;
                                _graphics.PreferredBackBufferHeight = (int)AvailableResolutions[0].Y;

                            }
                            else
                            {
                                _graphics.PreferredBackBufferWidth = (int)AvailableResolutions[i + 1].X;
                                _graphics.PreferredBackBufferHeight = (int)AvailableResolutions[i + 1].Y;
                            }
                            i = AvailableResolutions.Count;
                            _graphics.ApplyChanges();
                        }
                        
                    }
                    
                }
                ResolutionButton.isClicked = false;
                
            }
            if(_graphics.PreferredBackBufferHeight == 2160)
            {
                ResolutionButton._texture = Res4k;
            }
            else if (_graphics.PreferredBackBufferHeight == 1080)
            {
                ResolutionButton._texture = Res1080p;
            }
            else
            {
                ResolutionButton._texture = Res720p;
            }
            
            if (FPSButton.isClicked)
            {
                if(DungeonGame.VSyncRate == 60)
                {
                    DungeonGame.VSyncRate = 144;
                    FPSButton._texture = FPS144;

                }
                else
                {
                    DungeonGame.VSyncRate = 60;
                    FPSButton._texture = FPS60;
                }
                FPSButton.isClicked = false;
            }

        }
        static void CheckResolutions()
        {
            float maxHor = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            float maxVer = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            if (maxHor >= 2160 && maxVer >= 3840)
            {
                AvailableResolutions.Add(new Vector2(3840, 2160));
            }
            if(maxHor >= 1080)
            {
                AvailableResolutions.Add(new Vector2(1920, 1080));
            }
            if (maxHor >= 720)
            {
                AvailableResolutions.Add(new Vector2(1280, 720));
            }
        }
        public static void Draw()
        {
            BackButton.Draw();
            FullscreenButton.Draw();
            ResolutionButton.Draw();
            FPSButton.Draw();
            _spriteBatch.Draw(texture: Title, position: new Vector2(1920 / 2, 150), null, Color.White, 0f, origin: new Vector2(Title.Width / 2, Title.Height / 2), 0.6f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(texture: Fullscreen, position: new Vector2(1920  / 4, 245), null, Color.White, 0f, origin: new Vector2(0,Fullscreen.Height/2), 0.5f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(texture: Resolution, position: new Vector2(1920 /4, 315), null, Color.White, 0f, origin: new Vector2(0,Resolution.Height/2), 0.5f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(texture: MaxFPS, position: new Vector2(1920 / 4, 385), null, Color.White, 0f, origin: new Vector2(0, Resolution.Height / 2), 0.5f, SpriteEffects.None, 0f);

        }
    }
}
