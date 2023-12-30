using System;
using System.Collections.Generic;
using Dungeon_Game_MonoGame.Objects.game;
using Microsoft.Xna.Framework;

namespace Dungeon_Game_MonoGame.Handlers
{
    public class ObjectHandler
    {
        public List<Object> gameObjects = new List<object>();
        public List<MagicBall> magicList = new List<MagicBall>();
        public static Vector2 Offset = Vector2.Zero;
        public void AddObject(Object obj)
        {
            gameObjects.Add(obj);
        }
        public void AddMagic(MagicBall obj)
        {
            magicList.Add(obj);
        }
        public void Update()
        {
            if (magicList!= null)
            {
                if(!GameStateHandler.Paused)
                {
                    foreach (MagicBall obj in magicList)
                    {
                        obj.Update();
                    }
                }
                
            }
        }
        public void Draw()
        {
            if (magicList!=null)
            {
                foreach(MagicBall obj in magicList) { obj.Draw(); }
            }
        }
    }
}
