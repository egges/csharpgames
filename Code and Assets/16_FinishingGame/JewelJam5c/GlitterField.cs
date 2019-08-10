using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// A field of glitters that are randomly drawn on top of another sprite.
/// </summary>
class GlitterField : GameObject
{
    // The image of a single glitter
    Texture2D glitter;

    // The target image on which the glitter effect should be applied
    Texture2D target;
    // The rectangle within the target image that should receive glitters
    Rectangle targetRectangle;

    // The random positions of glitters in the field
    List<Vector2> positions;
    // The current scales of the glitters; these are numbers between 0 and 2
    List<float> scales;

    /// <summary>
    /// Creates a new GlitterField that can cover a certain part of the target sprite.
    /// </summary>
    /// <param name="target">The sprite on which the glitters should be applied.</param>
    /// <param name="numberOfGlitters">The total number of glitters to apply.</param>
    /// <param name="targetRectangle">The part of the sprite that should receive glitters.</param>
    public GlitterField(Texture2D target, int numberOfGlitters, Rectangle targetRectangle)
    {
        // load the glitter sprite
        glitter = ExtendedGame.ContentManager.Load<Texture2D>("spr_glitter");

        // initialize some member variables
        this.target = target;
        this.targetRectangle = targetRectangle;
        positions = new List<Vector2>();
        scales = new List<float>();

        // create random glitters
        for (int i = 0; i < numberOfGlitters; i++)
        {
            positions.Add(CreateRandomPosition());
            scales.Add(0f);
        }
    }

    Vector2 CreateRandomPosition()
    {
        // keep trying random positions until a valid one is found
        while (true)
        {
            // draw a random position within the target rectangle
            Point randomPos = new Point(
                ExtendedGame.Random.Next(targetRectangle.Width),
                ExtendedGame.Random.Next(targetRectangle.Height)
            ) + targetRectangle.Location;

            // get the pixel data at that position
            Rectangle rect = new Rectangle(randomPos, new Point(1, 1));
            Color[] retrievedColor = new Color[1];
            target.GetData(0, rect, retrievedColor, 0, 1);

            // if the pixel is fully opaque, accept it as the answer
            if (retrievedColor[0].A == 255)
                return randomPos.ToVector2();
        }
    }

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
