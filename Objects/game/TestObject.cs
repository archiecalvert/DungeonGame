
using Dungeon_Game_MonoGame.Handlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Game_MonoGame.Objects.game
{
    public class TestObject
    {
        public SpriteBatch _spriteBatch = DungeonGame._spriteBatch;
        public ContentManager Content = DungeonGame._content;
        Texture2D _texture;
        public void LoadContent()
        {
            _texture = Content.Load<Texture2D>("assets/misc/placeholder");
        }
        public void Draw()
        {
            _spriteBatch.Draw(texture: _texture, position: new Vector2(0,0) + (DungeonGame.CoordHandler.PlayerCoords * InputHandler.coordinateReducer), null, color: Color.White, rotation: 0f, Vector2.Zero, scale: 4F, SpriteEffects.None, layerDepth: 0f); ;
        }
    }
}
