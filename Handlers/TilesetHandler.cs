using System.Collections.Generic;
using System.IO;
using Dungeon_Game_MonoGame.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Game_MonoGame.Handlers
{
    public class TilesetHandler
    {
        //NOTE!! FOR THE DATA FILES YOU NEED TO CHANGE THE PROPERTIES OF THE FILE SO
        //THAT COPY TO OUPUT IS COPY IF NEWER

        string _dir;
        int _tileWidth;
        int _tileHeight;
        Texture2D _texture;
        int _mapWidth;
        int _mapHeight;
        List<Rectangle> _sourceRectangle = new List<Rectangle>();
        int[] _data;
        float scale = settings.GameScale * 10;
        ContentManager Content = DungeonGame._content;
        SpriteBatch _spriteBatch = DungeonGame._spriteBatch;
        GraphicsDeviceManager _graphics = DungeonGame._graphics;
        float localScale = settings.GameScale;
        public Vector2 Offset = new Vector2(11, -5);
        public TilesetHandler(string mapData, int scale)
        {
            _dir = mapData;
            localScale = localScale * scale;
        }
        public void LoadContent()
        {
            string _file = File.ReadAllText(Path.Join(Content.RootDirectory, _dir));
            var lines = _file.Split('\n');
            //Extracts the filename
            var tilesetFilename = lines[0].Trim();
            _texture = Content.Load<Texture2D>(tilesetFilename);
            //Extracts the tile dimensions
            var secondLine = lines[1].Split(",");
            _tileWidth = int.Parse(secondLine[0]);
            _tileHeight = int.Parse(secondLine[1]);
            int _tilesetColumns = _texture.Width / _tileWidth;
            int _tilesetRows = _texture.Height / _tileHeight;
            //Extracts the map dimensions
            var thirdLine = lines[2].Split(",");
            _mapWidth = int.Parse(thirdLine[0]);
            _mapHeight = int.Parse(thirdLine[1]);
            //Extracts the map data
            var fourthLine = lines[3].Split(",");
            _data = new int[_mapWidth * _mapHeight];
            for (int y = 0; y < _tilesetRows; y++)
            {
                for (int x = 0; x < _tilesetColumns; x++)
                {
                    _sourceRectangle.Add(new Rectangle(x * _tileWidth, y * _tileHeight, _tileWidth, _tileHeight));
                }
            }
            int num = _sourceRectangle.Capacity;
            for (int i = 0; i < _mapWidth * _mapHeight; i++)
            {
                _data[i] = int.Parse(fourthLine[i]);
            }
        }
        public void DrawTilemap()
        {
            int index = 0;
            for (int y = 0; y < _mapHeight; y++) //loops through the columns
            {
                for (int x = 0; x < _mapWidth; x++) //loops through the rows
                {   
                    if (_data[index] != 0) //if the index is 0, a blank tile will be drawn (i.e. nothing will be drawn)
                    {
                        if(CullingHandler.IsVisible(DungeonGame.CoordHandler.SetCoordinates(new Vector2(Offset.X, -Offset.Y)) + new Vector2(x * _tileWidth, y * _tileHeight) + DungeonGame.Camera.CameraOffset)) //checks to see if the tile is currently in view
                        {
                            Vector2 Pos = DungeonGame.CoordHandler.SetCoordinates(new Vector2(Offset.X, -Offset.Y)) + new Vector2(x * _tileWidth, y * _tileHeight) ;
                            _spriteBatch.Draw(texture: _texture,
                                          position: new Vector2(Pos.X * localScale, Pos.Y*localScale) + DungeonGame.Camera.CameraOffset,
                                          _sourceRectangle[_data[index] - 1],
                                          color: Color.White,
                                          rotation: 0f,
                                          new Vector2(_tileWidth/2, _tileWidth/2),
                                          scale: localScale,
                                          SpriteEffects.None,
                                          layerDepth: 1f);
                        }
                    }
                    index += 1; //holds the current item in the array of tiles
                }
            }

        }
    }
}
