abstract class Animal : SpriteGameObject
{
    protected Animal(string spriteName, int sheetIndex = 0) 
        : base(spriteName, sheetIndex)
    {
    }
}