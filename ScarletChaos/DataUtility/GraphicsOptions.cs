using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScarletChaos.DataUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.DataUtility
{
    public class GraphicsOptions
    {
        private GraphicsDeviceManager Graphics;

        public int ScreenMode = SCREENMODE_WINDOWED;
        public ScreenSize ScreenResolution = ScreenSize.SS_169_1280X720;

        public static string FILENAME_GRAPHICS = "Graphics.ini";

        public static string SECTION_DISPLAY = "Display";

        public static int SCREENMODE_WINDOWED = 0;
        public static int SCREENMODE_FULLSCREEN = 1;
        public static int SCREENMODE_BORDERLESSFULLSCREEN = 2;

        public GraphicsOptions(GraphicsDeviceManager graphics) => Graphics = graphics;


        public void LoadGraphicsOptions()
        {
            IniFile file = new IniFile(FILENAME_GRAPHICS);

            int.TryParse(file.Read("ScreenMode", SECTION_DISPLAY), out ScreenMode);

            if (int.TryParse(file.Read("ScreenResolution", SECTION_DISPLAY), out int temp))
            {
                if (ScreenSize.All_RESOLUTIONS.Any(x => x.MetaID == temp))
                    ScreenResolution = ScreenSize.All_RESOLUTIONS.First(x => x.MetaID == temp);
                else
                {
                    if (int.TryParse(file.Read("ScreenWidth", SECTION_DISPLAY), out int ScreenWidth)
                    && int.TryParse(file.Read("ScreenHeight", SECTION_DISPLAY), out int ScreenHeight))
                    {
                        ScreenResolution = new ScreenSize(ScreenWidth, ScreenHeight, "custom resolution", 0);
                    }
                }
            }
        }
        public void SaveGraphicsOptions()
        {
            IniFile file = new IniFile(FILENAME_GRAPHICS);

            file.Write("ScreenMode", ScreenMode.ToString(), SECTION_DISPLAY);
            file.Write("ScreenAspectRatio", ScreenResolution.AspectRatio.ToString(), SECTION_DISPLAY);
            file.Write("ScreenResolution", ScreenResolution.MetaID.ToString(), SECTION_DISPLAY);
            file.Write("ScreenWidth", ScreenResolution.Width.ToString(), SECTION_DISPLAY);
            file.Write("ScreenHeight", ScreenResolution.Height.ToString(), SECTION_DISPLAY);
        }

        public void ApplyGraphicOptions()
        {
            //TODO: Screen sizes.
            Graphics.PreferredBackBufferWidth = ScreenResolution.Width;
            Graphics.PreferredBackBufferHeight = ScreenResolution.Height;


            if (ScreenMode == SCREENMODE_WINDOWED)
            {
                if (Graphics.IsFullScreen == true)
                    Graphics.ToggleFullScreen();
            }
            else if (ScreenMode == SCREENMODE_FULLSCREEN)
            {
                Graphics.HardwareModeSwitch = true;
                if (Graphics.IsFullScreen == false)
                    Graphics.ToggleFullScreen();
            }
            else if (ScreenMode == SCREENMODE_BORDERLESSFULLSCREEN)
            {
                Graphics.HardwareModeSwitch = false;
                if (Graphics.IsFullScreen == false)
                    Graphics.ToggleFullScreen();
            }

            Graphics.ApplyChanges();
        }


    }
}
