#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace TopDownShooter
{
    public class Soldier : Mob
    {
        public Soldier(Vector2 POS, Vector2 FRAMES, int OWNERID, int LEVEL)
            : base("2d\\Units\\Mobs\\Guard", POS, new Vector2(80, 80), FRAMES, OWNERID, LEVEL)
        {
            speed = 2.0f;

            attackRange = 400;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
        {
            base.Update(OFFSET, ENEMY, GRID);
        }

        public override void AI(Player ENEMY, SquareGrid GRID)
        {
            if (ENEMY.prisoner != null && (Globals.GetDistance(pos, ENEMY.prisoner.pos) < attackRange * .9f)) 
            {
                isAttacking = true;

                attackTimer.UpdateTimer();

                if(attackTimer.Test())
                {
                    GameGlobals.PassProjectile(new Fireball(new Vector2(pos.X, pos.Y), this, new Vector2(ENEMY.prisoner.pos.X, ENEMY.prisoner.pos.Y)));

                    attackTimer.ResetToZero();
                    isAttacking = false;
                }
            }
            else
            {
                base.AI(ENEMY, GRID);
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
