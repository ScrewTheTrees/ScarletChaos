using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.DataStructs
{
    public struct EntityVector2
    {
        public double X;
        public double Y;

        public EntityVector2(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public struct EntityVector3
    {
        public double X;
        public double Y;
        public double Z;

        public EntityVector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
