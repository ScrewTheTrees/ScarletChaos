using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletResource
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

        public int OriginX;
        public int OriginY;

        /// <summary> Standard Sprite, no Animation </summary>
        public Animation(Texture2D sprite) : this(sprite, sprite.Width, sprite.Height, 0, 0, 0) { }

        /// <summary> Simple Animation Sprite </summary>
        /// <param name="width">Width of each Frame</param>
        /// <param name="height">Height of each Frame</param>
        /// <param name="TotalFrames">Manually define the amount of frames.</param>
        public Animation(Texture2D sprite, int width, int height, int TotalFrames = 0) : this(sprite, width, height, 0, 0, TotalFrames) { }

        /// <summary> Manually define entire sprite. </summary>
        /// <param name="sprite">Sprite Texture</param>
        /// <param name="width">Width of each Frame</param>
        /// <param name="height">Height of each Frame</param>
        /// <param name="offsetX">The starting X Offset of all frames.</param>
        /// <param name="offsetY">The starting Y Offset of all frames.</param>
        /// <param name="TotalFrames">Manually define the amount of frames.</param>
        public Animation(Texture2D sprite, int width, int height, int offsetX, int offsetY, int TotalFrames = 0)
        {
            Sprite = sprite;

            FrameWidth = Math.Max(width,1);
            FrameHeight = Math.Max(height,1);
            FrameRect = new Rectangle(offsetX, offsetY, FrameWidth, FrameHeight);

            if (TotalFrames <= 0)
            {
                for (var i = offsetX; i < Sprite.Width - FrameWidth; i += FrameWidth)
                {
                    FrameIndexTotal += 1;
                }
            }
            else FrameIndexTotal = TotalFrames;
        }
        /// <summary> Manually define entire sprite. </summary>
        /// <param name="sprite">Sprite Texture</param>
        /// <param name="width">Width of each Frame</param>
        /// <param name="height">Height of each Frame</param>
        /// <param name="offsetX">The starting X Offset of all frames.</param>
        /// <param name="offsetY">The starting Y Offset of all frames.</param>
        /// <param name="originX">The X Origin (Center) of sprite. </param>
        /// <param name="originY">The Y Origin (Center) of sprite. </param>
        /// <param name="TotalFrames">Manually define the amount of frames.</param>
        public Animation(Texture2D sprite, int width, int height, int offsetX, int offsetY, int originX, int originY ,int TotalFrames = 0) : this(sprite, width, height, offsetX, offsetY, TotalFrames)
        {
            OriginX = originX;
            OriginY = originY;
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


        public void DrawAnimation(SpriteBatch spriteBatch, Vector2 DrawPosition)
        {
            spriteBatch.Draw(Sprite, DrawPosition, FrameRect, Color.White);
        }



    }
}
