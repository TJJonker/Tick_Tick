using Microsoft.Xna.Framework;

namespace Engine.UI
{
    public class Slider : GameObjectList
    {
        // Sprites for the fore and background of the slider
        private SpriteGameObject back, front;

        // The minimum and maximum value of the slider
        private float minValue, maxValue;

        // The current value and the value in the previous frame
        private float currentValue, previousValue;

        // The number of pixels the foreground stays away from the border
        private float padding;

        // The difference between the minimum and maximum value that the slider can store
        private float Range { get { return maxValue - minValue; } }

        // The smallest X coordinate the front image may have
        private float minimumLocalX { get { return padding + front.Width / 2; } }

        // The largest X coordinatie the front image may have
        private float maximumLocalX { get { return back.Width - padding - front.Width / 2; } }

        // The total width that is available for the front image
        private float availableWidth { get { return maximumLocalX - minimumLocalX; } }

        /// <summary>
        /// Returns whether the slider's value has changed last frame
        /// </summary>
        public bool ValueChanged
        {
            get { return currentValue != previousValue; }
        }

        /// <summary>
        /// Gets or sets the current numeric value that's stored in this slider
        /// When the value is changed, the foreground image will move to the correct position
        /// </summary>
        public float Value
        {
            get { return currentValue; }
            set
            {
                currentValue = MathHelper.Clamp(value, minValue, maxValue);
                float fraction = (currentValue - minValue) / Range;
                float newX = minimumLocalX + fraction * availableWidth;
                front.Position = new Vector2(newX, padding);
            }
        }

        public Slider(string backGroundsprite, string foreGroundSprite, float minValue, float maxValue, float padding)
        {
            // Add the background sprite
            back = new SpriteGameObject(backGroundsprite, 0.9f);
            AddChild(back);

            // Add the foreground sprite
            front = new SpriteGameObject(foreGroundSprite, 0.95f);
            front.Origin = new Vector2(front.Width / 2, 0);
            AddChild(front);

            // Store the other values
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.padding = padding;

            // By default, start with the minimum value
            previousValue = this.minValue;
            Value = previousValue;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (!Visible) return;

            // Stores the previous slider value
            previousValue = Value;

            Vector2 mousePos = inputHelper.MousePositionWorld;
            if (inputHelper.MouseLeftButtonDown() && back.BoundingBox.Contains(mousePos))
            {
                // Translate the mouse position to a number between 0 and 1
                float correctedX = mousePos.X - GlobalPosition.X - minimumLocalX;
                float newFraction = correctedX / availableWidth;
                // Converts that to a new slider value
                Value = newFraction * Range + minValue;
            }
        }
    }
}