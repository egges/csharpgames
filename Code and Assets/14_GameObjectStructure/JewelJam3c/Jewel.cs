class Jewel : SpriteGameObject
{
    public int Type { get; private set; }

    public Jewel(int type)
        : base("spr_single_jewel" + (type + 1))
    {
        Type = type;
    }
}
