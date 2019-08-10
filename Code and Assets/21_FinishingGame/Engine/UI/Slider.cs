using Microsoft.Xna.Framework;

namespace Engine.UI
{
    public class Slider : GameObjectList
    {
        // The sprites for the background and foreground of this slider.
        SpriteGameObject back, front;

        // The minimum and maximum value associated to this slider.
        float minValue, maxValue;

        // The value that the slider is currently storing.
        float currentValue;

        // The value that the slider had in the previous frame.
        float previousValue;

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
            previousValue = this.minValue;
            Value = previousValue;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (!Visible)
                return;

            Vector2 mousePos = inputHelper.MousePositionWorld;

            // store the previous slider value as a back-up
            previousValue = Value;

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

        /// <summary>
        /// Returns whether the slider's value has changed in the last frame of the game loop.
        /// </summary>
        public bool ValueChanged
        {
            get { return currentValue != previousValue; }
        }

        // The difference between the minimum and maximum value that the slider can store.
        float Range { get { return maxValue - minValue; } }

        // The smallest X coordinate that the front image may have.
        float MinimumLocalX { get { return padding + front.Width / 2; } }

        // The largest X coordinate that the front image may have.
        float MaximumLocalX { get { return back.Width - padding - front.Width / 2; } }

        // The total pixel width that is available for the front image.
        float AvailableWidth { get { return MaximumLocalX - MinimumLocalX; } }
    }
}