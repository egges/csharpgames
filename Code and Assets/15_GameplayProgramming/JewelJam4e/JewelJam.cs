using System;

class JewelJam : ExtendedGame
{
    [STAThread]
    static void Main()
    {
        JewelJam game = new JewelJam();
        game.Run();
    }

    public JewelJam()
    {
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        // initialize the game world
        gameWorld = new JewelJamGameWorld(this);

        // to re-scale the game world to the screen size, we need to set the FullScreen property again
        worldSize = GameWorld.Size;
        FullScreen = false;
    }

    public static JewelJamGameWorld GameWorld
    {
        get { return (JewelJamGameWorld)gameWorld; }
    }

}