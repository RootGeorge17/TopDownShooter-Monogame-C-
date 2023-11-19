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
    public class SkillButton : Button2d
    {
        public Skill skill;

        public SkillButtonSlot slot;

        public SkillButton(string PATH, Vector2 POS, Vector2 DIMS, Vector2 Frames, PassObject BUTTONCLICKED, object INFO)
            : base(PATH, POS, DIMS, Frames, "", "", BUTTONCLICKED, INFO)
        {
            skill = (Skill)INFO;
            slot = null;

        }

        public override void Update(Vector2 OFFSET)
        {
            if(skill != null)
            {
                base.Update(OFFSET);
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);

            if(skill != null)
            {
                skill.icon.Draw(OFFSET);
            }
        }



    }
}
