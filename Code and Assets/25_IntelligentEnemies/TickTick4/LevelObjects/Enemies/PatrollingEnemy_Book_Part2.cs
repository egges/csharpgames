    ...

    /// <summary>
    /// Checks and returns if this enemy can keep moving in its current direction.
    /// </summary>
    protected bool CanMoveForward()
    {
        // determine the position to check
        Rectangle bbox = BoundingBox;
        Vector2 side;
        side.Y = bbox.Bottom + 1;
        if (sprite.Mirror)
            side.X = bbox.Left;
        else
            side.X = bbox.Right;
        
        Point tilePos = level.GetTileCoordinates(side);

        // we can continue moving if there's a platform below, and no wall ahead
        return level.GetTileType(tilePos.X, tilePos.Y) != Tile.Type.Empty
            && level.GetTileType(tilePos.X, tilePos.Y - 1) != Tile.Type.Wall;
    }

    /// <summary>
    /// Starts moving the enemy in the other direction.
    /// </summary>
    protected void TurnAround()
    {
        sprite.Mirror = !sprite.Mirror;
        velocity.X = walkSpeed;
        if (sprite.Mirror)
            velocity.X *= -1;
    }
}
