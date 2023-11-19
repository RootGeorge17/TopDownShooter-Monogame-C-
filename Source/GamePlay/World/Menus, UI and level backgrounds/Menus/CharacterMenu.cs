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
    public class CharacterMenu : Menu2d
    {
        public Escapee prisoner;
        public TextZone textZoneHealth;
        public TextZone textZoneStamina;
        public TextZone textZoneWanted;
        public TextZone textZoneSlots;
        public TextZone textZoneWeaponDamage;

        public int weaponDamage;

        public CharacterMenu(Escapee PRISONER) :
            base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(350, 600), null)
        {
            prisoner = PRISONER;

            textZoneHealth = new TextZone(new Vector2(0, 0), "Max Health: " + prisoner.healthMax, (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Black);
            textZoneStamina = new TextZone(new Vector2(0, 0), "Max Stamina: " + prisoner.staminaMax, (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Black);
            textZoneWanted = new TextZone(new Vector2(0, 0), "Wanted Level: " + prisoner.wantedLevel, (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Black);
            textZoneSlots = new TextZone(new Vector2(0, 0), "Max Inventory Slots: " + prisoner.noOfSlots, (int)(dims.X * .9f), 22, "Fonts\\Arial16", Color.Black);
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

                string tempStr = "" + prisoner.name + ": Stats ";
                Vector2 strDims = font.MeasureString(tempStr);
                Globals.spriteBatch.DrawString(font, tempStr, topLeft + new Vector2(bkg.dims.X / 2 - strDims.X / 2, 40), Color.Black);

                textZoneHealth.Draw(topLeft + new Vector2(10, 100));

                textZoneStamina.Draw(topLeft + new Vector2(10, 120));

                textZoneWanted.Draw(topLeft + new Vector2(10, 140));

                textZoneSlots.Draw(topLeft + new Vector2(10, 160));

            }
        }
    }
}
