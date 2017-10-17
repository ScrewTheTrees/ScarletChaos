using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public ScreenMode Mode = ScreenMode.WINDOWED;

        public GraphicsOptions(GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
        }

        public void LoadGraphicsOptions()
        {

        }

        public void ApplyGraphicOptions()
        {
            if (Mode == ScreenMode.WINDOWED)
            { 
                if (Graphics.IsFullScreen == true)
                    Graphics.ToggleFullScreen();
            }
            else if (Mode == ScreenMode.FULLSCREEN)
            {
                Graphics.HardwareModeSwitch = true;
                if (Graphics.IsFullScreen == false)
                    Graphics.ToggleFullScreen();
            }
            else if (Mode == ScreenMode.BORDERLESSFULLSCREEN)
            {
                Graphics.HardwareModeSwitch = false;
                if (Graphics.IsFullScreen == false)
                    Graphics.ToggleFullScreen();
            }


            Graphics.ApplyChanges();
        }


    }
    /// <summary>
    /// Mode to render the screen in.
    /// </summary>
    public enum ScreenMode
    {
        WINDOWED = 0,
        FULLSCREEN = 1,
        BORDERLESSFULLSCREEN = 2
    }
}
