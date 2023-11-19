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
    public class SkillMenu
    {
        public bool active;

        Animated2d bkg;

        Skill selectedSkill;

        Escapee prisoner;



        public SkillMenu(Escapee PRISONER)
        {
            bkg = new Animated2d("2d/Misc/solid", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(300, 400), new Vector2(1, 1), Color.Gray);

            prisoner = PRISONER;
        }

        public virtual void Update()
        {
            if(active)
            {
                for(int i=0; i< prisoner.skills.Count; i++)
                {
                    if(prisoner.skills[i].icon.Hover(bkg.pos - bkg.dims / 2 + new Vector2(30 + (i % 4) * 50, 30 + (i / 4) * 50)))
                    {
                        if(Globals.mouse.LeftClick())
                        {
                            selectedSkill = prisoner.skills[i];
                            selectedSkill.icon.color = Color.Blue;
                        }
                    }
                    else
                    {
                        prisoner.skills[i].icon.color = Color.White;
                    }
                }

                if(Globals.keyboard.GetSinglePress("D1"))
                {
                    prisoner.skillBar.slots[0].skillButton = new SkillButton("2d/Misc/solid", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), prisoner.SetSkill, selectedSkill);
                }
                if (Globals.keyboard.GetSinglePress("D2"))
                {
                    prisoner.skillBar.slots[1].skillButton = new SkillButton("2d/Misc/solid", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), prisoner.SetSkill, selectedSkill);
                }
                if (Globals.keyboard.GetSinglePress("D3"))
                {
                    prisoner.skillBar.slots[2].skillButton = new SkillButton("2d/Misc/solid", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), prisoner.SetSkill, selectedSkill);
                }
                if (Globals.keyboard.GetSinglePress("D4"))
                {
                    prisoner.skillBar.slots[4].skillButton = new SkillButton("2d/Misc/solid", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), prisoner.SetSkill, selectedSkill);
                }
                if (Globals.keyboard.GetSinglePress("D5"))
                {
                    prisoner.skillBar.slots[5].skillButton = new SkillButton("2d/Misc/solid", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), prisoner.SetSkill, selectedSkill);
                }
                if (Globals.keyboard.GetSinglePress("D6"))
                {
                    prisoner.skillBar.slots[6].skillButton = new SkillButton("2d/Misc/solid", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), prisoner.SetSkill, selectedSkill);
                }
            }
        }

        public virtual void ToggleActive()
        {
            active = !active;
            selectedSkill = null;
        }

        public virtual void Draw()
        {
            if (active)
            {
                bkg.Draw(Vector2.Zero);

                for (int i = 0; i < prisoner.skills.Count; i++)
                {
                    prisoner.skills[i].icon.Draw(bkg.pos - bkg.dims / 2 + new Vector2(30 + (i%4) * 50, 30 + (i/4) * 50));
                }
            }
        }


    }
}
