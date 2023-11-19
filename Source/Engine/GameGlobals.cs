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

    public class GameGlobals
    {
        public static bool paused = false;

        public static PassObject PassProjectile, PassBuilding, PassEffect, PassGold, PassMob, PassLootBag, PassKey, PassSpawnPoint, AddToInventory;

        public static int killConfirmed = 0;
        public static int pickedKeys = 0;
        public static int CompletedLevels = 0;
        public static int WinConfirmed = 0;

        //stats
        // levels
        public static int score = 0;
        public static int requiredScore = 5;

        public static int level = 1;
        public static int level2 = 2;





    }
}
