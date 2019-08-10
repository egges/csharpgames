using Microsoft.Xna.Framework;
using System;

namespace Engine
{
    public class CollisionDetection
    {
        /// <summary>
        /// Checks and returns whether two circles intersect.
        /// </summary>
        public static bool ShapesIntersect(Circle circle1, Circle circle2)
        {
            return Vector2.Distance(circle1.Center, circle2.Center) <= circle1.Radius + circle2.Radius;
        }

        /// <summary>
        /// Checks and returns whether two rectangles intersect.
        /// </summary>
        public static bool ShapesIntersect(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Intersects(rectangle2);
        }

        /// <summary>
        /// Checks and returns whether a rectangle and a circle intersect.
        /// </summary>
        public static bool ShapesIntersect(Rectangle rectangle, Circle circle)
        {
            Vector2 closestPoint;
            closestPoint.X = MathHelper.Clamp(circle.Center.X, rectangle.Left, rectangle.Right);
            closestPoint.Y = MathHelper.Clamp(circle.Center.Y, rectangle.Top, rectangle.Bottom);

            return Vector2.Distance(circle.Center, closestPoint) <= circle.Radius;
        }

        /// <summary>
        /// Calculates and returns the overlapping part of two input rectangles. 
        /// If the rectangles don't overlap at all, 
        /// this method returns a 'dummy' rectangle with a width and height of 0.
        /// </summary>
        public static Rectangle CalculateIntersection(Rectangle rect1, Rectangle rect2)
        {
            if (!ShapesIntersect(rect1, rect2))
                return new Rectangle(0,0,0,0);

            int xmin = Math.Max(rect1.Left, rect2.Left);
            int xmax = Math.Min(rect1.Right, rect2.Right);
            int ymin = Math.Max(rect1.Top, rect2.Top);
            int ymax = Math.Min(rect1.Bottom, rect2.Bottom);
            return new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
        }

    }
}
