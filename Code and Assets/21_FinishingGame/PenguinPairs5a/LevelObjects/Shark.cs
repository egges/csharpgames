using Microsoft.Xna.Framework;

class Shark : Animal
{
    public Shark(Level level, Point gridPosition) 
        : base(level, gridPosition, "Sprites/LevelObjects/spr_shark")
    {
    }
}