
using Dungeon_Game_MonoGame.Handlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Dungeon_Game_MonoGame.Objects.UI
{
    public class Button : InputHandler
    {
        public Texture2D _texture;
        Color _color;
        Vector2 _position;
        SpriteBatch _spriteBatch = DungeonGame._spriteBatch;
        GraphicsDeviceManager _graphics = DungeonGame._graphics;
        Rectangle _rectangle;
        Vector2 _scale;
        public Button(Texture2D texture, Color color, Vector2 position, Vector2 scale)
        {
            _texture = texture;
            _color = color;
            _position = position;
            _scale = scale;
        }
        public bool isClicked = false;
        public bool isVisible = true;
        bool down;
        public void Update()
        {
            isClicked = false;
            _rectangle = new Rectangle((int)((_position.X - (_texture.Width * _scale.X / 2)) * DungeonGame.scale), (int)((_position.Y - (_texture.Height * _scale.Y / 2)) * DungeonGame.scale), (int)(_texture.Width * _scale.X * DungeonGame.scale), (int)(_texture.Height * _scale.Y * DungeonGame.scale));
            Rectangle mouseRect = new Rectangle((int)MouseX, (int)MouseY, 1, 1); //used to see whether the mouse is touching the button
            if (mouseRect.Intersects(_rectangle) && LeftClick)
            {
                if (!GameStateHandler.menuDelay.isActive())
                {
                    isClicked = true;
                    GameStateHandler.menuDelay.Activate();
                }
                
            }
        }
        public void Draw()
        {
            if (isVisible)
            {
                _spriteBatch.Draw(texture: _texture, position: _position, null, color: _color, rotation: 0f, new Vector2(_texture.Width/2, _texture.Height/2), scale: _scale, SpriteEffects.None, layerDepth: 0f);
            }
        }

    }
}
