using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Dungeon_Game_MonoGame.Objects.UI;
using Microsoft.Xna.Framework.Content;
using Dungeon_Game_MonoGame.Handlers;

namespace Dungeon_Game_MonoGame.GameStates.Menus
{
    public class PauseMenu
    {
        public static Texture2D DimTexture = new Texture2D(DungeonGame._graphics.GraphicsDevice, 1, 1);
        
        static Button MainMenuButton;
        static Button OptionsButton;
        static Texture2D PausedTexture;
        static ContentManager Content = DungeonGame._content;
        public static string currentMenu = null;
        public static void LoadContent()
        {
            DimTexture.SetData(new[] { Color.White });
            MainMenuButton = new Button(texture: Content.Load<Texture2D>("assets/mainMenu/Paused/mainmenu"), new Color(255, 172, 0, 256), new Vector2(1920 * 4 / 8, 900), new Vector2(.55f));
            PausedTexture = Content.Load<Texture2D>("assets/mainMenu/Paused/paused");
            OptionsButton = new Button(texture: Content.Load<Texture2D>("assets/mainMenu/Options"), new Color(255, 172, 0, 256), new Vector2(1920 / 2, 325), new Vector2(.5f));
        }
        public static void Update()
        {
            
            if (MainMenuButton.isClicked)
            {
                MainMenu.LoadContent();
                GameStateHandler.hasSwitchedState= true;
                GameStateHandler.currentState = "Main Menu";
                MainGame.UnloadContent();
                MainMenuButton.isClicked = false;
                
            }
            if (OptionsButton.isClicked)
            {
                currentMenu = "Options";
                OptionsMenu.LoadContent();
                OptionsButton.isClicked = false;
                
            }
            if (currentMenu == null)
            {
                MainMenuButton.Update();
                OptionsButton.Update();
            }
            else if (currentMenu == "Options")
            {
                OptionsMenu.Update();
            }
        }
        
        public static void Draw()
        {
            
            if(currentMenu == null) 
            {
                DungeonGame._spriteBatch.Draw(texture: DimTexture, new Rectangle(0, 0, 1920, 1080), new Color(0, 0, 0, 100));
                DungeonGame._spriteBatch.Draw(texture: PausedTexture, position: new Vector2(1920 / 2, 200), null, Color.White, 0f, origin: new Vector2(PausedTexture.Width / 2, PausedTexture.Height / 2), 0.6f, SpriteEffects.None, 0f);
                MainMenuButton.Draw();
                OptionsButton.Draw();
            }
            else if (currentMenu == "Options")
            {
                DungeonGame._spriteBatch.Draw(texture: DimTexture, new Rectangle(0, 0, 1920, 1080), new Color(48,48,48,256));
                OptionsMenu.Draw();
            }
            
        }
    }
}
