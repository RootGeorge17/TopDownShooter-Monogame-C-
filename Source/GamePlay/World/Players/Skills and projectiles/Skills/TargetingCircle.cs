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
    public class TargetingCircle : Effect2d
    {

        public TargetingCircle(Vector2 POS, Vector2 DIMS) : base("2d\\Effects\\TargetCircle", POS, DIMS, new Vector2(1, 1), 400)
        {
            noTimer = true;
        }

    }
}
