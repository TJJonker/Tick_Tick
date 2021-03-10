using Microsoft.Xna.Framework;
using System;

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
        public bool IsLooping { get; private set; }

        /// <summary>
        /// The total number of frames in this animation
        /// </summary>
        public int NumberOfFrames { get { return NumberOfSheetElements; } }

        /// <summary>
        /// Whether or not the animation has finished playing
        /// </summary>
        public bool AnimationEnded
        {
            get { return !IsLooping && SheetIndex >= NumberOfFrames - 1; }
        }

        /// <summary>
        /// The time (in seconds) that has passed since the last frame change
        /// </summary>
        private float time;

        /// <summary>
        /// Creates a new <see cref="Animation"/> with the given asset, depth, 
        /// timePerFrame and whether it loops or not
        /// </summary>
        /// <param name="assetname">The name of the asset that should be drawn</param>
        /// <param name="depth">The depth on which the animation should be drawn</param>
        /// <param name="looping">Whether or not the animation will loop</param>
        /// <param name="timePerFrame">How long (in seconds) a frame will be shown</param>
        public Animation(string assetname, float depth, bool looping, float timePerFrame) 
            : base(assetname, depth)
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

            // If enough time has passed, go to the next frame
            while(time > TimePerFrame)
            {
                time -= TimePerFrame;

                if (IsLooping) // Go to the next frame, or loop around
                    SheetIndex = (SheetIndex + 1) % NumberOfSheetElements;
                else // Go to the next frame if it exists
                    SheetIndex = Math.Min(SheetIndex + 1, NumberOfFrames - 1);
            }
        }


    }
}