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
    public class ConditionMessage : Message
    {
        public Basic2d bkg;
        public Button2d button;

        public PassObject ConfirmFunction;

        public ConditionMessage(Vector2 POS, Vector2 DIMS, string MSG, Color COLOR, bool LOCKSCREEN, PassObject CONFIRMFUNCTION) 
            : base(POS, DIMS, MSG, 1000, COLOR, LOCKSCREEN)
        {
            bkg = new Basic2d("2d/Misc/solid", new Vector2(pos.X, pos.Y), new Vector2(dims.X, dims.Y));

            button = new Button2d("2d/Misc/shade", new Vector2(pos.X, pos.Y + 25), new Vector2(70, 35), new Vector2(1, 2), "Fonts/Arial16", "Ok", new PassObject(CompleteClick), null);

            ConfirmFunction = CONFIRMFUNCTION;
        }

        public override void Update()
        {
            button.Update(Vector2.Zero);
        }

        public virtual void CompleteClick(object INFO)
        {
            ConfirmFunction(INFO);

            done = true;
        }

        public override void Draw()
        {
            bkg.Draw(Vector2.Zero);
            textZone.color = color;
            textZone.Draw(new Vector2(pos.X - textZone.dims.X/2, pos.Y - 20));

            if(button != null)
            {
                button.Draw(Vector2.Zero);
            }
        }


    }
}
