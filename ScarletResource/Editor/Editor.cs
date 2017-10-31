using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ScarletResource.DataUtility;
using ScarletResource.Entities;
using ScarletResource;
using System.Collections.Generic;
using System.IO;
using System;
using ScarletResource.Pipeline;
using ScarletResource.MapObjects;
using MonoGame.Framework;

namespace ScarletResource.Editor
{
    public class Editor : Game
    {
        public static Editor EditorInstance;
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static GraphicsOptions OptionsGraphics;
        public static List<Entity> Entities = new List<Entity>();

        public static bool DebugDraw = true;
        public static Map CurrentMap = Map.CurrentMap;

        public static Camera GameCam = new Camera(new Viewport(0, 0, 1920, 1080, -1, 1));
        public static MouseState StateMouse = Mouse.GetState();
        public static MouseState StateMouseOld = Mouse.GetState();
        public static KeyboardState StateKeyboard = Keyboard.GetState();
        public static KeyboardState StateKeyboardOld = Keyboard.GetState();

        public static string GameDirectory = Directory.GetCurrentDirectory();

        public Editor()
        {
            Content.RootDirectory = FileManager.DIR_ASSETS; //Fix that shit

            IsMouseVisible = true;

            DebugLog.LogInfo("Editor Instance has been Initialized.");
            graphics = new GraphicsDeviceManager(this);

            EditorInstance = this;

            OptionsGraphics = new GraphicsOptions(graphics, "EditorGraphics.ini");
            OptionsGraphics.LoadGraphicsOptions();
            OptionsGraphics.ApplyGraphicOptions();
            OptionsGraphics.SaveGraphicsOptions();


            CurrentMap = new Map(); //Current map is static anyway.


            DebugLog.LogInfo("Editor Instance constructor loaded.");
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            DebugLog.LogInfo("Main Editor initialization.");
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load Content.
        /// </summary>
        protected override void LoadContent()
        {
            DebugLog.LogInfo("Creating Editor ContentLoaders.");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
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



            base.Update(gameTime);
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
