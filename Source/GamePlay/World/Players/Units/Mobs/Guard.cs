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
    public class Guard : Mob
    {
        public Guard(Vector2 POS, Vector2 FRAMES, int OWNERID, int LEVELID)
            : base("2d\\Units\\MObs\\Guard", POS, new Vector2(80, 80), FRAMES, OWNERID, LEVELID)
        {
            speed = 2.0f;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
        {
            base.Update(OFFSET, ENEMY, GRID);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
