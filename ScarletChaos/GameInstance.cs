using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ScarletChaos.DataUtility;
using ScarletChaos.Entities;
using ScarletChaos.Networking;
using ScarletResource;
using System.Collections.Generic;
using System.IO;

namespace ScarletChaos
{
    public class GameInstance : Game
    {
        public static GameInstance PrimaryGameInstance;
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public GraphicsOptions OptionsGraphics;
        public PlayerOptions OptionsPlayer;
        public List<Entity> EntityList = new List<Entity>();
        public TextureContent texturePipeline;
        public OnlineSession Session;

        public Camera GameCam;

        public static string GameDirectory = Directory.GetCurrentDirectory();

        public GameInstance()
        {
            DebugLog.LogInfo("Game Instance has been Initialized.");
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            PrimaryGameInstance = this;

            OptionsGraphics = new GraphicsOptions(graphics);
            OptionsGraphics.LoadGraphicsOptions();
            OptionsGraphics.ApplyGraphicOptions();
            OptionsGraphics.SaveGraphicsOptions();

            GameCam = new Camera(new Viewport(0, 0, ScreenSize.SS_169_1920X1080.Width, ScreenSize.SS_169_1920X1080.Height, -1000, 1000));

            OptionsPlayer = new PlayerOptions();

            DebugLog.LogInfo("Game Instance constructor loaded.");
        }

        public static bool IsOnline()
        {
            if (PrimaryGameInstance.Session == null) return false;

            return true;
        }
        public static bool IsHost()
        {
            if (PrimaryGameInstance.Session == null) return true;
            if (PrimaryGameInstance.Session.IsHost == true) return true;
            return false;
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            DebugLog.LogInfo("Main initialization.");
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load Content.
        /// </summary>
        protected override void LoadContent()
        {
            DebugLog.LogInfo("Creating ContentLoaders.");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texturePipeline = new TextureContent(GraphicsDevice);

            what = texturePipeline.solidAnimations.TEST_LOAD; //TODO: Shit


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            DebugLog.LogInfo("Flushed all Textures and unloaded all content.");
            // TODO: Unload any non ContentManager content here
            texturePipeline.FlushAllTextures();
        }


        public static double Delta1s = 0;
        public static double Delta1 = 0;
        public static double Delta10 = 0;
        public static double Delta30 = 0;
        public static double Delta60 = 0;
        public static double Delta120 = 0;

        public static double StepTime;
        public static double DrawTime;

        Sprite what; //TODO: Remove

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            var deltaTime = gameTime.ElapsedGameTime;
            DrawTime = deltaTime.Milliseconds * 0.001;

            //Roll up shit
            GameCam.Update();
            spriteBatch.GraphicsDevice.Viewport = GameCam.MainView;
            spriteBatch.Begin(SpriteSortMode.BackToFront);

            what.DrawAnimation(spriteBatch, new Vector2(200, 200), 0);

            Entity[] list = EntityList.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Visible == true)
                    list[i].Draw(spriteBatch);

                if (list[i].Sprite != null)
                    list[i].Sprite.Update(gameTime);
            }

            what.Update(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Q: UrrDurridurr why no pure delta timer fred? 
        // A: Delta timers are bad for platformers where pixel perfect jumps is a thing. Atleast the Draw event and StepRaw use Delta timers.
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //StepTime  is technically a second
            var deltaTime = gameTime.ElapsedGameTime;
            StepTime = (deltaTime.Milliseconds * 0.001);
            Delta1s += (StepTime) * 0.01;
            Delta1 += (StepTime) * 1;
            Delta10 += (StepTime) * 10;
            Delta30 += (StepTime) * 30;
            Delta60 += (StepTime) * 60;
            Delta120 += (StepTime) * 120;

            while (Delta1s > 1) { Step1s(); }
            while (Delta1 > 1) { Step1(); }
            while (Delta10 > 1) { Step10(); }
            while (Delta30 > 1) { Step30(); }
            while (Delta60 > 1) { Step60(); }
            while (Delta120 > 1) { Step120(); }

            Entity[] list = EntityList.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                list[i].StepRaw();
            }


            base.Update(gameTime);
        }
        private void Step120()
        {
            Delta120 -= 1;
            Entity[] list = EntityList.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Active == true)
                    list[i].Step120();
            }
            //Always update this last, that way it will be ready for the next step.
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Active == true)
                    list[i].UpdateEntityData();
            }

            //TODO: Debug
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                var e = new EntityPlayer();
                e.SetLocation(GetMouseLocation());
                e.Sprite = texturePipeline.solidAnimations.TEST_LOAD;
                e.Visible = true;
            }

        }
        private void Step60()
        {
            Delta60 -= 1;
            Entity[] list = EntityList.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Active == true)
                    list[i].Step60();
            }
        }
        private void Step30()
        {
            Delta30 -= 1;
            Entity[] list = EntityList.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Active == true)
                    list[i].Step30();
            }

        }
        private void Step10()
        {
            Delta10 -= 1;
            Entity[] list = EntityList.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Active == true)
                    list[i].Step10();
            }

        }
        private void Step1()
        {
            Delta1 -= 1;
            Entity[] list = EntityList.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Active == true)
                    list[i].Step1();
            }
        }
        private void Step1s()
        {
            Delta1s -= 1;
            Entity[] list = EntityList.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Active == true)
                    list[i].Step1s();
            }
        }


        public static Vector2 GetMouseLocation()
        {
            Vector2 loc = new Vector2(0, 0)
            {
                X = Mouse.GetState().X * ((float)PrimaryGameInstance.GameCam.ViewW / PrimaryGameInstance.OptionsGraphics.ScreenResolution.Width),
                Y = Mouse.GetState().Y * ((float)PrimaryGameInstance.GameCam.ViewH / PrimaryGameInstance.OptionsGraphics.ScreenResolution.Height)
            };
            return loc;
        }





        private static ulong NextEntityID = 0;
        public static ulong GetNextEntityID()
        {
            NextEntityID += 1;
            return NextEntityID;
        }


    }
}
