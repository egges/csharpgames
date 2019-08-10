using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

class Player : AnimatedGameObject
{
    const float walkingSpeed = 400; // Standard walking speed, in game units per second.
    const float jumpSpeed = 900; // Lift-off speed when the character jumps.
    const float gravity = 2300; // Strength of the gravity force that pulls the character down.
    const float maxFallSpeed = 1200; // The maximum vertical speed at which the character can fall.
    
    const float iceFriction = 1; // Friction factor that determines how slippery the ice is; closer to 0 means more slippery.
    const float normalFriction = 20; // Friction factor that determines how slippery a normal surface is.
    const float airFriction = 5; // Friction factor that determines how much (horizontal) air resistance there is.

    bool facingLeft; // Whether or not the character is currently looking to the left.

    bool isGrounded; // Whether or not the character is currently standing on something.
    bool standingOnIceTile, standingOnHotTile; // Whether or not the character is standing on an ice tile or a hot tile.
    float desiredHorizontalSpeed; // The horizontal speed at which the character would like to move.

    Level level;

    public bool IsMoving { get { return velocity != Vector2.Zero; } }

    public Player(Level level) : base(TickTick.Depth_LevelPlayer)
    {
        this.level = level;

        // load all animations
        LoadAnimation("Sprites/LevelObjects/Player/spr_idle", "idle", true, 0.1f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_run@13", "run", true, 0.04f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_jump@14", "jump", false, 0.08f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_celebrate@14", "celebrate", false, 0.05f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_die@5", "die", true, 0.1f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_explode@5x5", "explode", false, 0.04f);

        // start with the idle sprite
        PlayAnimation("idle");
        SetOriginToBottomCenter();
        facingLeft = false;
        isGrounded = true;
        standingOnIceTile = standingOnHotTile = false;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        // arrow keys: move left or right
        if (inputHelper.KeyDown(Keys.Left))
        {
            facingLeft = true;
            desiredHorizontalSpeed = -walkingSpeed;
            if (isGrounded)
                PlayAnimation("run");
        }
        else if (inputHelper.KeyDown(Keys.Right))
        {
            facingLeft = false;
            desiredHorizontalSpeed = walkingSpeed;
            if (isGrounded)
                PlayAnimation("run");
        }

        // no arrow keys: don't move
        else
        {
            desiredHorizontalSpeed = 0;
            if (isGrounded)
                PlayAnimation("idle");
        }

        // spacebar: jump
        if (isGrounded && inputHelper.KeyPressed(Keys.Space))
            Jump();

        // falling?
        if (!isGrounded)
            PlayAnimation("jump", false, 8);

        // set the origin to the character's feet
        SetOriginToBottomCenter();

        // make sure the sprite is facing the correct direction
        sprite.Mirror = facingLeft;
    }

    public void Jump(float speed = jumpSpeed)
    {
        velocity.Y = -speed;
        // play the jump animation; always make sure that the animation restarts
        PlayAnimation("jump", true);
    }

    /// <summary>
    /// Returns whether or not the Player is currently falling down.
    /// </summary>
    public bool IsFalling
    {
        get { return velocity.Y > 0 && !isGrounded; }
    }

    void SetOriginToBottomCenter()
    {
        Origin = new Vector2(sprite.Width / 2, sprite.Height);
    }

    public override void Update(GameTime gameTime)
    {
        Vector2 previousPosition = localPosition;

        // determine the friction coefficient for the character
        float friction;
        if (standingOnIceTile)
            friction = iceFriction;
        else if (isGrounded)
            friction = normalFriction;
        else
            friction = airFriction;

        // calculate how strongly the horizontal speed should move towards the desired value
        float multiplier = MathHelper.Clamp(friction * (float)gameTime.ElapsedGameTime.TotalSeconds, 0, 1);

        // update the horizontal speed
        velocity.X += (desiredHorizontalSpeed - velocity.X) * multiplier;
        if (Math.Abs(velocity.X) < 1)
            velocity.X = 0;

        ApplyGravity(gameTime);
        base.Update(gameTime);

        HandleTileCollisions(previousPosition);
    }

    void ApplyGravity(GameTime gameTime)
    {
        velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (velocity.Y > maxFallSpeed)
            velocity.Y = maxFallSpeed;
    }

    // Checks for collisions between the character and the level's tiles, and handles these collisions when needed.
    void HandleTileCollisions(Vector2 previousPosition)
    {
        isGrounded = false;
        standingOnIceTile = false;
        standingOnHotTile = false;

        // determine the range of tiles to check
        Rectangle bbox = BoundingBoxForCollisions;
        Point topLeftTile = level.GetTileCoordinates(new Vector2(bbox.Left, bbox.Top)) - new Point(1, 1);
        Point bottomRightTile = level.GetTileCoordinates(new Vector2(bbox.Right, bbox.Bottom)) + new Point(1, 1);

        for (int y = topLeftTile.Y; y <= bottomRightTile.Y; y++)
        {
            for (int x = topLeftTile.X; x <= bottomRightTile.X; x++)
            {
                Tile.Type tileType = level.GetTileType(x, y);

                // ignore empty tiles
                if (tileType == Tile.Type.Empty)
                    continue;

                // ignore platform tiles if the player is standing below them
                Vector2 tilePosition = level.GetCellPosition(x, y);
                if (tileType == Tile.Type.Platform && localPosition.Y > tilePosition.Y && previousPosition.Y > tilePosition.Y)
                    continue;

                // if there's no intersection with the tile, ignore this tile
                Rectangle tileBounds = new Rectangle((int)tilePosition.X, (int)tilePosition.Y, Level.TileWidth, Level.TileHeight);
                if (!tileBounds.Intersects(bbox))
                    continue;

                // calculate how large the intersection is
                Rectangle overlap = CollisionDetection.CalculateIntersection(bbox, tileBounds);

                // if the x-component is smaller, treat this as a horizontal collision
                if (overlap.Width < overlap.Height)
                {
                    if ((velocity.X >= 0 && bbox.Center.X < tileBounds.Left) || // right wall
                        (velocity.X <= 0 && bbox.Center.X > tileBounds.Right)) // left wall
                    {
                        localPosition.X = previousPosition.X;
                        velocity.X = 0;
                    }
                }

                // otherwise, treat this as a vertical collision
                else
                {
                    if (velocity.Y >= 0 && bbox.Center.Y < tileBounds.Top && overlap.Width > 6) // floor
                    {
                        isGrounded = true;
                        velocity.Y = 0;
                        localPosition.Y = tileBounds.Top;

                        // check the surface type: are we standing on a hot tile or an ice tile?
                        Tile.SurfaceType surface = level.GetSurfaceType(x, y);
                        if (surface == Tile.SurfaceType.Hot)
                            standingOnHotTile = true;
                        else if (surface == Tile.SurfaceType.Ice)
                            standingOnIceTile = true;
                    }
                    else if (velocity.Y <= 0 && bbox.Center.Y > tileBounds.Bottom && overlap.Height > 2) // ceiling
                    {
                        localPosition.Y = previousPosition.Y;
                        velocity.Y = 0;
                    }
                }
            }
        }
    }

    Rectangle BoundingBoxForCollisions
    {
        get
        {
            Rectangle bbox = BoundingBox;
            // adjust the bounding box
            bbox.X += 20;
            bbox.Width -= 40;
            bbox.Height += 1;

            return bbox;
        }
    }

    public void Die()
    {
        // TODO: This method will be filled in the next chapter.
    }
}