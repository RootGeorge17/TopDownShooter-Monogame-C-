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
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main: Game
    {
        bool lockUpdate;

        GraphicsDeviceManager graphics;

        GamePlay gamePlay;
        MainMenu mainMenu;

        public Texture2D background;

        Basic2d cursor;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            lockUpdate = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Globals.screenWidth = 1600; //1600
            Globals.screenHeight = 900; //900

            graphics.PreferredBackBufferWidth = Globals.screenWidth;
            graphics.PreferredBackBufferHeight = Globals.screenHeight;

            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Globals.content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
            cursor = new Basic2d("2d\\Misc\\CursorArrow", new Vector2(0, 0), new Vector2(28, 28));
            Globals.background = Content.Load<Texture2D>("2d/Misc/WorldMap"); // background

            Globals.normalEffect = Globals.content.Load<Effect>("2d/Effects/Normal");

            Globals.keyboard = new KeyBoardClass();
            Globals.mouse = new MouseClass();

            mainMenu = new MainMenu(ChangeGameState, ExitGame);
            gamePlay = new GamePlay(ChangeGameState);

            Globals.dragAndDropPacket = new DragAndDropPacket(new Vector2(40, 40));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
           if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
               Exit();

            Globals.gameTime = gameTime;
            Globals.keyboard.Update();
            Globals.mouse.Update();

            lockUpdate = false;
            for(int i=0; i<Globals.msgList.Count; i++)
            {
                Globals.msgList[i].Update();
                if(!Globals.msgList[i].done)
                {
                    if(Globals.msgList[i].lockScreen)
                    {
                        lockUpdate = true;
                    }
                }
                else
                {
                    Globals.msgList.RemoveAt(i);
                    i--;
                }

            }


            if(!lockUpdate)
            {
                if (Globals.gameState == 0)
                {
                    mainMenu.Update();
                }
                else if (Globals.gameState == 1)
                {
                    gamePlay.Update();
                }
            }

            if (Globals.dragAndDropPacket != null)
            {
                Globals.dragAndDropPacket.Update();
            }



            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();

            base.Update(gameTime);
        }

        public virtual void ChangeGameState(object INFO)
        {
            Globals.gameState = Convert.ToInt32(INFO, Globals.culture);
        }

        public virtual void ExitGame(object INFO)
        {
            System.Environment.Exit(0);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);


            if(Globals.gameState == 0)
            {
                mainMenu.Draw();
            }
            else if(Globals.gameState == 1)
            {
                Globals.spriteBatch.Draw(Globals.background, new Rectangle(0, 0, 1600, 900), Color.White); // background
                gamePlay.Draw();
            }

            Globals.normalEffect.Parameters["xSize"].SetValue((float)cursor.myModel.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)cursor.myModel.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)cursor.dims.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)cursor.dims.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();


            if (Globals.dragAndDropPacket != null)
            {
                Globals.dragAndDropPacket.Draw();
            }

            for(int i=0; i<Globals.msgList.Count; i++)
            {
                Globals.msgList[i].Draw();
            }

            cursor.Draw(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), new Vector2(0, 0), Color.White);
            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }

#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            using(var game = new Main())
                game.Run();
        }
    }
#endif
}
