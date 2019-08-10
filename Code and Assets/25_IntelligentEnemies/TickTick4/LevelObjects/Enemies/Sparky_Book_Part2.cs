    ...

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (timeUntilDrop > 0)
        {
            // decrease the timer. If enough time has passed, start falling down
            timeUntilDrop -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeUntilDrop <= 0)
            {
                PlayAnimation("electrocute");
                velocity.Y = fallSpeed;
            }
        }
        else
        {
            // stop falling?
            if (velocity.Y > 0 && localPosition.Y >= basePosition.Y)
                velocity.Y = 0;

            // start rising?
            if (velocity.Y == 0 && (sprite as Animation).AnimationEnded)
                velocity.Y = -riseSpeed;
            
            // stop rising?
            if (velocity.Y < 0 && localPosition.Y <= basePosition.Y - floatingHeight)
                Reset();

            // electrocute the player?
            if (IsDeadly && HasPixelPreciseCollision(level.Player))
                level.Player.Die();
        }
    }

    /// <summary>
    /// Returns whether this Sparky instance is currently harmful to the player.
    /// This is true during certain frames of the "electrocute" animation.
    /// </summary>
    bool IsDeadly
    {
        get { return timeUntilDrop <= 0 && SheetIndex >= 11 && SheetIndex <= 27; }
    }
}
