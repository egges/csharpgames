namespace Engine.UI
{
    public class Switch : Button
    {
        bool selected;
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

        public Switch(string assetName) : base(assetName)
        {
            Selected = false;
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