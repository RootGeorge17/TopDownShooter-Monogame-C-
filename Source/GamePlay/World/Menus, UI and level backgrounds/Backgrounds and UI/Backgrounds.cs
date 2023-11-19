#region Includes
using System;
using System.Diagnostics;
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
#endregion

namespace TopDownShooter
{
    public class Backgrounds
    {

        public Backgrounds(XElement BACKGROUNDS)
        {
            LoadData(BACKGROUNDS);
        }

        public virtual void LoadData(XElement BACKGROUNDS)
        {
            string bgpath = BACKGROUNDS.Element("path").Value;
            Console.WriteLine("-------------------------");
            Console.WriteLine("-------------------------");
            Console.WriteLine(bgpath);
            Console.WriteLine("-------------------------");
            Console.WriteLine("-------------------------");
            Globals.background = Globals.content.Load<Texture2D>(bgpath);
        }

        public virtual void Draw()
        {

        }
    }
}
