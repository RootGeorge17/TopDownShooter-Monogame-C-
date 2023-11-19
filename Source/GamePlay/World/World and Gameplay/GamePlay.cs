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
    public class GamePlay
    {
        int playState;

        public World world;

        public MapMenu worldMap;

        PassObject ChangeGameState;


        public GamePlay(PassObject CHANGEGAMESTATE)
        {
            playState = 1;

            ChangeGameState = CHANGEGAMESTATE;
            ResetWorld(null);

            worldMap = new MapMenu(LoadLevel);
        }

        public virtual void Update()
        {
            if(playState == 0)
            {
                world.Update();
            }
            else if (playState == 1)
            {
                worldMap.Update();
            }
        }

        public virtual void ChangePlayState(object INFO)
        {
            playState = Convert.ToInt32(INFO, Globals.culture);
        }

        public virtual void LoadLevel(object INFO)
        {
            playState = 0;

            int tempLevel = Convert.ToInt32(INFO, Globals.culture);

            Globals.msgList.Add(new Message(new Vector2(Globals.screenWidth/2, 30), new Vector2(800, 240), "Level: " + tempLevel, 3700, Color.Black, false));

            world = new World(ResetWorld, tempLevel, ChangeGameState, ChangePlayState, worldMap);
        }

        public virtual void ResetWorld(object INFO)
        {
            int levelid = 1;

            if(world != null)
            {
                levelid = world.levelid;
            }

            world = new World(ResetWorld, levelid, ChangeGameState, ChangePlayState, worldMap);

        }

        public virtual void Draw()
        {
            if(playState == 0)
            {
                world.Draw(Vector2.Zero);
            }
            else if (playState == 1)
            {
                worldMap.Draw();
            }
        }
    }
}
