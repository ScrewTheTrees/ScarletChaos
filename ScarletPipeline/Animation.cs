using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletPipeline
{
    public class Animation
    {
        public Texture2D Sprite;

        public bool IsAnimated = false;
        public bool Active = true;
        public bool Looping = false;

        private float Speed = 1f;

        public float FrameIndex;
        private int FrameIndexTotal;

        public Rectangle FrameRect;

        public int FrameWidth;
        public int FrameHeight;


        public void Draw(SpriteBatch batch)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (Active == false) return;

            //Averages at about 60 frames per second ;)
            FrameIndex += (float) (((gameTime.ElapsedGameTime.TotalMilliseconds * 0.001) * 60) * Speed);

            while (FrameIndex >= FrameIndexTotal)
            {
                FrameIndex -= FrameIndexTotal;
            }
        }

        public int GetCurrentFrame()
        {
            return (int)FrameIndex; //We want it rounded down anyway!
        }


        public void SetAnimationSpeed(float PlaybackSpeed)
        {
            Speed = PlaybackSpeed;
        }
        public void SetAnimationSpeed(int PlaybackSpeedByFramesSecond)
        {
            Speed = 60f / PlaybackSpeedByFramesSecond;
        }
    }
}
