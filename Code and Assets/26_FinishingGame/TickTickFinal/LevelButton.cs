using Engine;
using Microsoft.Xna.Framework;

class LevelButton : Engine.UI.LevelButton
{
    public LevelButton(int levelIndex, LevelStatus startStatus)
        : base(levelIndex, startStatus)
    {
        // add a label that shows the level index
        label = new TextGameObject("Fonts/MainFont", 1, Color.White, TextGameObject.Alignment.Right);
        label.LocalPosition = new Vector2(sprite.Width - 15, 10);
        label.Parent = this;
        label.Text = levelIndex.ToString();
    }
}
