using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Dungeon_Game_MonoGame.Abilities
{
    public class Dash
    {
        static Vector2 _dir;
        public static float speed = 6f;
        public static float duration = 5f;
        static float adj, opp;
        public static Vector2 TargetCoordinates, StartCoordinates;
        public static bool isActive = false;
        public static bool hasBeenActive = false;
        public static float cooldownDuration = 50f;
        public static void Begin()
        {
            adj = TargetCoordinates.X - DungeonGame.CoordHandler.CoordsToScreenPos(StartCoordinates).X * DungeonGame.scale; //calculates the horizontal distance between the player and the mouse
            opp = TargetCoordinates.Y - DungeonGame.CoordHandler.CoordsToScreenPos(StartCoordinates).Y * DungeonGame.scale;
            _dir = Vector2.Normalize(new Vector2(adj, opp));
            isActive = true;
        }
        public static void Update()
        {
            if (isActive)
            {
                duration -= 0.4f * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds);
                if (duration > 0)
                {
                    DungeonGame.CoordHandler.PlayerCoords -= (_dir) * (speed / 20) * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds);
                }
                else
                {
                    isActive= false;
                    hasBeenActive= true;
                    duration = 5f;
                    cooldownDuration = 0f;
                }
            }

        }
    }
}
