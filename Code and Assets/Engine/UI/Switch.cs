namespace Engine.UI
{
    /// <summary>
    /// A class that can represent a UI switch that can be turned on or off.
    /// It's essentially a button with an extra state that says whether it's turned on.
    /// </summary>
    public class Switch : Button
    {
        protected bool selected;

        /// <summary>
        /// Creates a new <see cref="Switch"/> with the given sprite name and depth.
        /// </summary>
        /// <param name="assetName">The name of the sprite to use.</param>
        /// <param name="depth">The depth at which the object should be drawn.</param>
        public Switch(string assetName, float depth) : base(assetName, depth)
        {
            Selected = false;
        }

        /// <summary>
        /// Whether or not this switch is currently selected.
        /// If you change this value, the switch will receive a different sprite sheet index.
        /// </summary>
        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (selected)
                    SheetIndex = 1;
                else
                    SheetIndex = 0;
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (Pressed)
                Selected = !Selected;
        }

        public override void Reset()
        {
            base.Reset();
            Selected = false;
        }
    }
}