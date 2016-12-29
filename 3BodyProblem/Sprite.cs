using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace _3BodyProblem
{
    /// <summary>
    /// sprite object
    /// </summary>
    public class Sprite 
    {
        /// <summary>
        /// the framework element that is the basis of this sprite
        /// </summary>
        public FrameworkElement DisplaySprite { get; private set; }

        /// <summary>
        /// delegate for working out it the sprite has interseted with a point in space
        /// </summary>
        private Func<Point, Vector2, bool> IntersectsSprite;

        /// <summary>
        /// display x position of the sprite
        /// </summary>
        public double XPos { get; private set; }

        /// <summary>
        /// displah y position of the sprite
        /// </summary>
        public double YPos { get; private set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="sprite">framework element base of the sprite</param>
        /// <param name="intersects">delegate to chek if it has intersected with a point in space</param>
        public Sprite(FrameworkElement sprite, Func<Point, Vector2, bool> intersects = null)
        {
            DisplaySprite = sprite;
            IntersectsSprite = intersects;
        }

        /// <summary>
        /// get the displaay sprite
        /// </summary>
        /// <returns></returns>
        public FrameworkElement GetDisplaySprite()
        {
            return DisplaySprite;
        }

        /// <summary>
        /// set the position of the sprite
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        public void SetPosition(double x, double y)
        {
            Canvas.SetLeft(DisplaySprite, x);
            Canvas.SetTop(DisplaySprite, y);

            XPos = x;
            YPos = y;
        }

        /// <summary>
        /// set the display position of the sprite
        /// </summary>
        /// <param name="position">2D vector postion of the sprite</param>
        public void SetPosition(Vector2 position)
        {
            Canvas.SetLeft(DisplaySprite, position.X);
            Canvas.SetTop(DisplaySprite, position.Y);

            XPos = position.X;
            YPos = position.Y;
        }

        /// <summary>
        /// get the 2D vector position of the sprite
        /// </summary>
        /// <returns>2D vector of display position</returns>
        public Vector2 GetSpritePosition()
        {
            //double top = (double)DisplaySprite.GetValue(Canvas.TopProperty);
            return new Vector2((float)Canvas.GetLeft(DisplaySprite), (float)Canvas.GetTop(DisplaySprite));
            //return new Vector(XPos, YPos);
        }

        /// <summary>
        /// get the width of the sprite
        /// </summary>
        /// <returns>double width of the sprite</returns>
        public double GetSpriteWidth()
        {
            return DisplaySprite.Width;
        }

        /// <summary>
        /// get the height of the sprite
        /// </summary>
        /// <returns>double height of the sprite</returns>
        public double GetSpriteHeight()
        {
            return DisplaySprite.Height;
        }

        /// <summary>
        /// set the width of the sprite
        /// </summary>
        /// <param name="width">double sprite width</param>
        public void SetSpriteWidth(double width)
        {
            DisplaySprite.Width = width;
        }

        /// <summary>
        /// set the height of the sprite
        /// </summary>
        /// <param name="height">double height</param>
        public void SetSpriteHeight(double height)
        {
            DisplaySprite.Height = height;
        }

        /// <summary>
        /// get the center of the sprite
        /// </summary>
        /// <returns>2D vector of sprite center position</returns>
        public Vector2 GetSpriteCentrePosition()
        {
            Vector2 topLeft = GetSpritePosition();
            return new Vector2((float)(topLeft.X + GetSpriteWidth() / 2), (float)(topLeft.Y + GetSpriteHeight() / 2));
        }

        /// <summary>
        /// check if the sprite has intersected with a point in space
        /// </summary>
        /// <param name="point">point to check for intersection</param>
        /// <returns>true if point intersects with sprite</returns>
        public bool Intersects(Point point)
        {
            if (IntersectsSprite == null)
            {
                return false;
            }
            return IntersectsSprite(point, GetSpriteCentrePosition());
        }
    }
}
