using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    /// <summary>
    /// A class that can represent a game object with a sprite
    /// </summary>
    public class SpriteGameObject : GameObject
    {
        /// <summary>
        /// The sprite that will be drawn on the screen.
        /// </summary>
        protected SpriteSheet sprite;

        /// <summary>
        /// Gets the width of the sprite in the game world, according to its sprite
        /// </summary>
        public int Width { get { return sprite.Width; } }

        /// <summary>
        /// Gets the height of the sprite in the game world, according to its sprite
        /// </summary>
        public int Height { get { return sprite.Height; } }

        /// <summary>
        /// The origin offset to use when the sprite gets drawn on screen
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// The sheetindex of the attached sprite sheet
        /// </summary>
        public int SheetIndex
        {
            get { return sprite.SheetIndex; }
            set { sprite.SheetIndex = value; }
        }

        /// <summary>
        /// The depth (number between 0 and 1) at which the obejct should be drawn
        /// A larger number will be drawn on top
        /// </summary>
        protected float depth;

        /// <summary>
        /// Creates a new <see cref="SpriteGameObject"/> with the given sprite name, 
        /// at he given depth and the given sheetindex (if applicable)
        /// </summary>
        /// <param name="spriteName">The name of the sprite to load</param>
        /// <param name="depth">The depth at which the object should be drawn</param>
        /// <param name="sheetIndex">The sheet index of the sprite to use initially</param>
        public SpriteGameObject(string spriteName, float depth, int sheetIndex = 0)
        {
            sprite = new SpriteSheet(spriteName);
            Origin = Vector2.Zero;
            SheetIndex = sheetIndex;
        }

        /// <summary>
        /// Returns a rectangle that describes this game object's bounding box
        /// Usefull for collision detection
        /// </summary>
        public Rectangle BoundingBox
        {
            get
            {
                Rectangle spriteBounds = sprite.Bounds;
                spriteBounds.Offset(GlobalPosition - Origin);
                return spriteBounds;
            }
        }

        /// <summary>
        /// Draws this object on the screen, using its global position and origin
        /// </summary>
        /// <param name="gameTime">An object containing information about the time that has passed in the game</param>
        /// <param name="spriteBatch">A sprite batch object used for drawing sprites</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;

            if(sprite != null)
                sprite.Draw(spriteBatch, GlobalPosition, Origin);
        }

        /// <summary>
        /// Updates the origin so that it lies in the center of the sprite
        /// </summary>
        public void SetOriginToCenter()
        {
            Origin = sprite.Center;
        }

        public bool HasPixelPreciseCollision(Rectangle other)
        {
            Rectangle b = CollisionDetection
        }
    }
}