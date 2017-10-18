using ScarletChaos.DataUtility;

namespace ScarletChaos
{
    /// <summary>
    /// Parental class for all Game Entities that are used to move around.
    /// </summary>
    public class Entity
    {
        public ulong EntityID;

        public EntityVector2 Location = new EntityVector2(0, 0);
        public EntityVector2 PreviousLocation = new EntityVector2(0,0);

        public double DrawDelta;
        public double StepDelta;

        public void Create() { }
        public void Destroy() { }
        public void Draw() { }

        public void StepRaw() { }
        public void Step1s() { }
        public void Step1() { }
        public void Step10() { }
        public void Step30() { }
        public void Step60() { }
        public void Step120() { }




        public void SetLocation(EntityVector2 vec) { Location.X = vec.X; Location.Y = vec.Y; }
        public void SetLocation(double x, double y) { Location.X = x; Location.Y = y; }

        /// <summary>Updated after step120, after all the step events have been executed.</summary>
        public void UpdateEntityData()
        {
            PreviousLocation.X = Location.X;
            PreviousLocation.Y = Location.Y;
        }
        public EntityVector2 GetDeltaPosition()
        {
            var x1 = Location.X;
            var y1 = Location.Y;
            var x2 = PreviousLocation.X;
            var y2 = PreviousLocation.Y;

            var xNew = x1 - x2;
            var yNew = y1 - y2;
            xNew = xNew * GameInstance.Delta120;
            yNew = yNew * GameInstance.Delta120;
            xNew += x1;
            yNew += y1;
            return new EntityVector2(xNew, yNew);
        }
        //End
    }
}
