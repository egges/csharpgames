using Microsoft.Xna.Framework;

class MovableAnimal : Animal
{
    Vector2 targetWorldPosition;
    const float speed = 300;

    bool isInHole;

    public MovableAnimal(Level level, Point gridPosition, int animalIndex)
        : base(level, gridPosition, GetSpriteName(false), animalIndex)
    {
        targetWorldPosition = LocalPosition;
    }

    public int AnimalIndex { get { return SheetIndex; } }
    bool IsSeal { get { return AnimalIndex == 7; } }
    bool IsMultiColoredPenguin { get { return AnimalIndex == 6; } }

    public bool IsInHole
    {
        get { return isInHole; }
        private set
        {
            isInHole = value;
            sprite = new SpriteSheet(GetSpriteName(isInHole), AnimalIndex);
        }
    }

    static string GetSpriteName(bool isInHole)
    {
        if (isInHole)
            return "Sprites/LevelObjects/spr_penguin_boxed@8";
        return "Sprites/LevelObjects/spr_penguin@8";
    }

    bool IsMoving { get { return LocalPosition != targetWorldPosition; } }

    bool IsPairWith(MovableAnimal other)
    {
        // if one animal is a seal, then there's no match
        if (IsSeal || other.IsSeal)
            return false;

        // if one animal is a multi-colored penguin, then there's always a match
        if (IsMultiColoredPenguin || other.IsMultiColoredPenguin)
            return true;

        // otherwise, there's a match if both animals are the same type of penguin
        return AnimalIndex == other.AnimalIndex;
    }

    protected override void ApplyCurrentPosition()
    {
        // set the position in the game world
        LocalPosition = level.GetCellPosition(currentGridPosition.X, currentGridPosition.Y);

        // stop moving
        velocity = Vector2.Zero;

        // if the current tile has the "empty" type, let the animal die
        Tile.Type tileType = level.GetTileType(currentGridPosition);
        if (tileType == Tile.Type.Empty)
        {
            Visible = false;
            return;
        }

        // If the current tile already contains another animal, then both animals should disappear!
        Animal otherAnimal = level.GetAnimal(currentGridPosition);
        if (otherAnimal != null)
        {
            level.RemoveAnimalFromGrid(currentGridPosition);
            Visible = false;
            otherAnimal.Visible = false;

            // TODO: if the other animal matches, notify the game that we've made a pair

            return;
        }

        // In all other cases, the animal shouldn't disappear yet, so it's still part of the level. 
        // Add the animal to the level's grid.
        level.AddAnimalToGrid(this, currentGridPosition);

        // if the current tile is a hole, mark this animal as "inside a hole"
        if (tileType == Tile.Type.Hole)
            IsInHole = true;
    }

    /// <summary>
    /// Checks and returns whether this MovableAnimal can move (at least one grid cell) in the given direction. 
    /// Moving in this direction might cause the MovableAnimal to die, but that doesn't matter here.
    /// </summary>
    /// <param name="direction">A direction to move in.</param>
    /// <returns>true if the animal can move in the given direction; false otherwise.</returns>
    public bool CanMoveInDirection(Point direction)
    {
        if (!Visible || IsInHole || IsMoving)
            return false;

        // check what's going on at the current tile
        Tile.Type tileType = level.GetTileType(currentGridPosition);
        Animal otherAnimal = level.GetAnimal(currentGridPosition);

        // if the current tile has the "empty" or "hole" type, the animal should stop moving
        if (tileType == Tile.Type.Empty || tileType == Tile.Type.Hole)
            return false;

        // if there's another animal at the current tile, something should happen
        if (otherAnimal != null && otherAnimal != this)
            return false;

        // check what's going on at the next tile
        Point nextPosition = currentGridPosition + direction;
        Tile.Type nextType = level.GetTileType(nextPosition);
        Animal nextAnimal = level.GetAnimal(nextPosition);

        // if the next tile is a wall, we can't go there
        if (nextType == Tile.Type.Wall)
            return false;

        // if the next tile contains a movable animal that doesn't match, we can't go there
        if (nextAnimal is MovableAnimal && !IsPairWith((MovableAnimal)nextAnimal))
            return false;

        // in all other cases, we can move in the given direction (although we might die then)
        return true;
    }

    /// <summary>
    /// Tries to move this MovableAnimal into the given direction.
    /// </summary>
    /// <param name="direction">A direction to move in.</param>
    public void TryMoveInDirection(Point direction)
    {
        // move the animal as long as it can move in that direction
        Point positionBeforeMoving = currentGridPosition;
        while (CanMoveInDirection(direction))
            currentGridPosition += direction;

        if (currentGridPosition != positionBeforeMoving)
        {
            // set the target position
            targetWorldPosition = level.GetCellPosition(currentGridPosition.X, currentGridPosition.Y);

            // calculate a velocity
            Vector2 dir = targetWorldPosition - LocalPosition;
            dir.Normalize();
            velocity = dir * speed;

            // remove the animal from its current position in the grid. We'll re-add somewhere it when it has stopped moving.
            level.RemoveAnimalFromGrid(positionBeforeMoving);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // if we're moving, check if we've reached our destination
        if (IsMoving && Vector2.Distance(LocalPosition, targetWorldPosition) < speed * gameTime.ElapsedGameTime.TotalSeconds)
        {
            // store the new position in the grid
            ApplyCurrentPosition();
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
}