using ScarletResource.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletResource
{
    public class Map
    {
        public static Map CurrentMap;

        public Map()
        {
            CurrentMap = this;
        }

        public List<Solid> Solids = new List<Solid>();
        public List<Tile> Tiles = new List<Tile>();

        public int MapX = 0;
        public int MapY = 0;
        public int MapWidth = 64 * 64;
        public int MapHeight = 32 * 64;



        private static int NewSolidID = 0;
        private static int GetNewSolidID()
        {
            NewSolidID += 1;
            return NewSolidID;
        }

        //These wont ever change.
        public static string CurrentIDV = MapIDV1;
        public const string MapIDV1 = "SCM1";

        public const int BlockWidth = 64;
        public const int BlockHeight = 64;
    }
}
