using Microsoft.Xna.Framework;

class MovableAnimal : Animal
{
    bool isInHole;

    public MovableAnimal(Level level, int animalIndex, bool isInHole)
        : base(level, GetSpriteName(isInHole), animalIndex)
    {
        this.isInHole = isInHole;
    }

    public int AnimalIndex { get { return SheetIndex; } }

    static string GetSpriteName(bool isInHole)
    {
        if (isInHole)
            return "Sprites/LevelObjects/spr_penguin_boxed@8";
        return "Sprites/LevelObjects/spr_penguin@8";
    }

    public bool IsInHole
    {
        get { return isInHole; }
        private set
        {
            isInHole = value;
            sprite = new SpriteSheet(GetSpriteName(isInHole), AnimalIndex);
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (Visible
        && BoundingBox.Contains(inputHelper.MousePositionWorld)
        && inputHelper.MouseLeftButtonPressed())
        {
            level.SelectAnimal(this);
        }
    }

    public void TryMoveInDirection(Point direction)
    {
    }

    public bool CanMoveInDirection(Point direction)
    {
        return true;
    }
}