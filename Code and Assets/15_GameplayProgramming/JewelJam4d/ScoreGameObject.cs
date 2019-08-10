using Microsoft.Xna.Framework;

class ScoreGameObject : TextGameObject
{
    public ScoreGameObject() : base("JewelJamFont", Color.White, Alignment.Right)
    {
    }

    public override void Update(GameTime gameTime)
    {
        Text = JewelJam.GameWorld.Score.ToString();
    }
}