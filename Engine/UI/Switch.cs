namespace Engine.UI
{
    /// <summary>
    /// A class that van represent a UI switch that can be turner on or off
    /// </summary>
    public class Switch : Button
    {
        private bool selected;

        /// <summary>
        /// Whether or not this switch is turned on or off
        /// When toggled, the switch will receive a new sprite
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

        /// <summary>
        /// Creates a new <see cref="Switch"/> with the given sprite and depth
        /// </summary>
        /// <param name="assetName">The name of the sprite to use</param>
        /// <param name="depth">The depth aty which the switch should be drawn</param>
        public Switch(string assetName, float depth) : base(assetName, depth)
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