using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class Ball : ThreeColorGameObject
{
    bool shooting;

    public Ball(ContentManager Content) : base(Content, "spr_ball_red", "spr_ball_green", "spr_ball_blue")
    {
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.MouseLeftButtonPressed() && !shooting)
        {
            shooting = true;
            velocity = (inputHelper.MousePosition - Painter.GameWorld.Cannon.Position) * 1.2f;
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (shooting)
        {
            velocity.Y += 400.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            Color = Painter.GameWorld.Cannon.Color;
            position = Painter.GameWorld.Cannon.BallPosition;
        }
        if (Painter.GameWorld.IsOutsideWorld(position))
        {
            Reset();
        }

        base.Update(gameTime);
    }

    public override void Reset()
    {
        base.Reset();
        velocity = Vector2.Zero;
        position = new Vector2(65, 390);
        shooting = false;
    }
}