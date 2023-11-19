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
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;
#endregion

namespace TopDownShooter
{
    public class LootBag : Animated2d
    {
        public List<InventoryItem> items = new List<InventoryItem>();

        public bool done;

        public LootBag(string PATH, Vector2 POS, List<InventoryItem> ITEMS)
            : base(PATH, POS, new Vector2(40, 40), new Vector2(1, 1), Color.White)
        {
           done = false;

            if(ITEMS != null)
            {
                items = ITEMS;
            }

        }

        public virtual void Update(Vector2 OFFSET)
        {
            if(Globals.mouse.LeftClick() && Hover(OFFSET))
            {
                for(int i=0; i<items.Count; i++)
                {
                    GameGlobals.AddToInventory(items[i]);
                }
                done = true;
            }
        }


    }
}
