using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _3BodyProblem
{
    public class DisplayCanvas : IDisplayCanvas
    {
        private Canvas displayCanvas;

        public DisplayCanvas(Canvas canvas)
        {
            this.displayCanvas = canvas;
        }

        public void AddCanvasChild(FrameworkElement element)
        {
            displayCanvas.Children.Add(element);
        }

        public void RemoveCanvasChild(FrameworkElement element)
        {
            if (displayCanvas.Children.Contains(element))
            {
                displayCanvas.Children.Remove(element);
            }
        }

        public Canvas GetVisualCanvas()
        {
            return displayCanvas;
        }

        public bool CanvasChildContainsElement(FrameworkElement element)
        {
            return displayCanvas.Children.Contains(element);
        }

        public void ClearCanvas()
        {
            if (displayCanvas.Children.Count > 0)
            {
                displayCanvas.Children.RemoveRange(0, displayCanvas.Children.Count);
            }
        }

        public double GetCanvasWidth()
        {
            return displayCanvas.ActualWidth;
        }

        public double GetCanvasHeight()
        {
            return displayCanvas.ActualHeight;
        }
    }
}
