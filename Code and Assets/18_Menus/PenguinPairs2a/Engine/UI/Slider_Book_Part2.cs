    ...
    // The difference between the minimum and maximum value that the slider can store.
    float Range { get { return maxValue - minValue; } }

    // The smallest X coordinate that the front image may have.
    float MinimumLocalX { get { return padding + front.Width / 2; } }
    // The largest X coordinate that the front image may have.
    float MaximumLocalX { get { return back.Width - padding - front.Width / 2; } }
    // The total pixel width that is available for the front image.
    float AvailableWidth { get { return MaximumLocalX - MinimumLocalX; } }
	
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        
        if (!Visible)
            return;
		
        Vector2 mousePos = inputHelper.MousePositionWorld;
        if (inputHelper.MouseLeftButtonDown() && back.BoundingBox.Contains(mousePos))
        {
            // translate the mouse position to a number between 0 (left) and 1 (right)
            float correctedX = mousePos.X - GlobalPosition.X - MinimumLocalX;
            float newFraction = correctedX / AvailableWidth;
            // convert that to a new slider value
            Value = newFraction * Range + minValue;
        }
    }

    /// <summary>
    /// Gets or sets the current numeric value that's stored in this slider.
    /// When you set this value, the foreground image will move to the correct position.
    /// </summary>
    public float Value
    {
        get { return currentValue; }
        set
        {
            // store the value
            currentValue = MathHelper.Clamp(value, minValue, maxValue);
            // calculate the new position of the foreground image
            float fraction = (currentValue - minValue) / Range;
            float newX = MinimumLocalX + fraction * AvailableWidth;
            front.LocalPosition = new Vector2(newX, padding);
        }
    }
}
