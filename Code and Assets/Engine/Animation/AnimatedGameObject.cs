using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Engine
{
    /// <summary>
    /// A class that can represent a game object with several animated sprites.
    /// </summary>
    public class AnimatedGameObject : SpriteGameObject
    {
        Dictionary<string, Animation> animations;

        public AnimatedGameObject(float depth) : base(null, depth)
        {
            animations = new Dictionary<string, Animation>();
        }

        public void LoadAnimation(string assetName, string id, 
            bool looping, float frameTime)
        {
            Animation anim = new Animation(assetName, depth, looping, frameTime);
            animations[id] = anim;
        }

        public void PlayAnimation(string id, bool forceRestart=false, int startSheetIndex=0)
        {
            // if the animation is already playing, do nothing
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