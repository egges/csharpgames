using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

    ...