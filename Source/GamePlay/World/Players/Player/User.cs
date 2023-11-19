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
    public class User : Player
    {
        public User(int ID, XElement DATA, int LEVEL) : base(ID, DATA, LEVEL)
        {
            level = LEVEL;
        }

        public override void Update(Player ENEMY, Vector2 OFFSET, SquareGrid GRID, int LEVEL)
        {
            base.Update(ENEMY, OFFSET, GRID, LEVEL);
           
        }
    }
}
