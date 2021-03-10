using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// A class that can represent a game object with animated sprites.
    /// </summary>
    public class AnimationGameObject : SpriteGameObject
    {
        private Dictionary<string, Animation> animations;

        public AnimationGameObject(float depth) : base(null, depth)
        {
            animations = new Dictionary<string, Animation>();
        }

        public void LoadAnimation(string assetName, string id,
            bool looping, float frameTime)
        {
            Animation anim = new Animation(assetName, depth, looping, frameTime);
            animations[id] = anim;
        }

        public void PlayAnimation(string id, bool forceRestart = false, int startSheetIndex = 0)
        {
            // If the animation is already playing, do nothing
            if (!forceRestart && sprite == animations[id])
                return;

            animations[id].Play(startSheetIndex);
            sprite = animations[id];
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (sprite != null)
                ((Animation)sprite).Update(gameTime);
        }
    }
}