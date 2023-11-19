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
    public class AIPlayer : Player
    {
        public AIPlayer(int ID, XElement DATA, int LEVEL)
            : base(ID, DATA, LEVEL)
        {
            level = LEVEL;
        }

        public override void Update(Player ENEMY, Vector2 OFFSET, SquareGrid GRID, int LEVEL)
        {
            base.Update(ENEMY, OFFSET, GRID, LEVEL);
        }

        public override void ChangeScore(int SCORE)
        {
            GameGlobals.score += SCORE;
        }

        public override void CheckIfDefeated()
        {
            if(level == 1)
            {
                if (GameGlobals.killConfirmed >= 1 && GameGlobals.pickedKeys >= 1)
                {
                    defeated = true;
                    GameGlobals.killConfirmed = 0;
                }
            }
            if (level == 2)
            {
                if (GameGlobals.killConfirmed >= 1 && GameGlobals.pickedKeys >= 2)
                {
                    defeated = true;
                    GameGlobals.killConfirmed = 0;
                }
            }
            if (level == 3)
            {
                if (GameGlobals.killConfirmed >= 1 && GameGlobals.pickedKeys >= 3)
                {
                    defeated = true;
                    GameGlobals.killConfirmed = 0;
                }
            }
            if (level == 4)
            {
                if (GameGlobals.killConfirmed >= 1 && GameGlobals.pickedKeys >= 4)
                {
                    defeated = true;
                    GameGlobals.killConfirmed = 0;
                }
            }
            if (level == 5)
            {
                if (GameGlobals.killConfirmed >= 1 && GameGlobals.pickedKeys >= 5)
                {
                    defeated = true;
                    GameGlobals.killConfirmed = 0;
                }
            }
            if (level == 6)
            {
                if (GameGlobals.killConfirmed >= 1 && GameGlobals.pickedKeys >= 6)
                {
                    defeated = true;
                    GameGlobals.killConfirmed = 0;
                    GameGlobals.pickedKeys = 0;
                }
            }
        }
    }
}
