using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ScarletChaos.DataUtility;
using ScarletChaos.Entities;
using ScarletResource;
using System.Collections.Generic;
using System.IO;
using System;
using ScarletResource.TextureContents;
using ScarletResource.MapObjects;
using MonoGame.Framework;

namespace ScarletChaos
{
    public class GameInstance : Game
    {
        public static GameInstance PrimaryGameInstance;
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static GraphicsOptions OptionsGraphics;
        public static PlayerOptions OptionsPlayer;
        public static List<Entity> Entities = new List<Entity>();

        public static TextureContent TexturePipeline;
        public static FontContent FontPipeline;

        public static bool DebugDraw = true;
        public static Map CurrentMap = Map.CurrentMap;

        public static Camera GameCam = new Camera(new Viewport(0, 0, 1920, 1080, -1, 1));
        public static MouseState StateMouse = Mouse.GetState();
        public static MouseState StateMouseOld = Mouse.GetState();
        public static KeyboardState StateKeyboard = Keyboard.GetState();
        public static KeyboardState StateKeyboardOld = Keyboard.GetState();

        public static string GameDirectory = Directory.GetCurrentDirectory();

        public GameInstance()
        {
            Content.RootDirectory = PipeLine.ASSETS; //Fix that shit

            IsMouseVisible = true;
            
            DebugLog.LogInfo("Game Instance has been Initialized.");
            graphics = new GraphicsDeviceManager(this);

            PrimaryGameInstance = this;

            OptionsGraphics = new GraphicsOptions(graphics);
            OptionsGraphics.LoadGraphicsOptions();
            OptionsGraphics.ApplyGraphicOptions();
            OptionsGraphics.SaveGraphicsOptions();

            OptionsPlayer = new PlayerOptions();


            CurrentMap = new Map(); //Current map is static anyway.


            DebugLog.LogInfo("Game Instance constructor loaded.");
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
            // Create a new TexturePipeline that can be used to load textures
            TexturePipeline = new TextureContent(GraphicsDevice);
            FontPipeline = new FontContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            DebugLog.LogInfo("Flushed all Textures and unloaded all content.");
            // TODO: Unload any non ContentManager content here
            TextureContent.FlushAllTextures();
            FontContent.FlushAllFonts();
        }


        public static double Delta1s = 0;
        public static double Delta1 = 0;
        public static double Delta10 = 0;
        public static double Delta30 = 0;
        public static double Delta60 = 0;
        public static double Delta120 = 0;

        public static double StepTime;
        public static double DrawTime;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            var deltaTime = gameTime.ElapsedGameTime;
            DrawTime = deltaTime.Milliseconds * 0.001;

            //Roll up shit
            GameCam.Update();
            spriteBatch.GraphicsDevice.Viewport = GameCam.MainView;
            spriteBatch.Begin(SpriteSortMode.BackToFront);

            SpriteFont font = FontContent.GetFont(@"FontArial16");

            spriteBatch.DrawString(font, "I like big butts and i cannot lie.", new Vector2(32, 32), Color.White);

            Entity[] list = Entities.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Visible == true)
                    list[i].Draw(spriteBatch);

                if (list[i].Sprite != null)
                    list[i].Sprite.Update(gameTime);

                if (list[i].CollisionMask != null)
                    list[i].CollisionMask.Update(gameTime);
            }

            if (DebugDraw == true)
            {
                Solid[] solids = CurrentMap.Solids.ToArray();
                for (var j = 0; j < solids.Length; j++)
                {
                    solids[j].CollisionMask.Update(gameTime);
                    solids[j].CollisionMask.UpdateCollisionLocation();

                    if (solids[j].Visible == true || DebugDraw == true)
                        solids[j].Draw(spriteBatch);
                }
            }

            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Q: UrrDurridurr why no pure delta timer fred? 
        // A: Delta timers are bad for platformers where pixel perfect jumps is a thing. The Draw event and StepRaw use Delta timers.
        protected override void Update(GameTime gameTime)
        {
            StateMouseOld = StateMouse;
            StateMouse = Mouse.GetState();
            StateKeyboardOld = StateKeyboard;
            StateKeyboard = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (MouseRightButtonIsPressed())
            {
                CurrentMap.Solids.Add(new Solid(GetMouseLocation()));
                DebugLog.LogDebug("Created Solid at: (" + GetMouseLocation().X + "," + GetMouseLocation().Y + ")");
            }

            //StepTime  is technically a second when you screw it over like me
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

            Entity[] list = Entities.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                list[i].StepRaw();
            }

            base.Update(gameTime);
        }
        private void Step120()
        {
            Delta120 -= 1;
            Entity[] list = Entities.ToArray();
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
            if (StateMouse.LeftButton == ButtonState.Pressed && IsActive == true)
            {
                Entity e = (Entity)Activator.CreateInstance(Entity.GetEntityTypeFromID(Entity.ENTITY_PLAYER));
                e.SetLocation(GetMouseLocation());
                e.Sprite = EntitySprites.GetSprite("kirbytestwalk"); //TODO: Shit

                DebugLog.LogDebug("Created Entity with type: " + e.EntityType);
            }

        }
        private void Step60()
        {
            Delta60 -= 1;
            Entity[] list = Entities.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Active == true)
                    list[i].Step60();
            }
        }
        private void Step30()
        {
            Delta30 -= 1;
            Entity[] list = Entities.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Active == true)
                    list[i].Step30();
            }

        }
        private void Step10()
        {
            Delta10 -= 1;
            Entity[] list = Entities.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Active == true)
                    list[i].Step10();
            }

        }
        private void Step1()
        {
            Delta1 -= 1;
            Entity[] list = Entities.ToArray();
            for (var i = 0; i < list.Length; i++)
            {
                if (list[i].Active == true)
                    list[i].Step1();
            }
        }
        private void Step1s()
        {
            Delta1s -= 1;
            Entity[] list = Entities.ToArray();
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
                X = Mouse.GetState().X * ((float)GameCam.ViewW / OptionsGraphics.ScreenResolution.Width),
                Y = Mouse.GetState().Y * ((float)GameCam.ViewH / OptionsGraphics.ScreenResolution.Height)
            };
            return loc;
        }
        public static bool MouseLeftButtonIsPressed()
        {
            return (StateMouse.LeftButton == ButtonState.Pressed && StateMouseOld.LeftButton == ButtonState.Released);
        }
        public static bool MouseRightButtonIsPressed()
        {
            return (StateMouse.RightButton == ButtonState.Pressed && StateMouseOld.RightButton == ButtonState.Released);
        }

        public static bool MouseLeftButtonIsReleased()
        {
            return (StateMouse.LeftButton == ButtonState.Released && StateMouseOld.LeftButton == ButtonState.Pressed);
        }
        public static bool MouseRightButtonIsReleased()
        {
            return (StateMouse.RightButton == ButtonState.Released && StateMouseOld.RightButton == ButtonState.Pressed);
        }





        private static ulong NextEntityID = 0;
        public static ulong GetNextEntityID()
        {
            NextEntityID += 1;
            return NextEntityID;
        }


    }
}
