
using System.Collections.Generic;
using Dungeon_Game_MonoGame.Handlers;
using Dungeon_Game_MonoGame.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Game_MonoGame.Objects.game
{
    public class AnimatedObject
    {
        Texture2D _texture;
        int _numberOfFrames;
        int _columns;
        int _rows;
        public Vector2 pos;
        SpriteBatch _spriteBatch = DungeonGame._spriteBatch;
        ContentManager Content = DungeonGame._content;
        Vector2 _size;
        List<Rectangle> _sourceRectangle = new List<Rectangle>();
        int frameNum;
        int _speed;
        int timeSinceLastFrame = 0;
        float _scale;
        GameTime gameTime = DungeonGame._gameTime;
        public AnimatedObject(Texture2D texture, int width, int height, Vector2 coordinates, int speed, float scale)
        {
            _texture= texture;
            pos = new Vector2(coordinates.X, coordinates.Y);
            _numberOfFrames = (_texture.Width/width) * (_texture.Height/height);
            _rows = (int)(_texture.Height/height);
            _columns = (int)(_texture.Width/width);
            _size = new Vector2(width, height);
            _speed = speed;
            _scale = scale;
        }
        public void LoadContent()
        {
            for(int y = 0; y < _rows; y++)
            {
                for(int x = 0; x < _columns; x++)
                {
                    _sourceRectangle.Add(new Rectangle((int)(x * _size.X), (int)(y * _size.Y), (int)_size.X, (int)_size.Y));
                }
            
                
            }
        }
        public void Draw()
        {
            if (CullingHandler.IsVisible(DungeonGame.CoordHandler.SetCoordinates(pos) + DungeonGame.Camera.CameraOffset))
            {
                _spriteBatch.Draw(texture: _texture,
                                          position: DungeonGame.CoordHandler.SetCoordinates(new Vector2(pos.X, pos.Y)) * settings.GameScale + DungeonGame.Camera.CameraOffset,
                                          _sourceRectangle[frameNum],
                                          color: Color.White,
                                          rotation: 0f,
                                          new Vector2(_size.X/2, _size.Y/2),
                                          scale: _scale * settings.GameScale,
                                          SpriteEffects.None,
                                          layerDepth: 1f);

            }
            if(!GameStateHandler.Paused)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > _speed)
                {
                    timeSinceLastFrame -= _speed;
                    if (frameNum == _numberOfFrames - 1)
                    {
                        frameNum = 0;
                    }
                    else
                    {
                        frameNum++;

                    }
                }
            }  
        }
    }
}
