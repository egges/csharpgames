using Microsoft.Xna.Framework;

class PairList : GameObjectList
{
    int nrPairsMade;
    SpriteGameObject[] pairObjects;

    public PairList(int nrPairs)
    {
        // add the background image
        AddChild(new SpriteGameObject("Sprites/spr_frame_goal"));

        // add a sprite object for each pair that the player should make
        Vector2 offset = new Vector2(100, 7);
        pairObjects = new SpriteGameObject[nrPairs];
        for (int i = 0; i < nrPairs; i++)
        {
            pairObjects[i] = new SpriteGameObject("Sprites/spr_penguin_pairs@8", 7);
            pairObjects[i].LocalPosition = offset + new Vector2(i * pairObjects[i].Width, 0);
            AddChild(pairObjects[i]);
        }

        // start at 0 pairs
        nrPairsMade = 0;
    }

    public void AddPair(int penguinIndex)
    {
        pairObjects[nrPairsMade].SheetIndex = penguinIndex;
        nrPairsMade++;
    }

    public bool Completed { get { return nrPairsMade == pairObjects.Length; } }
}