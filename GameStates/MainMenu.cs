using Dungeon_Game_MonoGame.GameStates.Menus;
using Dungeon_Game_MonoGame.Handlers;
using Dungeon_Game_MonoGame.Objects.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Game_MonoGame.GameStates
{
    public class MainMenu
    {
        static Button StartButton;
        static Button OptionsButton;
        static Button ExitButton;
        static Texture2D logo;
        static SpriteBatch _spriteBatch = DungeonGame._spriteBatch;
        static ContentManager Content = DungeonGame._content;
        static GraphicsDeviceManager _graphics = DungeonGame._graphics;
        public static void LoadContent()
        {
            StartButton = new Button(Content.Load<Texture2D>("assets/mainMenu/start"), new Color(255, 172, 0, 256), new Vector2(1920F / 2, (1080F * 3 / 4) - 60), new Vector2(0.4f)); //creates the start button
            OptionsButton = new Button(Content.Load<Texture2D>("assets/mainMenu/options"), new Color(255, 172, 0, 256), new Vector2(1920F/2, 10 + (1080F * 3/4)), new Vector2(0.4f));
            ExitButton = new Button(Content.Load<Texture2D>("assets/mainMenu/exit"), new Color(255, 172, 0, 256), new Vector2(1920 / 2, 80 + (1080F * 3 / 4)), new Vector2(0.4f));
            logo = Content.Load<Texture2D>("assets/mainMenu/logo"); //loads the logo into memory
        }
        public static void Update()
        {
            StartButton.Update();
            OptionsButton.Update();
            ExitButton.Update();
            if (StartButton.isClicked) //checks to see whether the start button has been clicked
            {
                MainGame.LoadContent();
                PauseMenu.LoadContent();
                GameStateHandler.hasSwitchedState = true;
                GameStateHandler.currentState = "Game";
                GameStateHandler.Paused= false;
            }
            if (OptionsButton.isClicked)
            {
                OptionsMenu.LoadContent();
                GameStateHandler.hasSwitchedState = true;
                GameStateHandler.currentState = "Options";  
            }
            if(ExitButton.isClicked)
            {
                System.Environment.Exit(0);
            }
        }
        public static void Draw()
        {
            StartButton.Draw();
            OptionsButton.Draw();
            ExitButton.Draw();
            _spriteBatch.Draw(texture: logo, position: new Vector2(1920 / 2, 300), null, Color.White, 0f, origin: new Vector2(logo.Width/2, logo.Height/2), 1f, SpriteEffects.None, 0f); //draws the logo on screen
        }
    }

    
}
