using System.Windows;
using System.Windows.Controls;

namespace _3BodyProblem
{
    public class DisplayCanvas : IDisplayCanvas
    {
        /// <summary>
        /// display canvas
        /// </summary>
        private Canvas displayCanvas;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="canvas">canvas to display eveything on</param>
        public DisplayCanvas(Canvas canvas)
        {
            this.displayCanvas = canvas;
        }

        /// <inherit/>
        public void AddCanvasChild(FrameworkElement element)
        {
            displayCanvas.Children.Add(element);
        }

        /// <inherit/>
        public void RemoveCanvasChild(FrameworkElement element)
        {
            if (displayCanvas.Children.Contains(element))
            {
                displayCanvas.Children.Remove(element);
            }
        }

        /// <inherit/>
        public Canvas GetVisualCanvas()
        {
            return displayCanvas;
        }

        /// <inherit/>
        public bool CanvasChildContainsElement(FrameworkElement element)
        {
            return displayCanvas.Children.Contains(element);
        }

        /// <inherit/>
        public void ClearCanvas()
        {
            if (displayCanvas.Children.Count > 0)
            {
                displayCanvas.Children.RemoveRange(0, displayCanvas.Children.Count);
            }
        }

        /// <inherit/>
        public double GetCanvasWidth()
        {
            return displayCanvas.ActualWidth;
        }

        /// <inherit/>
        public double GetCanvasHeight()
        {
            return displayCanvas.ActualHeight;
        }
    }
}
