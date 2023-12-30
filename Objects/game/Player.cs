using System;
using Dungeon_Game_MonoGame.Handlers;
using Dungeon_Game_MonoGame.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Game_MonoGame.Objects.game
{
    public class Player : InputHandler
    {
        public Vector2 Pos;
        private Texture2D[] sprites = new Texture2D[2];
        private int currentFrame = 0;
        public Texture2D currentImage;
        ContentManager Content = DungeonGame._content;
        SpriteBatch _spriteBatch = DungeonGame._spriteBatch;
        GameTime gameTime = DungeonGame._gameTime;
        private SpriteEffects faceLeft;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 100;
        public Player(Vector2 position)
        {
            Pos = position;
        }
        public void LoadContent()
        {
            DungeonGame.CoordHandler.PlayerCoords = new Vector2(0, 0); //sets the initial player coordinates
            sprites[0] = Content.Load<Texture2D>("assets/player/idle1L");
            sprites[1] = Content.Load<Texture2D>("assets/player/idle2L");
            currentImage = sprites[1];
            
        }
        public void Update()
        {
            currentImage = sprites[currentFrame]; //holds the current frame of the walking animation
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds; //used to set the speed of the animation
            if (timeSinceLastFrame > millisecondsPerFrame) //checks to see whether the frame needs to be changed
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                if (isMoving)
                {
                    if (currentFrame == 1) { currentFrame = 0; }
                    else { currentFrame = 1; }
                }
                else { currentFrame = 0; }
            }
            if (MouseX > Pos.X * DungeonGame.scale) //used to turn the player depending on the mouse position
            {
                faceLeft = SpriteEffects.FlipHorizontally;
            }
            else
            {
                faceLeft = SpriteEffects.None;
            }
        }
        public void Draw()
        {
            _spriteBatch.Draw(texture: currentImage, position: Pos + DungeonGame.Camera.CameraOffset, null, color: Color.White, rotation: 0f, new(currentImage.Width / 2, currentImage.Height / 2), scale: settings.GameScale * 0.55f, faceLeft, layerDepth: 0f);
        }
    }
}
