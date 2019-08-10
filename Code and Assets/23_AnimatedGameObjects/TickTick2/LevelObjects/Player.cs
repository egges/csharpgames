using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class Player : AnimatedGameObject
{
    const float walkingSpeed = 400; // Standard walking speed, in pixels per second.
    bool facingLeft; // Whether or not the character is currently looking to the left.

    public Player() : base(TickTick.Depth_LevelPlayer)
    {
        // load all animations
        LoadAnimation("Sprites/LevelObjects/Player/spr_idle", "idle", true, 0.1f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_run@13", "run", true, 0.04f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_jump@14", "jump", false, 0.08f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_celebrate@14","celebrate",false,0.05f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_die@5", "die", true, 0.1f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_explode@5x5", "explode", false, 0.04f);

        // start with the idle sprite
        PlayAnimation("idle");
        SetOriginToBottomCenter();
        facingLeft = false;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        // arrow keys: move left or right
        if (inputHelper.KeyDown(Keys.Left))
        {
            facingLeft = true;
            velocity.X = -walkingSpeed;
            PlayAnimation("run");
        }
        else if (inputHelper.KeyDown(Keys.Right))
        {
            facingLeft = false;
            velocity.X = walkingSpeed;
            PlayAnimation("run");
        }
        // no arrow keys: don't move
        else
        {
            velocity.X = 0;
            PlayAnimation("idle");
        }

        // set the origin to the character's feet
        SetOriginToBottomCenter();

        // make sure the sprite is facing the correct direction
        sprite.Mirror = facingLeft;
    }

    void SetOriginToBottomCenter()
    {
        Origin = new Vector2(sprite.Width / 2, sprite.Height);
    }

}