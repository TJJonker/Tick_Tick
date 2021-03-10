using Microsoft.Xna.Framework;

namespace Engine
{
    public struct Circle
    {
        // Radius of the circle
        public float Radius { get; private set; }

        // Center of the circle
        public Vector2 Center { get; private set; }

        private Circle(float radius, Vector2 center)
        {
            Radius = radius;
            Center = center;
        }
    }
}