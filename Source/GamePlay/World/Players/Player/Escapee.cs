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
    public class Escapee : Unit
    {
        public SkillBar skillBar;

        public string name;
        public string levelnumber;
        public int steps = 0;

        public string up = "W";
        public string down = "S";
        public string left = "A";
        public string right = "D";

        public int noOfSlots = 12;

        public Escapee(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID)
            : base(PATH, POS, DIMS, FRAMES, OWNERID)
        {
            speed = 1.5f;

            name = "Zeus";

            health = 10;
            healthMax = health;
            stamina = 10;
            staminaMax = stamina;
            wantedLevel = 1;
            wantedLevelMax = 5;

            skills.Add(new FlameWave(this));

            skillBar = new SkillBar(new Vector2(80, Globals.screenHeight - 80), 52, 10);

            for(int j=0; j<skills.Count; j++)
            {
                if (j < skillBar.slots.Count)
                {
                    skillBar.slots[j].skillButton = new SkillButton("2d/Misc/solid", new Vector2(0, 0), new Vector2(40, 40), new Vector2(1, 1), SetSkill, skills[j]);
                }
                else
                {
                    break;
                }
            }

            for(int i=0; i<noOfSlots; i++)
            {
                inventorySlots.Add(new InventorySlot(new Vector2(0, 0), new Vector2(48, 48)));
            }
        }

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
        {
            if (Globals.keyboard.GetPress(left))
            {
                pos = new Vector2(pos.X - speed, pos.Y);
            }
            if(Globals.keyboard.GetSinglePress(left))
            {
                steps++;
            }

            if(Globals.keyboard.GetPress(right))
            {
                pos = new Vector2(pos.X + speed, pos.Y);
            }
            if (Globals.keyboard.GetSinglePress(right))
            {
                steps++;
            }

            if (Globals.keyboard.GetPress(up))
            {
                pos = new Vector2(pos.X, pos.Y - speed);
            }
            if (Globals.keyboard.GetSinglePress(up))
            {
                steps++;
            }

            if (Globals.keyboard.GetPress(down))
            {
                pos = new Vector2(pos.X, pos.Y + speed);
            }
            if (Globals.keyboard.GetSinglePress(down))
            {
                steps++;
            }
            if (steps == 20)
            {
                stamina--;
                steps = 0;
            }
            if(stamina == 0)
            {
                up = "";
                down = "";
                left = "";
                right = "";
            }         
         
            rot = Globals.RotateTowards(pos, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET);


            if(currentSkill == null)
            {
                if(Globals.mouse.LeftClick())
                {
                    GameGlobals.PassProjectile(new Fireball(new Vector2(pos.X, pos.Y), this, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET));
                }
            }
            else
            {
                currentSkill.Update(OFFSET, ENEMY);

                if(currentSkill.done)
                {
                    currentSkill.Reset();
                    currentSkill = null;
                }
            }

            if(Globals.mouse.RightClick())
            {
                if(currentSkill != null)
                {
                    currentSkill.targetEffect.done = true;
                    currentSkill.Reset();
                    currentSkill = null;
                }
            }


            skillBar.Update(Vector2.Zero);

            base.Update(OFFSET, ENEMY, GRID);
        }

        public virtual void SetSkill(object INFO)
        {
            if(INFO != null)
            {
                currentSkill = (Skill)INFO;
                currentSkill.Active = true;
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);

            skillBar.Draw(Vector2.Zero);
        }
    }
}
