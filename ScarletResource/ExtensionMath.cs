using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletResource
{
    public static class ExtensionMath
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static double DegToRad(this double angle)
        {
            return (Math.PI / 180) * angle;
        }
        public static float DegToRad(this float angle)
        {
            return (float)(Math.PI / 180) * angle;
        }


    }
}
