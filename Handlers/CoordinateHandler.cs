using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace Dungeon_Game_MonoGame.Handlers
{
    public class CoordinateHandler
    {
        public Dictionary<string, Vector2> CoordinateDict = new Dictionary<string, Vector2>(); //Dictionary that stores all of the game objects coordinates with their identifiers
        public Vector2 PlayerCoords; //Allows for the players coordinates to be accessed anywhere
        public void MovePlayer(Vector2 Coords)
        {
            PlayerCoords += Coords * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds);
        }
        public Vector2 SetCoordinates(Vector2 vect)
        {
            vect = vect * InputHandler.coordinateReducer;
            return new Vector2(1920 / 2 + PlayerCoords.X * InputHandler.coordinateReducer, 1080 / 2 + PlayerCoords.Y * InputHandler.coordinateReducer) - vect;
        }
        public Vector2 CoordsToScreenPos(Vector2 vect) //converts a coordinate to where it would be located on the screen
        {
            return new Vector2(1920 / 2 + PlayerCoords.X * InputHandler.coordinateReducer, 1080 /2 + PlayerCoords.Y * InputHandler.coordinateReducer) - vect * InputHandler.coordinateReducer;
        }
    }
}
