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
    public class TextZone
    {
        public int maxWidth, lineHeight;
        string str;
        public Vector2 pos, dims;
        public Color color;

        public SpriteFont font;

        public List<string> lines = new List<string>();

        public TextZone(Vector2 POS, string STR, int MAXWIDTH, int LINEHEIGHT, string FONT, Color FONTCOLOR)
        {
            pos = POS;
            str = STR;


            maxWidth = MAXWIDTH;
            lineHeight = LINEHEIGHT;
            color = FONTCOLOR;

            font = Globals.content.Load<SpriteFont>(FONT);

            if (str != "")
            {
                ParseLines();
            }
        }

        public string Str
        {
            get
            {
                return str;
            }

            set
            {
                str = value;
                ParseLines();
            }
        }

     
        public virtual void ParseLines()
        {
            lines.Clear();

            List<string> wordList = new List<string>();
            string tempString = "";

            int largestWidth = 0, currentWdith = 0;

            if (str != "" && str != null)
            {
                wordList = str.Split(' ').ToList<string>();

                for (int i = 0; i < wordList.Count; i++)
                {
                    if (tempString != "")
                    {
                        tempString += " ";
                    }

                    currentWdith = (int)(font.MeasureString(tempString + wordList[i]).X);

                    if (currentWdith > largestWidth && currentWdith <= maxWidth)
                    {
                        largestWidth = currentWdith;
                    }

                    if (currentWdith <= maxWidth)
                    {
                        tempString += wordList[i];
                    }
                    else
                    {
                        lines.Add(tempString);

                        tempString = wordList[i];
                    }

                }

                if (tempString != "")
                {
                    lines.Add(tempString);
                }

                SetDims(largestWidth);
            }
        }

        public virtual void SetDims(int LARGESTWIDTH)
        {
            dims = new Vector2(LARGESTWIDTH, lineHeight * lines.Count);
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Globals.spriteBatch.DrawString(font, lines[i], OFFSET + new Vector2(pos.X, pos.Y + (lineHeight * i)), color);
            }
        }
    }
}

