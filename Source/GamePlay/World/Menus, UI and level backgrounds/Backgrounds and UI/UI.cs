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
    public class UI
    {
        public Basic2d pauseOverlay;

        public Button2d resetBtn, skillMenuBtn, missionMenuBtn, characterMenuBtn, inventoryMenuBtn;

        public SkillMenu skillMenu;
        public MissionsMenu missionsMenu;
        public CharacterMenu characterMenu;
        public InventoryMenu inventoryMenu;

        public SpriteFont font;
        public SpriteFont font1;

        public QuantityDisplayBar healthBar;
        public QuantityDisplayBar staminaBar;
        public QuantityDisplayBar levelBar;

        public UI(PassObject RESET, Escapee PRISONER, MissionsMenu MISSIONSMENU, CharacterMenu CHARACTERMENU, InventoryMenu INVENTORYMENU)
        {
            pauseOverlay = new Basic2d("2d\\Misc\\PauseOverlay", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(300, 300));

            missionsMenu = MISSIONSMENU;
            characterMenu = CHARACTERMENU;
            inventoryMenu = INVENTORYMENU;

            font = Globals.content.Load<SpriteFont>("Fonts\\Arial24");
            font1 = Globals.content.Load<SpriteFont>("Fonts\\Lindsey15");

            resetBtn = new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(96, 32), new Vector2(1, 1), "Fonts\\Arial16", "Reset", RESET, null);


            skillMenuBtn = new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(106, 35), new Vector2(1, 1), "Fonts\\Arial16", "Skills", ToggleSkillMenu, null);
            skillMenu = new SkillMenu(PRISONER);
            missionMenuBtn = new Button2d("2d/Misc/SimpleBtn", new Vector2(0, 0), new Vector2(106, 35), new Vector2(1, 1), "Fonts\\Arial16", "Missions", ToggleMissionsMenu, null);
            characterMenuBtn = new Button2d("2d/Misc/SimpleBtn", new Vector2(0, 0), new Vector2(106, 35), new Vector2(1, 1), "Fonts\\Arial16", "Character", ToggleCharacterMenu, null);
            inventoryMenuBtn = new Button2d("2d/Misc/SimpleBtn", new Vector2(0, 0), new Vector2(106, 35), new Vector2(1, 1), "Fonts\\Arial16", "Inventory", ToggleInventoryMenu, null);

            healthBar = new QuantityDisplayBar(new Vector2(380, 30), 2, Color.Red);
            staminaBar = new QuantityDisplayBar(new Vector2(380, 30), 2, Color.LightBlue);
            levelBar = new QuantityDisplayBar(new Vector2(380, 30), 2, Color.LightCoral);
        }

        public void Update(World WORLD)
        {
            healthBar.Update(WORLD.user.prisoner.health, WORLD.user.prisoner.healthMax);
            staminaBar.Update(WORLD.user.prisoner.stamina, WORLD.user.prisoner.staminaMax);
            levelBar.Update(GameGlobals.score, GameGlobals.requiredScore);

            if (WORLD.user.prisoner.dead)
            {
                resetBtn.Update(new Vector2(Globals.screenWidth/2, Globals.screenHeight/2 + 100));

            }
            skillMenuBtn.Update(new Vector2(Globals.screenWidth - 70, Globals.screenHeight - 30));
            missionMenuBtn.Update(new Vector2(Globals.screenWidth - 190, Globals.screenHeight - 30));
            characterMenuBtn.Update(new Vector2(Globals.screenWidth - 310, Globals.screenHeight - 30));
            inventoryMenuBtn.Update(new Vector2(Globals.screenWidth - 430, Globals.screenHeight - 30));
            skillMenu.Update();
        }

        public virtual void ToggleSkillMenu(object INFO)
        {
            skillMenu.ToggleActive();
        }

        public virtual void ToggleMissionsMenu(object INFO)
        {
            missionsMenu.Active = true;
        }

        public virtual void ToggleCharacterMenu(object INFO)
        {
            characterMenu.Active = true;
        }

        public virtual void ToggleInventoryMenu(object INFO)
        {
            inventoryMenu.Active = true;
        }
        


        public void Draw(World WORLD)
        {
            Globals.normalEffect.Parameters["xSize"].SetValue(1.0f);
            Globals.normalEffect.Parameters["ySize"].SetValue(1.0f);
            Globals.normalEffect.Parameters["xDraw"].SetValue(1.0f);
            Globals.normalEffect.Parameters["yDraw"].SetValue(1.0f);
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            healthBar.Draw(new Vector2(1205, 10));
            staminaBar.Draw(new Vector2(1205, 50));
            levelBar.Draw(new Vector2(1205, 90));

            // Level 
            if (5 * GameGlobals.level == GameGlobals.score)
            {
                GameGlobals.level++;
                GameGlobals.requiredScore = GameGlobals.level * 5;
                GameGlobals.score = 0;
            }

            string tempStr = "Level " + GameGlobals.level + " | Score: " + GameGlobals.score + "/ " + GameGlobals.requiredScore + " |";
            Vector2 strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font1, tempStr, new Vector2(1300, 90), Color.DarkBlue);           

            tempStr = "Health: " + WORLD.user.prisoner.health;
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font1, tempStr, new Vector2(1350, 10), Color.DarkBlue);

            tempStr = "Stamina: " + WORLD.user.prisoner.stamina;
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font1, tempStr, new Vector2(1345, 50), Color.DarkBlue);
         
            tempStr = "Wanted Level:" + WORLD.user.prisoner.wantedLevel;
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(1335, 125), Color.DarkBlue);
            

            if (WORLD.user.prisoner.dead)
            {
                tempStr = "Press Enter or click the button to Restart!";
                strDims = font.MeasureString(tempStr);
                Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth/2 - strDims.X/2, Globals.screenHeight/2), Color.Black);

                resetBtn.Draw(new Vector2(Globals.screenWidth/2, Globals.screenHeight/2 + 100));
            }

            skillMenuBtn.Draw(new Vector2(Globals.screenWidth - 70, Globals.screenHeight - 30));
            missionMenuBtn.Draw(new Vector2(Globals.screenWidth - 190, Globals.screenHeight - 30));
            characterMenuBtn.Draw(new Vector2(Globals.screenWidth - 310, Globals.screenHeight - 30));
            inventoryMenuBtn.Draw(new Vector2(Globals.screenWidth - 430, Globals.screenHeight - 30));
            skillMenu.Draw();

            if (GameGlobals.paused)
            {
                pauseOverlay.Draw(Vector2.Zero);
            }
        }
    }
}
