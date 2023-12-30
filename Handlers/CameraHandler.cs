using Microsoft.Xna.Framework;

namespace Dungeon_Game_MonoGame.Handlers
{
    public class CameraHandler
    {
        public bool isAttached = true; //Boolean for if the camera is currently attached to a game object
        public Vector2 CameraOffset; //if its detatched, how far away the camera is from the target object
        public void AttachToPlayer()
        {
            isAttached= true;
            CameraOffset = Vector2.Zero;
        }
        public void DetachFromPlayer()
        {
            isAttached= false;
            
        }
        public void MoveCamera(Vector2 cameraOffset)
        {
            CameraOffset += cameraOffset;
        }
    }
}
