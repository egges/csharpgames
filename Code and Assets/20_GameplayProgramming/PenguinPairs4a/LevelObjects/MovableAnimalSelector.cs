using Microsoft.Xna.Framework;

class MovableAnimalSelector : GameObjectList
{
    Arrow[] arrows;
    Point[] directions;
    MovableAnimal selectedAnimal;

    public MovableAnimalSelector()
    {
        // define the four directions
        directions = new Point[4];
        directions[0] = new Point(1, 0);
        directions[1] = new Point(0, -1);
        directions[2] = new Point(-1, 0);
        directions[3] = new Point(0, 1);

        // add the four arrows
        arrows = new Arrow[4];
        for (int i = 0; i < 4; i++)
        {
            arrows[i] = new Arrow(i);
            arrows[i].LocalPosition = new Vector2(directions[i].X * arrows[i].Width, directions[i].Y * arrows[i].Height);
            AddChild(arrows[i]);
        }

        SelectedAnimal = null;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (SelectedAnimal == null)
            return;

        base.HandleInput(inputHelper);

        // check if any of the arrow buttons have been pressed
        for (int i = 0; i < 4; i++)
        {
            if (arrows[i].Pressed)
            {
                SelectedAnimal.TryMoveInDirection(directions[i]);
                return;
            }
        }

        // if the player clicks anywhere else, deselect the current animal
        if (inputHelper.MouseLeftButtonPressed())
            SelectedAnimal = null;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (SelectedAnimal != null)
        {
            LocalPosition = selectedAnimal.LocalPosition;

            // the arrows should only be visible if the animal can move in that direction
            for (int i = 0; i < 4; i++)
                arrows[i].Visible = SelectedAnimal.CanMoveInDirection(directions[i]);
        }
    }

    public MovableAnimal SelectedAnimal
    {
        get { return selectedAnimal; }
        set
        {
            selectedAnimal = value;
            Visible = (selectedAnimal != null);
        }
    }
}
