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
    public class World
    {
        public Vector2 offset;

        public int levelid;
        public int winconfirmed1;

        public MissionsMenu MissionsMenu;
        public CharacterMenu CharacterMenu;
        public InventoryMenu InventoryMenu;
        public ExitMenu exitMenu;

        public UI ui;

        public User user;
        public Backgrounds background;
        public AIPlayer aIPlayer;
        public MapMenu mapMenu;

        public SquareGrid grid;

        public List<Projectile2d> projectiles = new List<Projectile2d>();
        public List<Effect2d> effects = new List<Effect2d>();
        public List<AttackableObject> allObjects = new List<AttackableObject>();
        public List<LootBag> lootBags = new List<LootBag>();
        public List<Key> keys = new List<Key>();

        PassObject ResetWorld, ChangeGameState, ChangePlayState;

        public World(PassObject RESETWORLD, int LEVELID, PassObject CHANGEGAMESTATE, PassObject CHANGEPLAYSTATE, MapMenu MAPMENU)
        {
            levelid = LEVELID;

            mapMenu = MAPMENU;

            winconfirmed1 = 0;

            ResetWorld = RESETWORLD;
            ChangeGameState = CHANGEGAMESTATE;
            ChangePlayState = CHANGEPLAYSTATE;

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassEffect = AddEffect;
            GameGlobals.PassMob = AddMob;
            GameGlobals.PassSpawnPoint = AddSpawnPoint;
            // GameGlobals.CheckScroll = CheckScroll;
            GameGlobals.PassGold = AddGold;
            GameGlobals.PassLootBag = AddLoot;
            GameGlobals.PassKey = AddKey;
            GameGlobals.PassBuilding = AddBuilding;

            GameGlobals.paused = false;

            offset = new Vector2(0, 0);

            LoadData(levelid);

            MissionsMenu = new MissionsMenu(user.prisoner, LEVELID);
            InventoryMenu = new InventoryMenu(user.prisoner);
            CharacterMenu = new CharacterMenu(user.prisoner);
            exitMenu = new ExitMenu(ChangePlayState);

            MissionsMenu.Active = true;

            ui = new UI(ResetWorld, user.prisoner, MissionsMenu, CharacterMenu, InventoryMenu);
        }

        public virtual void Update()
        {
            ui.Update(this);

            if (!NoUpdate())
            {
                allObjects.Clear();
                allObjects.AddRange(user.GetAllObjects());
                allObjects.AddRange(aIPlayer.GetAllObjects());

                user.Update(aIPlayer, offset, grid, levelid);
                aIPlayer.Update(user, offset, grid, levelid);

                for(int i=0; i<lootBags.Count; i++)
                {
                    lootBags[i].Update(offset);

                    if (lootBags[i].done)
                    {
                        lootBags.RemoveAt(i);
                        i--;
                    }
                }

                for (int i = 0; i < keys.Count; i++)
                {
                    keys[i].Update(offset);

                    if (keys[i].done)
                    {
                        keys.RemoveAt(i);
                        i--;
                    }
                }

                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Update(offset, allObjects);

                    if (projectiles[i].done)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }

                for (int i = 0; i < effects.Count; i++)
                {
                    effects[i].Update(offset);

                    if (effects[i].done)
                    {
                        effects.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
                if (Globals.keyboard.GetPress("Enter") && (user.prisoner.dead))
                {
                    ResetWorld(null);
                }
            }

            MissionsMenu.Update();
            InventoryMenu.Update();
            CharacterMenu.Update();
            exitMenu.Update();

            if (grid != null)
            {
                grid.Update(offset);
            }

            if (Globals.keyboard.GetSinglePress("Back"))
            {
                ResetWorld(null);
                ChangeGameState(0);
                
            }

            if (Globals.keyboard.GetSinglePress("Space"))
            {
                GameGlobals.paused = !GameGlobals.paused;
            }
            if (Globals.keyboard.GetSinglePress("Escape"))
            {
                exitMenu.Active = !exitMenu.Active;
                MissionsMenu.Active = false;
                InventoryMenu.Active = false;
                CharacterMenu.Active = false;
            }

            if (Globals.keyboard.GetSinglePress("G"))
            {
                grid.showGrid = !grid.showGrid;
            }
            if (Globals.keyboard.GetSinglePress("Q"))
            {
                MissionsMenu.Active = true;
                exitMenu.Active = false;
                InventoryMenu.Active = false;
                CharacterMenu.Active = false;
            }
            if(Globals.keyboard.GetSinglePress("I"))
            {
                InventoryMenu.Active = true;
                exitMenu.Active = false;
                MissionsMenu.Active = false;
                CharacterMenu.Active = false;
            }
            if(Globals.keyboard.GetSinglePress("C"))
            {
                CharacterMenu.Active = true;
                InventoryMenu.Active = false;
                exitMenu.Active = false;
                MissionsMenu.Active = false;
            }
            if(aIPlayer.defeated)
            {
                Globals.msgList.Add(new ConditionMessage(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(250, 110), "Level Completed", Color.Black, true, WinConfirmed));
                GameGlobals.WinConfirmed++;
            }
            if(GameGlobals.WinConfirmed == 1)
            {
                GameGlobals.CompletedLevels = 1;
                mapMenu.LoadData();
            }
            if (GameGlobals.WinConfirmed == 2)
            {
                GameGlobals.CompletedLevels = 2;
                mapMenu.LoadData();
            }
            if (GameGlobals.WinConfirmed == 3)
            {
                GameGlobals.CompletedLevels = 3;
                mapMenu.LoadData();
            }
            if (GameGlobals.WinConfirmed == 4)
            {
                GameGlobals.CompletedLevels = 4;
                mapMenu.LoadData();
            }
            if (GameGlobals.WinConfirmed == 5)
            {
                GameGlobals.CompletedLevels = 5;
                mapMenu.LoadData();
            }
            if (GameGlobals.WinConfirmed == 6)
            {
                GameGlobals.CompletedLevels = 6;
                mapMenu.LoadData();
            }
        }
            
        public virtual void AddEffect(object INFO)
        {
            effects.Add((Effect2d)INFO);
        }


        public virtual void AddGold(object INFO)
        {
            PlayerValuePacket packet = (PlayerValuePacket)INFO;

            if(user.id == packet.playerId)
            {
                user.gold += (int)packet.value;
            }
            else if(aIPlayer.id == packet.playerId)
            {
                aIPlayer.gold += (int)packet.value;
            }
        }

        public virtual void AddLoot(object INFO)
        {
            LootBag tempBags = (LootBag)INFO;

            lootBags.Add(tempBags);


        }

        public virtual void AddKey(object INFO)
        {
            Key tempKeys = (Key)INFO;

            keys.Add(tempKeys);
        }

        public virtual void AddBuilding(object INFO)
        {
            Building tempBuilding = (Building)INFO;

            if (user.id == tempBuilding.ownerId)
            {
                user.AddBuilding(tempBuilding);
            }
            else if (aIPlayer.id == tempBuilding.ownerId)
            {
                aIPlayer.AddBuilding(tempBuilding);
            }
        }

        public virtual void AddMob(object INFO)
        {
            Unit tempUnit = (Unit)INFO;

            if(user.id == tempUnit.ownerId)
            {
                user.AddUnit(tempUnit);
            }
            else if(aIPlayer.id == tempUnit.ownerId)
            {
                aIPlayer.AddUnit(tempUnit);
            }
        }



        public virtual void AddProjectile(object INFO)
        {
            projectiles.Add((Projectile2d)INFO);
        }

        

        public virtual void AddSpawnPoint(object INFO)
        {
            SpawnPoint tempSpawnPoint = (SpawnPoint)INFO;

            if(user.id == tempSpawnPoint.ownerId)
            {
                user.AddSpawnPoint(tempSpawnPoint);
            }
            else if(aIPlayer.id == tempSpawnPoint.ownerId)
            {
                aIPlayer.AddSpawnPoint(tempSpawnPoint);
            }

        }

        public virtual bool NoUpdate()
        {
            if(user.prisoner.dead || GameGlobals.paused || MissionsMenu.Active || exitMenu.Active || InventoryMenu.Active || CharacterMenu.Active || ui.skillMenu.active)
            {
                return true;
            }

            return false;
        }

        public virtual void LoadData(int LEVEL)
        {
            XDocument xml = XDocument.Load("XML\\Levels\\Level"+LEVEL+".xml");

            XElement tempElement = null;
            if(xml.Element("Root").Element("User") != null)
            {
                tempElement = xml.Element("Root").Element("User");
            }

            user = new User(1, tempElement, LEVEL);

            if(user.prisoner != null)
            {
                GameGlobals.AddToInventory = user.prisoner.AddToInventory;
            }

            tempElement = null;
            if(xml.Element("Root").Element("AIPlayer") != null)
            {
                tempElement = xml.Element("Root").Element("AIPlayer");
            }

            XElement tempBackground = null;
            if(xml.Element("Root").Element("Background") != null)
            {
                tempBackground = xml.Element("Root").Element("Background");
            }

            background = new Backgrounds(tempBackground);

            grid = new SquareGrid(new Vector2(25, 25), new Vector2(-100, -100), new Vector2(Globals.screenWidth + 200, Globals.screenHeight + 200), xml.Element("Root").Element("GridItems"));

            

            aIPlayer = new AIPlayer(2, tempElement, levelid);        
        }

        public virtual void WinConfirmed(object INFO)
        {       
            ResetWorld(null);
            ChangePlayState(1);
        }

        public virtual void Draw(Vector2 OFFSET)
        {            
            grid.DrawGrid(offset);

            for(int i=0; i<lootBags.Count; i++)
            {
                lootBags[i].Draw(offset);
            }
            for (int i = 0; i < keys.Count; i++)
            {
                keys[i].Draw(offset);
            }

            user.Draw(offset);
            aIPlayer.Draw(offset);


         
            /*for(int i=0; i<sceneItems.Count; i++)
            {
                sceneItems[i].Draw(offset);
            }*/
            
            for(int i=0;i<projectiles.Count;i++)
            {
                projectiles[i].Draw(offset);
            }


            for(int i=0;i<effects.Count;i++)
            {
                effects[i].Draw(offset);
            }

            ui.Draw(this);

            MissionsMenu.Draw();
            InventoryMenu.Draw();
            exitMenu.Draw();
            CharacterMenu.Draw();
        }
    }
}
