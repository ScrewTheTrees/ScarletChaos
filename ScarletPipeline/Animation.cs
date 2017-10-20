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
        public bool Looping = false;

        private float Speed = 1f;

        public float FrameIndex = 0f;
        private int FrameIndexTotal = 1;

        public Rectangle FrameRect;

        public int FrameWidth;
        public int FrameHeight;

        /// <summary> Standard Sprite, no Animation </summary>
        public Animation(Texture2D sprite)
        {
            Sprite = sprite;
            FrameWidth = sprite.Width;
            FrameHeight = sprite.Height;
            FrameRect = new Rectangle(0, 0, FrameWidth, FrameHeight);
        }


        public void Draw(SpriteBatch batch)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (IsAnimated == false) return;

            //Averages at about 60 frames per second ;)
            FrameIndex += (float)(((gameTime.ElapsedGameTime.TotalMilliseconds * 0.001) * 60) * Speed);

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
