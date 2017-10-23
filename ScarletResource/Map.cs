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
        public List<Solid> Solids = new List<Solid>();
        public int BlockWidth = 64;
        public int BlockHeight = 64;


        public int MapWidth = 64 * 64;
        public int MapHeight = 32 * 64;

        public static string CurrentIDV = MapIDV1;


        public static string MapIDV1 = "SCM1";


    }
}
