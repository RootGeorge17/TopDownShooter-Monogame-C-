﻿#region Includes
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
    public class PlayerValuePacket
    {

        public int playerId;

        public object value;

        public PlayerValuePacket(int PLAYERID, object VALUE)
        {

            playerId = PLAYERID;
            value = VALUE;

        }
    }
}
