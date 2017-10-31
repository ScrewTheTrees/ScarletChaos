using Microsoft.Xna.Framework.Graphics;
using ScarletResource.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletResource
{
    public class Camera
    {
        public Entity Target;

        public int ViewX;
        public int ViewY;

        public int ViewW;
        public int ViewH;

        public Viewport MainView;

        public void Update()
        {
            MainView.X = ViewX;
            MainView.Y = ViewY;
            ViewW = MainView.Width;
            ViewH = MainView.Height;
        }


        public Camera(Viewport view)
        {
            MainView = view;
            ViewW = view.Width;
            ViewH = view.Height;
        }
    }
}
