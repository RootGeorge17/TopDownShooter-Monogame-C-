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
using System.Xml.Serialization;
#endregion

namespace TopDownShooter
{
    public class MapMenu
    {
        public Basic2d bkg;
        public List<Button2d> levels = new List<Button2d>();

        PassObject ChangeGameState;

        List<XElement> levelList;

        public MapMenu(PassObject CHANGEGAMESTATE)
        {
            ChangeGameState = CHANGEGAMESTATE;

            bkg = new Basic2d("2d//UI//Backgrounds//WorldMap", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(Globals.screenWidth, Globals.screenHeight));

            LoadData();
        }

        public virtual void Update()
        {
            for (int i = 0; i < levels.Count; i++)
            {
                levels[i].Update(Vector2.Zero);            
            }
        }

        public virtual void LevelClicked(object INFO)
        {
            ChangeGameState(INFO);
        }

        public virtual void LoadData()
        {
            XDocument xml = XDocument.Load("XML/Levels/Levels.xml");

            levelList = (from t in xml.Descendants("Level")
                                        select t).ToList<XElement>();

            levels.Clear();
            for (int i = 0; i < levelList.Count; i++)
            {
                levels.Add(new Button2d("2d//Misc//solid", new Vector2(Convert.ToInt32(levelList[0].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[0].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(40, 40), new Vector2(1, 1), "fonts//Arial16", levelList[0].Attribute("id").Value, LevelClicked, levelList[0].Attribute("id").Value));

                if (GameGlobals.WinConfirmed == 1)
                {
                    levels.Add(new Button2d("2d//Misc//solid", new Vector2(Convert.ToInt32(levelList[1].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[1].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(40, 40), new Vector2(1, 1), "fonts//Arial16", levelList[1].Attribute("id").Value, LevelClicked, levelList[1].Attribute("id").Value));
                    levels.RemoveAt(levels.Count - 2);
                }
                if (GameGlobals.WinConfirmed == 2)
                {
                    levels.Add(new Button2d("2d//Misc//solid", new Vector2(Convert.ToInt32(levelList[2].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[2].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(40, 40), new Vector2(1, 1), "fonts//Arial16", levelList[2].Attribute("id").Value, LevelClicked, levelList[2].Attribute("id").Value));
                    levels.RemoveAt(levels.Count - 2);
                }
                if (GameGlobals.WinConfirmed == 3)
                {
                    levels.Add(new Button2d("2d//Misc//solid", new Vector2(Convert.ToInt32(levelList[3].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[3].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(40, 40), new Vector2(1, 1), "fonts//Arial16", levelList[3].Attribute("id").Value, LevelClicked, levelList[3].Attribute("id").Value));
                    levels.RemoveAt(levels.Count - 2);
                }
                if (GameGlobals.WinConfirmed == 4)
                {
                    levels.Add(new Button2d("2d//Misc//solid", new Vector2(Convert.ToInt32(levelList[4].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[4].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(40, 40), new Vector2(1, 1), "fonts//Arial16", levelList[4].Attribute("id").Value, LevelClicked, levelList[4].Attribute("id").Value));
                    levels.RemoveAt(levels.Count - 2);
                }
                if (GameGlobals.WinConfirmed == 5)
                {
                    levels.Add(new Button2d("2d//Misc//solid", new Vector2(Convert.ToInt32(levelList[5].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[5].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(40, 40), new Vector2(1, 1), "fonts//Arial16", levelList[5].Attribute("id").Value, LevelClicked, levelList[5].Attribute("id").Value));
                    levels.RemoveAt(levels.Count - 2);
                }
                if(GameGlobals.WinConfirmed == 6)
                {
                    levels.Add(new Button2d("2d//Misc//solid", new Vector2(Convert.ToInt32(levelList[0].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[0].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(40, 40), new Vector2(1, 1), "fonts//Arial16", levelList[0].Attribute("id").Value, LevelClicked, levelList[0].Attribute("id").Value));
                    levels.Add(new Button2d("2d//Misc//solid", new Vector2(Convert.ToInt32(levelList[1].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[1].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(40, 40), new Vector2(1, 1), "fonts//Arial16", levelList[1].Attribute("id").Value, LevelClicked, levelList[1].Attribute("id").Value));
                    levels.Add(new Button2d("2d//Misc//solid", new Vector2(Convert.ToInt32(levelList[2].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[2].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(40, 40), new Vector2(1, 1), "fonts//Arial16", levelList[2].Attribute("id").Value, LevelClicked, levelList[2].Attribute("id").Value));
                    levels.Add(new Button2d("2d//Misc//solid", new Vector2(Convert.ToInt32(levelList[3].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[3].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(40, 40), new Vector2(1, 1), "fonts//Arial16", levelList[3].Attribute("id").Value, LevelClicked, levelList[3].Attribute("id").Value));
                    levels.Add(new Button2d("2d//Misc//solid", new Vector2(Convert.ToInt32(levelList[4].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[4].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(40, 40), new Vector2(1, 1), "fonts//Arial16", levelList[4].Attribute("id").Value, LevelClicked, levelList[4].Attribute("id").Value));
                    levels.Add(new Button2d("2d//Misc//solid", new Vector2(Convert.ToInt32(levelList[5].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[5].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(40, 40), new Vector2(1, 1), "fonts//Arial16", levelList[5].Attribute("id").Value, LevelClicked, levelList[5].Attribute("id").Value));
                }
            }
        }

        public virtual void Draw()
        {
            bkg.Draw(Vector2.Zero);

            for (int i = 0; i < levels.Count; i++)
            {
                levels[i].Draw(Vector2.Zero);
            }
        }
    }
}
