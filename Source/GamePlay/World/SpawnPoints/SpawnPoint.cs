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
    public class SpawnPoint: AttackableObject
    {
        public int level;

        public List<MobChoice> mobChoices = new List<MobChoice>();

        public TimerClass spawnTimer = new TimerClass(2400);

        public SpawnPoint(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID, XElement DATA, int LEVEL)
            : base(PATH, POS, DIMS, FRAMES, OWNERID)
        {
            dead = false;

            level = LEVEL;

            health = 999;
            healthMax = health;


            LoadData(DATA);

            hitDist = 35.0f;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
        {
            spawnTimer.UpdateTimer();
            if(spawnTimer.Test())
            {
                SpawnMob();
                spawnTimer.ResetToZero();
            }

            base.Update(OFFSET, ENEMY, GRID);
        }

        public virtual void LoadData(XElement DATA)
        {
            if(DATA != null)
            {
                spawnTimer.AddToTimer(Convert.ToInt32(DATA.Element("timerAdd").Value, Globals.culture));


                List<XElement> mobList = (from t in DATA.Descendants("mob")
                                            select t).ToList<XElement>();

                for(int i=0;i<mobList.Count;i++)
                {
                    mobChoices.Add(new MobChoice(mobList[i].Value, Convert.ToInt32(mobList[i].Attribute("rate").Value, Globals.culture)));

                }
            }
        }


        public virtual void SpawnMob()
        {
            GameGlobals.PassMob(new Guard(new Vector2(pos.X, pos.Y), new Vector2(1, 1), ownerId, level));
        }

        public override void Draw(Vector2 OFFSET)
        {
            Globals.normalEffect.Parameters["xSize"].SetValue((float)myModel.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)myModel.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dims.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dims.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(OFFSET);
        }
    }
}
