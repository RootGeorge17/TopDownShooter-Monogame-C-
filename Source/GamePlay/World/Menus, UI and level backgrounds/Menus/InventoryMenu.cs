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
    public class InventoryMenu : Menu2d
    {
        public Escapee prisoner;
        public TextZone textZone;
        public TextZone textZone1;

        public InventoryMenu(Escapee PRISONER) :
            base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(350, 600), null)
        {
            prisoner = PRISONER;

            textZone = new TextZone(new Vector2(0, 0), "You have a total of " + prisoner.noOfSlots + " inventory slots. Progress in level to get more." , (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Black);
            textZone1 = new TextZone(new Vector2(0, 0), "You can drag items from slot to slot or you can drag items out of the inventory to drop items.", (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Black);
        }

        public override void Update()
        {
            base.Update();

            for (int i = 0; i < prisoner.inventorySlots.Count; i++)
            {
                Vector2 tempVec = new Vector2(40 + 54 * (int)(i % 6), 300 + 54 * (int)(i / 6));
                prisoner.inventorySlots[i].Update(topLeft + tempVec);
            }
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

                string tempStr = "" + prisoner.name + ": Inventory ";
                Vector2 strDims = font.MeasureString(tempStr);
                Globals.spriteBatch.DrawString(font, tempStr, topLeft + new Vector2(bkg.dims.X / 2 - strDims.X / 2, 40), Color.Black);

                for (int i=0; i< prisoner.inventorySlots.Count; i++)
                {
                    Vector2 tempVec = new Vector2(40 + 54 * (int)(i%6), 300 + 54 * (int)(i/6));
                    prisoner.inventorySlots[i].Draw(topLeft + tempVec);
                }

                textZone.Draw(topLeft + new Vector2(10, 110));


                textZone1.Draw(topLeft + new Vector2(10, 195));

            }
        }

    }
}
