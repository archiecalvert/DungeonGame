using Dungeon_Game_MonoGame.Stats;
using Microsoft.Xna.Framework;

namespace Dungeon_Game_MonoGame.Handlers
{
    public class CullingHandler : DungeonGame
    {
        public CullingHandler() { }
        public static bool IsVisible(Vector2 vect)
        {
            if(vect.X >= -64 / settings.GameScale && vect.X <= (1920 + 64)/settings.GameScale) //checks to see if the position passed in is horizontally on screen
            {
                if(vect.Y >= -64 / settings.GameScale && vect.Y <= (1080 + 64) / settings.GameScale) //checks to see if the position passed in is vertically on screen
                {
                    return true;
                }
            }
            return false;
        }
    }
}
