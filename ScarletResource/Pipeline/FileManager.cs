using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletResource.Pipeline
{
    public class FileManager
    {
        public static string DIR_ROOT = Directory.GetCurrentDirectory();
        public static string DIR_ASSETS = Directory.GetCurrentDirectory() + @"\Assets\";
        public static string DIR_TEXTURES = Directory.GetCurrentDirectory() + @"\Assets\Textures\";
        public static string DIR_SOUNDS = Directory.GetCurrentDirectory() + @"\Assets\Sounds\";
        public static string DIR_MUSIC = Directory.GetCurrentDirectory() + @"\Assets\Music\";
        public static string DIR_MAPS = Directory.GetCurrentDirectory() + @"\Assets\Maps\";
        public static string DIR_FONTS = Directory.GetCurrentDirectory() + @"\Assets\Fonts\";
    }
}
