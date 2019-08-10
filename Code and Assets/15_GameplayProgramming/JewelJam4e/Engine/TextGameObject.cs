using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class TextGameObject : GameObject
{
    protected SpriteFont font;
    protected Color color;
    public string Text { get; set; }

    public enum Alignment
    {
        Left, Right, Center
    }

    protected Alignment alignment;

    public TextGameObject(string fontName, Color color,
        Alignment alignment = Alignment.Left)
    {
        font = ExtendedGame.ContentManager.Load<SpriteFont>(fontName);
        this.color = color;
        this.alignment = alignment;

        Text = "";
    }


    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!Visible)
            return;

        // calculate the origin
        Vector2 origin = new Vector2(OriginX, 0);

        // draw the text
        spriteBatch.DrawString(font, Text, GlobalPosition,
            color, 0f, origin, 1, SpriteEffects.None, 0);
    }

    float OriginX
    {
        get
        {
            if (alignment == Alignment.Left) // left-aligned
                return 0;

            if (alignment == Alignment.Right) // right-aligned
                return font.MeasureString(Text).X;

            return font.MeasureString(Text).X / 2.0f; // centered
        }
    }
}