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
    public class ExitMenu : Menu2d
    {
        public List<Button2d> buttons = new List<Button2d>();

        public PassObject Exit;

        public ExitMenu(PassObject EXIT)
            : base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(350, 500), null)
        {
            Exit = EXIT;


            buttons.Add(new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(150, 40), new Vector2(1, 1), "Fonts\\Arial16", "Return", ReturnClick, 0));
            buttons.Add(new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(150, 40), new Vector2(1, 1), "Fonts\\Arial16", "Exit Level", ExitClick, 1));

        }

        public override void Update()
        {
            base.Update();

            if (Active)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Update(topLeft + new Vector2(dims.X / 2, 30 + 50 * i));
                }
            }
        }

        public virtual void ExitClick(object INFO)
        {
            Exit(INFO);
        }

        public virtual void ReturnClick(object INFO)
        {
            Active = false;
        }

        public override void Draw()
        {
            base.Draw();

            if (Active)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Draw(topLeft + new Vector2(dims.X / 2, 30 + 50 * i));
                }
            }
        }
    }
}