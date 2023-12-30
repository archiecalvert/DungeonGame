using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Dungeon_Game_MonoGame.GameStates;
using Dungeon_Game_MonoGame.GameStates.Menus;
using System.Runtime.Remoting;

namespace Dungeon_Game_MonoGame.Handlers
{
    public class GameStateHandler
    {
        public static string currentState;
        public static bool Paused;
        static string isMenu = "Main Menu";
        static string isGame = "Game";
        public static bool hasSwitchedState = true;
        public static MenuDelay menuDelay = new MenuDelay();
        public static void Update()
        {
            //MENU BUTTON DELAYS UPDATE
            if(Paused)
            {
                menuDelay.Update();
            }
            //MAIN MENU UPDATES
            if (currentState == isMenu)
            {
                if (hasSwitchedState)
                {
                    MainMenu.LoadContent();
                    hasSwitchedState = false;
                }
                MainMenu.Update();
            }
            //MAIN GAME UPDATE
            else if (currentState == isGame)
            {
                if (hasSwitchedState)
                {
                    Paused = false;
                    hasSwitchedState = false;
                }
                if (!Paused)
                {
                    MainGame.Update();
                }
            }
            //OPTIONS MENU UPDATE
            else if(currentState == "Options")
            {
                if (hasSwitchedState)
                {
                    hasSwitchedState= false;
                }
                OptionsMenu.Update();
            }
            if (currentState == isGame && Paused)
            {
                PauseMenu.Update();
                if(PauseMenu.currentMenu == "Options")
                {
                    OptionsMenu.Update();
                }
            }
        }
        public static void Draw()
        {
            if(currentState == isMenu)
            {
                MainMenu.Draw();
            }
            else if(currentState == isGame)
            {
                MainGame.Draw();
                DungeonGame._objectHandler.Draw();
                MainGame.DrawUI();
            }
            else if(currentState == "Options")
            {
                OptionsMenu.Draw();
            }
            if(currentState == isGame && Paused)
            {
                PauseMenu.Draw();
            }
        }
    }
}
