using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.Numerics;

namespace _3BodyProblem
{
    public class Sprite
    {
        public FrameworkElement DisplaySprite { get; private set; }
        private Func<Point, Vector2, bool> IntersectsSprite;

        public double XPos { get; private set; }
        public double YPos { get; private set; }

        public Sprite(FrameworkElement sprite, Func<Point, Vector2, bool> intersects = null)
        {
            DisplaySprite = sprite;
            IntersectsSprite = intersects;
        }

        public FrameworkElement GetDisplaySprite()
        {
            return DisplaySprite;
        }

        public void SetPosition(double x, double y)
        {
            Canvas.SetLeft(DisplaySprite, x);
            Canvas.SetTop(DisplaySprite, y);

            XPos = x;
            YPos = y;
        }

        public void SetPosition(Vector2 position)
        {
            Canvas.SetLeft(DisplaySprite, position.X);
            Canvas.SetTop(DisplaySprite, position.Y);

            XPos = position.X;
            YPos = position.Y;
        }

        public Vector2 GetSpritePosition()
        {
            //double top = (double)DisplaySprite.GetValue(Canvas.TopProperty);
            return new Vector2((float)Canvas.GetLeft(DisplaySprite), (float)Canvas.GetTop(DisplaySprite));
            //return new Vector(XPos, YPos);
        }

        public double GetSpriteWidth()
        {
            return DisplaySprite.Width;
        }

        public double GetSpriteHeight()
        {
            return DisplaySprite.Height;
        }

        public void SetSpriteWidth(double width)
        {
            DisplaySprite.Width = width;
        }

        public void SetSpriteHeight(double height)
        {
            DisplaySprite.Height = height;
        }

        public Vector2 GetSpriteCentrePosition()
        {
            Vector2 topLeft = GetSpritePosition();
            return new Vector2((float)(topLeft.X + GetSpriteWidth() / 2), (float)(topLeft.Y + GetSpriteHeight() / 2));
        }

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
