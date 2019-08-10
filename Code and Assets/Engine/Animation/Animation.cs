using System;
using Microsoft.Xna.Framework;

namespace Engine
{
    public class Animation : SpriteSheet
    {
        /// <summary>
        /// Indicates how long (in seconds) each frame of the animation is shown.
        /// </summary>
        public float TimePerFrame { get; protected set; }

        /// <summary>
        /// Whether or not the animation should restart when the last frame has passed.
        /// </summary>
        public bool IsLooping { get; protected set; }

        /// <summary>
        /// The total number of frames in this animation.
        /// </summary>
        public int NumberOfFrames { get { return NumberOfSheetElements; } }

        /// <summary>
        /// Whether or not the animation has finished playing.
        /// </summary>
        public bool AnimationEnded
        {
            get { return !IsLooping && SheetIndex >= NumberOfFrames - 1; }
        }

        /// <summary>
        /// The time (in seconds) that has passed since the last frame change.
        /// </summary>
        float time;

        public Animation(string assetname, float depth, 
            bool looping, float timePerFrame) : base(assetname, depth)
        {
            IsLooping = looping;
            TimePerFrame = timePerFrame;
        }

        public void Play(int startSheetIndex)
        {
            SheetIndex = startSheetIndex;
            time = 0.0f;
        }

        public void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // if enough time has passed, go to the next frame
            while (time > TimePerFrame)
            {
                time -= TimePerFrame;

                if (IsLooping) // go to the next frame, or loop around
                    SheetIndex = (SheetIndex + 1) % NumberOfSheetElements;
                else // go to the next frame if it exists
                    SheetIndex = Math.Min(SheetIndex + 1, NumberOfSheetElements - 1);
            }
        }
    }
}