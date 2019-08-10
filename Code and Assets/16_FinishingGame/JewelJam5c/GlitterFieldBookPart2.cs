    ...

    public override void Update(GameTime gameTime)
    {
        // update each glitter
        for (int i = 0; i < positions.Count; i++)
        {
            // Let the glitter grow. If the glitter is currently invisible, 
            // it has a small chance to start growing.
            if (scales[i] > 0 || ExtendedGame.Random.NextDouble() < 0.001)
            {
                scales[i] += 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                // If the glitter has reached scale 2, initialize a new random glitter.
                if (scales[i] >= 2.0f)
                {
                    scales[i] = 0f;
                    positions[i] = CreateRandomPosition();
                }
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Vector2 glitterCenter = new Vector2(glitter.Width, glitter.Height) / 2;
        for (int i = 0; i < scales.Count; i++)
        {
            float scale = scales[i];
            // a scale between 1 and 2 means that the glitter is shrinking again
            if (scales[i] > 1)
                scale = 2 - scales[i];
            
            // draw the glitter at its current scale
            spriteBatch.Draw(glitter, GlobalPosition + positions[i], null, 
                Color.White, 0f, glitterCenter, scale, SpriteEffects.None, 0);
        }
    }
}
