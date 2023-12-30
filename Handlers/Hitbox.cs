using Dungeon_Game_MonoGame.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Game_MonoGame.Handlers
{
    public class Hitbox
    {
        public Vector2 _size;
        public Texture2D hitboxRectangle;
        public Vector2 Coords;
        bool IsAttached = false;
        string _ObjectName;
        bool _isMoveable;
        public Rectangle _hitbox;
        public Hitbox(Vector2 size, Vector2 Coordinates, bool IsMoveable)
        {
            _size = size;
            Coords = Coordinates;
            _hitbox = new Rectangle((int)Coords.X, (int)Coords.Y, (int)size.X, (int)size.Y);
            _isMoveable = IsMoveable;
        }
        public void LoadContent()
        {
            hitboxRectangle = DungeonGame._content.Load<Texture2D>("assets/misc/hitbox"); //Debug texture for hitboxes
            if(_isMoveable)
            {
                DungeonGame.CollisionSystem.MoveableHitboxes.Add(this); //adds the hitbox to a list of moveable hitboxes
            }
            else
            {
                DungeonGame.CollisionSystem.StaticHitboxes.Add(this); //adds the hitbox to a list of immoveable hitboxes
            }
        }
        public void Draw()
        {
            DungeonGame._spriteBatch.Draw(texture: hitboxRectangle, 
                                          position: DungeonGame.CoordHandler.CoordsToScreenPos(Coords) + DungeonGame.Camera.CameraOffset,
                                          sourceRectangle: null,
                                          color: Color.Red,
                                          rotation: 0f,
                                          new Vector2(hitboxRectangle.Width/2, hitboxRectangle.Height/2),
                                          scale: new Vector2(_size.X/hitboxRectangle.Width, _size.Y/hitboxRectangle.Height) * settings.GameScale,
                                          SpriteEffects.None,
                                          layerDepth: 1f); //if debug settings is enabled, it draws the hitbox to the screen
        }
        public void Update()
        {
            if (!IsAttached)
            {
                return;
            };
            DungeonGame.CoordHandler.CoordinateDict.TryGetValue(_ObjectName, out Vector2 temp);
            Coords = temp;

        }
        public void AttachToObject(string ObjectName)//allows for hitboxes to move if attached to a moving object
        {
            if(DungeonGame.CoordHandler.CoordinateDict.TryGetValue(ObjectName, out Vector2 coord))
            {
                IsAttached= true;
                _ObjectName = ObjectName;
            }
        }
    }
}
