using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletResource.Pipeline
{
    public class Sprite
    {
        public Texture2D Tex;
        public String TexPath;
        public SpriteEffects spriteEffect = new SpriteEffects();
        public Rectangle FrameRect;
        public Color ColorBlend = Color.White;

        public Vector2 Origin = new Vector2(0, 0);
        public Vector2 Offset = new Vector2(0, 0);
        public Vector2 Scale = new Vector2(1, 1);

        public bool IsAnimated = true;
        public bool Looping = false;
        public bool Ended = false;

        private float Speed = 1;
        public float Rotation = 0;
        public float FrameIndex = 0;
        private int FrameIndexTotal = 1;
        public int FrameWidth;
        public int FrameHeight;

        /// <summary> Standard Sprite, no Animation </summary>
        /// <param name="tex">Sprite Texture</param>
        public Sprite(Texture2D tex, String texPath) : this(tex, texPath, tex.Width, tex.Height, 0, 0, 0) { }

        /// <summary> Simple Animation Sprite </summary>
        /// <param name="tex">Sprite Texture</param>
        /// <param name="width">Width of each Frame</param>
        /// <param name="height">Height of each Frame</param>
        /// <param name="TotalFrames">Manually define the amount of frames.</param>
        public Sprite(Texture2D tex, String texPath, int width, int height, int TotalFrames = 0) : this(tex, texPath, width, height, 0, 0, TotalFrames) { }

        /// <summary> Manually define entire sprite. </summary>
        /// <param name="tex">Sprite Texture</param>
        /// <param name="width">Width of each Frame</param>
        /// <param name="height">Height of each Frame</param>
        /// <param name="offsetX">The starting X Offset of all frames.</param>
        /// <param name="offsetY">The starting Y Offset of all frames.</param>
        /// <param name="TotalFrames">Manually define the amount of frames.</param>
        public Sprite(Texture2D tex, String texPath, int width, int height, int offsetX, int offsetY, int TotalFrames = 0)
        {
            Tex = tex;
            TexPath = texPath;

            Offset.X = offsetX;
            Offset.Y = offsetY;
            FrameWidth = Math.Max(width, 1);
            FrameHeight = Math.Max(height, 1);
            FrameRect = new Rectangle(offsetX, offsetY, FrameWidth, FrameHeight);

            if (TotalFrames <= 0)
            {
                for (var i = offsetX; i < Tex.Width - FrameWidth; i += FrameWidth)
                {
                    FrameIndexTotal += 1;
                }
            }
            else FrameIndexTotal = TotalFrames;

            if (FrameIndexTotal > 1) IsAnimated = true;
        }
        /// <summary> Manually define entire sprite including optional origin. </summary>
        /// <param name="tex">Sprite Texture</param>
        /// <param name="width">Width of each Frame</param>
        /// <param name="height">Height of each Frame</param>
        /// <param name="offsetX">The starting X Offset of all frames.</param>
        /// <param name="offsetY">The starting Y Offset of all frames.</param>
        /// <param name="originX">The X Origin (Center) of sprite. </param>
        /// <param name="originY">The Y Origin (Center) of sprite. </param>
        /// <param name="TotalFrames">Manually define the amount of frames.</param>
        public Sprite(Texture2D tex, String texPath, int width, int height, int offsetX, int offsetY, int originX, int originY, int TotalFrames = 0) : this(tex, texPath, width, height, offsetX, offsetY, TotalFrames)
        {
            Origin.X = originX;
            Origin.Y = originY;
        }



        public void Update(GameTime gameTime)
        {
            if (IsAnimated == false) return;

            //Averages at about 60 frames per second ;)
            FrameIndex += (float)(((gameTime.ElapsedGameTime.TotalMilliseconds * 0.001) * 60) * Speed);

            if (FrameIndex >= FrameIndexTotal && FrameIndexTotal > 0)
            {
                if (Looping == false)
                {
                    Ended = true;
                    FrameIndex = FrameIndexTotal - 1;
                }
                else FrameIndex -= FrameIndexTotal;
            }
            FrameRect.X = (int)(Offset.X + ((int)FrameIndex * FrameWidth));
            FrameRect.Y = (int)(Offset.Y);
        }

        public void SetOrigin(Vector2 ori) { SetOrigin(ori.X, ori.Y); }
        public void SetOrigin(float originX, float originY)
        {
            Origin.X = originX;
            Origin.Y = originY;
        }
        public void SetOriginCenter()
        {
            Origin.X = FrameWidth / 2;
            Origin.Y = FrameHeight / 2;
        }

        public int GetCurrentFrame()
        {
            return (int)FrameIndex; //We want it rounded down anyway!
        }

        public void SetAnimationSpeed(float PlaybackSpeed)
        {
            Speed = PlaybackSpeed;
        }
        public void SetAnimationSpeed(int FramesPerSecond)
        {
            Speed = FramesPerSecond / 60f;
        }
        public void DrawAnimation(SpriteBatch spriteBatch, Vector2 DrawPosition, Single depth)
        {
            spriteBatch.Draw(Tex, DrawPosition, FrameRect, ColorBlend, Rotation, Origin, Scale, spriteEffect, depth);
        }
    }
}
