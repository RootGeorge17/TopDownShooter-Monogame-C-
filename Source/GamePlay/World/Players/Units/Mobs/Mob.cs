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
    public class Mob : Unit
    {
        public bool currentlyPathing;
        public bool isAttacking;

        public float attackRange;

        public bool hit;
        public Vector2 difference;

        public int level;
        public int runOnce = 0;


        public TimerClass rePathTimer = new TimerClass(200), attackTimer = new TimerClass(350);
        public TimerClass ShootTimer = new TimerClass(1000);

        public Mob(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID, int LEVEL)
            : base(PATH, POS, DIMS, FRAMES, OWNERID)
        {
            attackRange = 50;
            level = LEVEL;
            runOnce++;
            
            isAttacking = false;

            currentlyPathing = false;

            speed = 2.0f;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY, SquareGrid GRID)
        {
            AI(ENEMY, GRID);

            base.Update(OFFSET, ENEMY, GRID);
        }


        public virtual void AI(Player ENEMY, SquareGrid GRID)
        {
            rePathTimer.UpdateTimer();
            ShootTimer.UpdateTimer();

            if (pathNodes == null || (pathNodes.Count == 0 && pos.X == moveTo.X && pos.Y == moveTo.Y) || rePathTimer.Test())
            {
                if (!currentlyPathing)
                {

                    Task repathTask = new Task(() =>
                    {
                        currentlyPathing = true;

                        pathNodes = FindPath(GRID, GRID.GetSlotFromPixel(ENEMY.prisoner.pos, Vector2.Zero));
                        moveTo = pathNodes[0];
                        pathNodes.RemoveAt(0);

                        rePathTimer.ResetToZero();

                        currentlyPathing = false;
                    });

                    repathTask.Start();


                }
            }
            else
            {
                MoveUnit();

                while (Globals.GetDistance(pos, ENEMY.prisoner.pos) < GRID.slotDims.X * 1)
                {
                    difference = ENEMY.prisoner.pos - pos;
                    pos += -difference;
                    hit = true;
                    if (ShootTimer.Test() && hit == true)
                    {
                        ShootTimer.ResetToZero();
                        ENEMY.prisoner.GetHit(this, 1);
                        hit = false;
                    }
                    break;
                }
            }
        }


        public override void GetHit(AttackableObject ATTACKER, float DAMAGE)
        {
            health -= DAMAGE;

            if (health <= 0)
            {
                dead = true;

                GameGlobals.PassGold(new PlayerValuePacket(ATTACKER.ownerId, killValue));

                int num = Globals.rand.Next(0, 2 + 1);
                GameGlobals.killConfirmed++;

                if (num == 0)
                {
                    LootBag tempBag = new LootBag("2d/Loot/LootBag", new Vector2(pos.X, pos.Y), null);
                    tempBag.items.Add(new TestItem());

                    GameGlobals.PassLootBag(tempBag);

                }

                if(level == 1)
                {
                    if (GameGlobals.killConfirmed == 10)
                    {
                        Key tempKey = new Key("2d/Loot/Items/key", new Vector2(pos.X, pos.Y), null);
                        tempKey.items.Add(new KeyDrop());

                        GameGlobals.PassKey(tempKey);
                    }
                }
                if (level == 2)
                {
                    if (GameGlobals.killConfirmed == 20)
                    {
                        Key tempKey = new Key("2d/Loot/Items/key", new Vector2(pos.X, pos.Y), null);
                        tempKey.items.Add(new KeyDrop());

                        GameGlobals.PassKey(tempKey);
                    }
                }
                if (level == 3)
                {
                    if (GameGlobals.killConfirmed == 30)
                    {
                        Key tempKey = new Key("2d/Loot/Items/key", new Vector2(pos.X, pos.Y), null);
                        tempKey.items.Add(new KeyDrop());

                        GameGlobals.PassKey(tempKey);
                    }
                }
                if (level == 4)
                {
                    if (GameGlobals.killConfirmed == 40)
                    {
                        Key tempKey = new Key("2d/Loot/Items/key", new Vector2(pos.X, pos.Y), null);
                        tempKey.items.Add(new KeyDrop());

                        GameGlobals.PassKey(tempKey);
                    }
                }
                if (level == 5)
                {
                    if (GameGlobals.killConfirmed == 50)
                    {
                        Key tempKey = new Key("2d/Loot/Items/key", new Vector2(pos.X, pos.Y), null);
                        tempKey.items.Add(new KeyDrop());

                        GameGlobals.PassKey(tempKey);
                    }
                }
                if (level == 6)
                {
                    if (GameGlobals.killConfirmed == 60)
                    {
                        Key tempKey = new Key("2d/Loot/Items/key", new Vector2(pos.X, pos.Y), null);
                        tempKey.items.Add(new KeyDrop());

                        GameGlobals.PassKey(tempKey);
                    }
                }
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
