using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.UI
{
    /// <summary>
    /// A button which can represent a level at the levelmenu screen
    /// </summary>
    public class LevelButton : Button
    {
        /// <summary>
        /// The index of a level the button represents
        /// </summary>
        public int LevelIndex { get; private set; }
        
        private LevelStatus status;
        protected TextGameObject label;

        /// <summary>
        /// Creates a new <see cref="LevelButton"/> with the given index and status
        /// </summary>
        /// <param name="levelIndex">The index of the level the button represents</param>
        /// <param name="startStatus">The initials status of the associated level</param>
        public LevelButton(int levelIndex, LevelStatus startStatus) : base(null, .9f)
        {
            LevelIndex = levelIndex;
            status = startStatus;
        }

        /// <summary>
        /// Gets or sets the status of this level button.
        /// When you change the status, the button will also receive a new sprite
        /// </summary>
        public LevelStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                sprite = new SpriteSheet(GetSpriteNameForStatus(status), depth);
                sprite.SheetIndex = (LevelIndex - 1) % sprite.NumberOfSheetElements;
            }
        }

        /// <summary>
        /// Returns a string with the path to the designated status sprite
        /// </summary>
        /// <param name="status">The status for the needed string</param>
        /// <returns>A string with the path to the status sprite</returns>
        protected virtual string getSpriteNameForStatus(LevelStatus status)
        {
            if (status == LevelStatus.Locked)
                return "Sprites/UI/spr_level_locked";
            if (status == LevelStatus.Unlocked)
                return "Sprites/UI/spr_level_locked";
            return "Sprites/UI/spr_level_solved";
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (label != null)
                label.Draw(gameTime, spriteBatch);
        }
    }
}