using Engine;
using Engine.UI;

class Arrow : Button
{
    SpriteSheet normalSprite, hoverSprite;

    public Arrow(int sheetIndex) : base("Sprites/LevelObjects/spr_arrow1@4")
    {
        SheetIndex = sheetIndex;
        normalSprite = sprite;
        hoverSprite = new SpriteSheet("Sprites/LevelObjects/spr_arrow2@4", sheetIndex);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (BoundingBox.Contains(inputHelper.MousePositionWorld))
            sprite = hoverSprite;
        else
            sprite = normalSprite;
    }
}