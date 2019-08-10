using Microsoft.Xna.Framework;

class Slider : GameObjectList
{
    // The sprites for the background and foreground of this slider.
    SpriteGameObject back, front;

    // The minimum and maximum value associated to this slider.
    float minValue, maxValue;
    // The value that the slider is currently storing.
    float currentValue;

    // The number of pixels that the foreground block should stay away from the border.
    float padding;

    public Slider(string backgroundSprite, string foregroundSprite, 
        float minValue, float maxValue, float padding)
    {
        // add the background image
        back = new SpriteGameObject(backgroundSprite);
        AddChild(back);

        // add the foreground image, with a custom origin
        front = new SpriteGameObject(foregroundSprite);
        front.Origin = new Vector2(front.Width / 2, 0);
        AddChild(front);

        // store the other values
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.padding = padding;

        // by default, start at the minimum value
        Value = this.minValue;
    }
    ...
