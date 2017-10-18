using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScarletChaos.DataUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos
{
    public class GraphicsOptions
    {
        private GraphicsDeviceManager Graphics;

        public bool OptionFullscreen = false;
        public bool OptionRealFullscreen = false;
        public int ScreenMode = SCREENMODE_WINDOWED;

        public static string FILENAME_GRAPHICS = "Graphics.ini";

        public static string SECTION_DISPLAY = "Display";

        public static int SCREENMODE_WINDOWED = 0;
        public static int SCREENMODE_FULLSCREEN = 1;
        public static int SCREENMODE_BORDERLESSFULLSCREEN = 2;

        public GraphicsOptions(GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
        }

        public void LoadGraphicsOptions()
        {
            IniFile file = new IniFile(FILENAME_GRAPHICS);

            int.TryParse(file.Read("ScreenMode", SECTION_DISPLAY), out ScreenMode);

        }
        public void SaveGraphicsOptions()
        {
            IniFile file = new IniFile(FILENAME_GRAPHICS);

            file.Write("ScreenMode", ScreenMode.ToString(), SECTION_DISPLAY);
        }

        public void ApplyGraphicOptions()
        {
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

            //TODO: Screen sizes.
            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 720;

            Graphics.ApplyChanges();
        }


    }
}
