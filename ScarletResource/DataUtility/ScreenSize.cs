using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScarletResource.DataUtility
{
    public struct ScreenSize
    {
        public int Width;
        public int Height;
        public string Name;
        public int MetaID;
        public float AspectRatio { get { return (float)Width / Height; } }

        public ScreenSize(int width, int height, string name, int metaID)
        {
            Width = width;
            Height = height;
            Name = name;
            MetaID = metaID;
        }

        /// <summary> All resolutions available. </summary>
        public static ScreenSize[] All_RESOLUTIONS
        {
            get
            {
                List<ScreenSize> resolutions = new List<ScreenSize>();

                resolutions.AddRange(RESOLUTIONS_43);
                resolutions.AddRange(RESOLUTIONS_169);
                resolutions.AddRange(RESOLUTIONS_1610);
                resolutions.AddRange(RESOLUTIONS_219);

                ScreenSize[] list = resolutions.ToArray();
                return list;
            }
        }


        /// <summary> All 4 by 3 resolutions. </summary>
        public static ScreenSize[] RESOLUTIONS_43 =
        {
            SS_43_640X480, SS_43_800X600, SS_43_960X720, SS_43_1024X768,
            SS_43_1280X960, SS_43_1440X1080, SS_43_1600X1200, SS_43_1920X1440
        };
        /// <summary> All 16 by 9 resolutions. </summary>
        public static ScreenSize[] RESOLUTIONS_169 =
        {
            SS_169_1280X720, SS_169_1366X768, SS_169_1600X900,SS_169_1920X1080,
            SS_169_2560X1440, SS_169_3200X1800, SS_169_3840X2160, SS_169_4096X2304
        };
        /// <summary> All 16 by 10 resolutions. </summary>
        public static ScreenSize[] RESOLUTIONS_1610 =
        {
            SS_1610_1280X800, SS_1610_1440X900, SS_1610_1680X1050, SS_1610_1920X1200,
            SS_1610_2560X1600
        };
        /// <summary> All 21 by 9 resolutions. </summary>
        public static ScreenSize[] RESOLUTIONS_219 =
        {
            SS_219_1680X720, SS_219_2560X1080, SS_219_3440X1440, SS_219_5120X2160
        };

        //4 by 3
        public static ScreenSize SS_43_640X480 { get { return new ScreenSize(640, 480, "640x480", 1); } }
        public static ScreenSize SS_43_800X600 { get { return new ScreenSize(800, 600, "800x600", 2); } }
        public static ScreenSize SS_43_960X720 { get { return new ScreenSize(960, 720, "960x720", 3); } }
        public static ScreenSize SS_43_1024X768 { get { return new ScreenSize(1024, 768, "1024x768", 4); } }
        public static ScreenSize SS_43_1280X960 { get { return new ScreenSize(1280, 960, "1280x960", 5); } }
        public static ScreenSize SS_43_1440X1080 { get { return new ScreenSize(1440, 1080, "1440x1080", 6); } }
        public static ScreenSize SS_43_1600X1200 { get { return new ScreenSize(1600, 1200, "1600x1200", 7); } }
        public static ScreenSize SS_43_1920X1440 { get { return new ScreenSize(1920, 1440, "1920x1440", 8); } }

        //16 by 9
        public static ScreenSize SS_169_1280X720 { get { return new ScreenSize(1280, 720, "1280x720", 9); } }
        public static ScreenSize SS_169_1366X768 { get { return new ScreenSize(1366, 768, "1366x768", 10); } }
        public static ScreenSize SS_169_1600X900 { get { return new ScreenSize(1600, 900, "1600x900", 11); } }
        public static ScreenSize SS_169_1920X1080 { get { return new ScreenSize(1920, 1080, "1920x1080", 12); } }
        public static ScreenSize SS_169_2560X1440 { get { return new ScreenSize(2560, 1440, "2560x1440", 13); } }
        public static ScreenSize SS_169_3200X1800 { get { return new ScreenSize(3200, 1800, "3200x1800", 14); } }
        public static ScreenSize SS_169_3840X2160 { get { return new ScreenSize(3840, 2160, "3840x2160", 15); } }
        public static ScreenSize SS_169_4096X2304 { get { return new ScreenSize(4096, 2304, "4096x2304", 16); } }

        //16 by 10
        public static ScreenSize SS_1610_1280X800 { get { return new ScreenSize(1280, 800, "1280x800", 17); } }
        public static ScreenSize SS_1610_1440X900 { get { return new ScreenSize(1440, 900, "1440x900", 18); } }
        public static ScreenSize SS_1610_1680X1050 { get { return new ScreenSize(1680, 1050, "1680x1050", 19); } }
        public static ScreenSize SS_1610_1920X1200 { get { return new ScreenSize(1920, 1200, "1920x1200", 20); } }
        public static ScreenSize SS_1610_2560X1600 { get { return new ScreenSize(2560, 1600, "2560x1600", 21); } }

        //21 by 9
        public static ScreenSize SS_219_1680X720 { get { return new ScreenSize(1680, 720, "1680x720", 22); } }
        public static ScreenSize SS_219_2560X1080 { get { return new ScreenSize(2560, 1080, "2560x1080", 23); } }
        public static ScreenSize SS_219_3440X1440 { get { return new ScreenSize(3440, 1440, "3440x1440", 24); } }
        public static ScreenSize SS_219_5120X2160 { get { return new ScreenSize(5120, 2160, "5120x2160", 25); } }
    }
}
