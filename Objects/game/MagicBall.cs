using System;
using Dungeon_Game_MonoGame.Handlers;
using Dungeon_Game_MonoGame.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dungeon_Game_MonoGame.Objects.game
{
    public class MagicBall
    {
        public float MagicSpeed = 250;
        public bool isDestroyed = false;
        public AnimatedObject projectile;
        Vector2 direction;
        float adj;
        float opp;
        Hitbox _hitbox;
        public void CreateProjectile(Vector2 StartPosition, Vector2 Target)
        {
            adj = Target.X - DungeonGame.CoordHandler.CoordsToScreenPos(StartPosition).X * DungeonGame.scale; //calculates the horizontal distance between the player and the mouse
            opp = Target.Y - DungeonGame.CoordHandler.CoordsToScreenPos(StartPosition).Y * DungeonGame.scale; //calculates the vertical distance between the player and the mouse
            direction = Vector2.Normalize(new Vector2(adj, opp)); //makes the length of the final vector 1
            projectile = new AnimatedObject(DungeonGame._content.Load<Texture2D>("assets/projectiles/fireball/fireball"), 16, 24, StartPosition, 100, 2F); //creates the animated fireball
            projectile.LoadContent();
            DungeonGame._objectHandler.AddMagic(this); //adds the magicball to the list
            _hitbox = new Hitbox(new Vector2(16 * 2,24 * 2), StartPosition, true);
            _hitbox.LoadContent();
        }
        public void Update()
        {
            if(!isDestroyed)
            {
                projectile.pos -= direction * MagicSpeed * 0.001f * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds); //moves the fireball every frame
                _hitbox.Coords -= direction * MagicSpeed * 0.001f * (float)(144 * DungeonGame._gameTime.ElapsedGameTime.TotalSeconds);       
            }
        }
        public void Draw()
        {
            projectile.Draw();
            if (settings.Debugger)
            {
                _hitbox.Draw();
            }
        }
    }
}
