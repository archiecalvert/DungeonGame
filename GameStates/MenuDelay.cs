using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Game_MonoGame.GameStates
{
    public class MenuDelay
    {
        static float value = 3f;
        float delay = value;
        bool _delayActive = true;
        public bool isActive()
        {
            if (_delayActive)
            {
                return true;
            }
            return false;
        }
        public void Activate()
        {
            _delayActive = true;
        }
        public void Update()
        {
            if(_delayActive)
            {
                delay -= 0.1f * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds);
                if(delay <= 0)
                {
                    delay = value;
                    _delayActive= false;
                }
            }

        }
    }
}
