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
    public class MissionsMenu : Menu2d
    {
        public Escapee prisoner;

        public TextZone textZone;

        public int level;

        public string mission1 = "The bars of your cell have opened, now it's the perfect time to escape the prison. The guards have been notified, your mission is to kill all the guards until one drops the key to the door that will get you to the next room.";

        public string mission2 = "The mission for this level is to kill 20 guards until you get dropped a key.";

        public string mission3 = "The mission for this level is to kill 30 guards until you get dropped a key.";

        public string mission4 = "The mission for this level is to kill 40 guards until you get dropped a key.";

        public string mission5 = "The mission for this level is to kill 50 guards until you get dropped a key.";

        public string mission6 = "The mission for this level is to kill 60 guards until you get dropped a key.";

        public MissionsMenu(Escapee PRISONER, int LEVEL) :
            base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(350, 600), null)
        {
            prisoner = PRISONER;

            level = LEVEL;

            if(level == 1)
            {
                textZone = new TextZone(new Vector2(0, 0), mission1, (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Black);
                
            }
            else if(level == 2)
            {
                textZone = new TextZone(new Vector2(0, 0), mission2, (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Black);
            }
            else if (level == 3)
            {
                textZone = new TextZone(new Vector2(0, 0), mission3, (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Black);
            }
            else if (level == 4)
            {
                textZone = new TextZone(new Vector2(0, 0), mission4, (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Black);
            }
            else if (level == 5)
            {
                textZone = new TextZone(new Vector2(0, 0), mission5, (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Black);
            }
            else if (level == 6)
            {
                textZone = new TextZone(new Vector2(0, 0), mission6, (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Black);
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();

            if(Active)
            {
                Globals.normalEffect.Parameters["xSize"].SetValue(1.0f);
                Globals.normalEffect.Parameters["ySize"].SetValue(1.0f);
                Globals.normalEffect.Parameters["xDraw"].SetValue(1.0f);
                Globals.normalEffect.Parameters["yDraw"].SetValue(1.0f);
                Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
                Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

                string tempStr = "" + prisoner.name + " Level: " + level;
                Vector2 strDims = font.MeasureString(tempStr);
                Globals.spriteBatch.DrawString(font, tempStr, topLeft + new Vector2(bkg.dims.X / 2 - strDims.X / 2, 40), Color.Black);

                textZone.Draw(topLeft + new Vector2(10, 100));       
            }
        }
    }
}
