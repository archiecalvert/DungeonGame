using Dungeon_Game_MonoGame.Abilities;
using Dungeon_Game_MonoGame.GameStates;
using Dungeon_Game_MonoGame.GameStates.Menus;
using Dungeon_Game_MonoGame.Objects.game;
using Dungeon_Game_MonoGame.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Dungeon_Game_MonoGame.Handlers
{
    public class InputHandler
    {
        public static float MouseX, MouseY;
        public static bool LeftClick = false;
        public static bool isMoving = false;
        public static float coordinateReducer = 64f;
        public static float SpeedReducer = 0.5f;
        public static bool delayActive = false;
        public static float delay;
        public static bool MagicDelayActive = false;
        public static float magicDelay = 0;
        static float value = 3;
        static Dash dash = new Dash();
        public static bool GetKey(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }
        public static void KeyDelayUpdates()
        {
            if(!GameStateHandler.Paused)
            {
                if (MagicDelayActive) //used as a delay between shooting magic projectiles
                {
                    magicDelay -= 0.2f * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds);
                    if (magicDelay <= 0)
                    {
                        MagicDelayActive = false;
                    }
                }
                if (Dash.cooldownDuration < 50 && Dash.hasBeenActive)
                {
                    Dash.cooldownDuration += 0.05f * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds);
                }
                else
                {
                    Dash.hasBeenActive= false;
                    
                }
            }
            if (delayActive)
            {
                delay -= 0.05f * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds);
                if (delay <= 0)
                {
                    delayActive = false;
                }
            }
        }
        public static void Update()
        {
            //PLAYER MOVEMENT
            if(!GameStateHandler.Paused)
            {
                if (GetKey(Keys.Space) && !Dash.isActive && !Dash.hasBeenActive && DungeonGame.Camera.isAttached)
                {
                    Dash.StartCoordinates = DungeonGame.CoordHandler.PlayerCoords;
                    Dash.TargetCoordinates = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                    Dash.Begin();
                }
                isMoving= false;
                if (GetKey(Keys.W) && !GetKey(Keys.S))
                {
                    if (DungeonGame.Camera.isAttached)
                    {
                        if (!DungeonGame.CollisionSystem.CheckIfColliding(MainGame.PlayerHitBox, "Y", 3))
                        {
                            isMoving = true;
                            DungeonGame.CoordHandler.MovePlayer(new Vector2(0, playerStats.movementSpeed * SpeedReducer / coordinateReducer));
                        }
                    }
                    else
                    {
                        DungeonGame.Camera.CameraOffset += new Vector2(0, settings.CameraSpeed) * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    
                }
                if (GetKey(Keys.S) && !GetKey(Keys.W))
                {
                    if (DungeonGame.Camera.isAttached)
                    {
                        if (!DungeonGame.CollisionSystem.CheckIfColliding(MainGame.PlayerHitBox, "Y", -3))
                        {
                            isMoving = true;
                            DungeonGame.CoordHandler.MovePlayer(new Vector2(0, -playerStats.movementSpeed * SpeedReducer / coordinateReducer));

                        }
                    }
                    else
                    {
                        DungeonGame.Camera.CameraOffset += new Vector2(0, -settings.CameraSpeed) * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    
                }
                if (GetKey(Keys.D) && !GetKey(Keys.A)) 
                {
                    if (DungeonGame.Camera.isAttached)
                    {
                        if (!DungeonGame.CollisionSystem.CheckIfColliding(MainGame.PlayerHitBox, "X", 3))
                        {
                            isMoving = true;
                            DungeonGame.CoordHandler.MovePlayer(new Vector2(-playerStats.movementSpeed * SpeedReducer / coordinateReducer, 0));

                        }
                    }
                    else
                    {
                        DungeonGame.Camera.CameraOffset += new Vector2(-settings.CameraSpeed, 0) * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    
                }
                if (GetKey(Keys.A) && !GetKey(Keys.D))
                {
                    if (DungeonGame.Camera.isAttached)
                    {
                        if (!DungeonGame.CollisionSystem.CheckIfColliding(MainGame.PlayerHitBox, "X", -3))
                        {
                            isMoving = true;
                            DungeonGame.CoordHandler.MovePlayer(new Vector2(playerStats.movementSpeed * SpeedReducer / coordinateReducer, 0));
                        }
                    }
                    else
                    {
                        DungeonGame.Camera.CameraOffset += new Vector2(settings.CameraSpeed, 0) * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds);
                    }
                     
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (magicDelay <= 0)
                    {
                        MagicBall magic = new MagicBall();
                        magic.CreateProjectile(DungeonGame.CoordHandler.PlayerCoords, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
                        magicDelay = 1;
                        MagicDelayActive = true;
                    }
                }
            }
            //DEBUG ENABLER
            if (GetKey(Keys.P) && !delayActive) //enables debugging
            {
                settings.Debugger = !settings.Debugger;
                delayActive = true;
                delay = value;
            }
            //CAMERA CONTROLS
            if (GetKey(Keys.L) && !delayActive) //enables the camera to be detached
            {
                if(DungeonGame.Camera.isAttached)
                {
                    DungeonGame.Camera.DetachFromPlayer();
                }
                else
                {
                    DungeonGame.Camera.AttachToPlayer();
                }
                delayActive = true;
                delay = value;
            }
            //MOUSE POSITION
            if (GetKey(Keys.Escape) && !delayActive && GameStateHandler.currentState != "Main Menu" && PauseMenu.currentMenu == null)
            {
                GameStateHandler.Paused = !GameStateHandler.Paused;
                delayActive = true;
                delay = value;
            }
            MouseX = Mouse.GetState().X;
            MouseY = Mouse.GetState().Y;
            //LEFT CLICK
            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                LeftClick  = true;
            }
            else
            {
                LeftClick = false;
            }
            //KEY DELAY HANDLER
            KeyDelayUpdates();
        }
    }
}
