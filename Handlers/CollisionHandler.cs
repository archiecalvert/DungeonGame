using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using Dungeon_Game_MonoGame.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Dungeon_Game_MonoGame.Handlers
{
    public class CollisionHandler
    {
        public bool isD = false;
        public List<Hitbox> MoveableHitboxes = new List<Hitbox>();
        public List<Hitbox> StaticHitboxes = new List<Hitbox>();
        public void Update()
        {
            foreach (Hitbox item in MoveableHitboxes)
            {
                item.Update();
            }
            
        }
        public bool CheckIfColliding(Hitbox Main, string axis, int distance)
        {
            Rectangle MainR = new Rectangle();
            if (axis == "x" || axis == "X")
            { 
                    MainR = new Rectangle((int)(DungeonGame.CoordHandler.CoordsToScreenPos(Main.Coords).X + distance), (int)(DungeonGame.CoordHandler.CoordsToScreenPos(Main.Coords).Y), (int)Main._size.X, (int)Main._size.Y);  
            }
            if(axis == "y" || axis == "Y")
            {
                MainR = new Rectangle((int)DungeonGame.CoordHandler.CoordsToScreenPos(Main.Coords).X, (int)DungeonGame.CoordHandler.CoordsToScreenPos(Main.Coords).Y - distance, (int)Main._size.X, (int)Main._size.Y);
            }
            Rectangle ItemR;
            foreach(Hitbox item in StaticHitboxes)
            {
                ItemR = new Rectangle((int)DungeonGame.CoordHandler.CoordsToScreenPos(item.Coords).X, (int)DungeonGame.CoordHandler.CoordsToScreenPos(item.Coords).Y, (int)item._size.X, (int)item._size.Y);
                if(MainR.Intersects(ItemR))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
