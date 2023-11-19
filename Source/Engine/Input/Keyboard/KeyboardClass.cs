﻿#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
#endregion

namespace TopDownShooter
{
    public class KeyBoardClass
    {

        public KeyboardState newKeyboard, oldKeyboard;

        public List<KeyClass> pressedKeys = new List<KeyClass>(), previousPressedKeys = new List<KeyClass>();

        public KeyBoardClass()
        {

        }

        public virtual void Update()
        {
            newKeyboard = Keyboard.GetState();

            GetPressedKeys();

        }

        public void UpdateOld()
        {
            oldKeyboard = newKeyboard;

            previousPressedKeys = new List<KeyClass>();
            for (int i = 0; i < pressedKeys.Count; i++)
            {
                previousPressedKeys.Add(pressedKeys[i]);
            }
        }


        public bool GetPress(string KEY)
        {

            for (int i = 0; i < pressedKeys.Count; i++)
            {

                if (pressedKeys[i].key == KEY)
                {
                    return true;
                }

            }


            return false;
        }


        public virtual void GetPressedKeys()
        {
            pressedKeys.Clear();
            for (int i = 0; i < newKeyboard.GetPressedKeys().Length; i++)
            {

                pressedKeys.Add(new KeyClass(newKeyboard.GetPressedKeys()[i].ToString(), 1));

            }
        }

        public bool GetSinglePress(string KEY)
        {

            for (int i = 0; i < pressedKeys.Count; i++)
            {
                bool isIn = false;

                for (int j = 0; j < previousPressedKeys.Count; j++)
                {
                    if (pressedKeys[i].key == previousPressedKeys[j].key)
                    {
                        isIn = true;
                        break;
                    }
                }


                if (!isIn && (pressedKeys[i].key == KEY || pressedKeys[i].print == KEY))
                {
                    return true;
                }
            }



            return false;
        }

    }
}
