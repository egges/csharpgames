using System;
using Microsoft.Xna.Framework;

namespace Engine
{
    public class Animation : SpriteSheet
    {
        // Indicates how long (in seconds) each frame of the animation is shown.
        public float TimePerFrame { get; protected set; }

        // Whether or not the animation should restart when the last frame has passed.
        public bool IsLooping { get; protected set; }

        // The total number of frames in this animation.
        public int NumberOfFrames { get { return NumberOfSheetElements; } }

        // Whether or not the animation has finished playing.
        public bool AnimationEnded
        {
            get { return !isLooping && SheetIndex >= NumberOfFrames - 1; }
        }

        /// The time (in seconds) that has passed since the last frame change.
        float time;

        public Animation(string assetname, float depth, 
            bool looping, float timePerFrame) : base(assetname, depth)
        {
            IsLooping = looping;
            TimePerFrame = timePerFrame;
        }

        public void Play()
        {
            SheetIndex = 0;
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