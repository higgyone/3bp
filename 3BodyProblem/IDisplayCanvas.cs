using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _3BodyProblem
{
    /// <summary>
    /// ICanvasDisplay interface for putting stuff on the main display canvas
    /// </summary>
    public interface IDisplayCanvas
    {
        /// <summary>
        /// add an element to the canvas
        /// </summary>
        /// <param name="element">uielement to add to the canvas</param>
        void AddCanvasChild(FrameworkElement element);

        /// <summary>
        /// remove a child element from the display canvas 
        /// </summary>
        /// <param name="element">element to remove</param>
        void RemoveCanvasChild(FrameworkElement element);

        /// <summary>
        /// does the canvas contain the element
        /// </summary>
        /// <param name="element">element to check for</param>
        /// <returns>true if canvas contains the element</returns>
        bool CanvasChildContainsElement(FrameworkElement element);

        /// <summary>
        /// get the visual canvas
        /// </summary>
        /// <returns>the visual canvas</returns>
        Canvas GetVisualCanvas();

        /// <summary>
        /// clear the canvas of all elements
        /// </summary>
        void ClearCanvas();

        /// <summary>
        /// get the canvas width
        /// </summary>
        /// <returns>canvas width</returns>
        double GetCanvasWidth();

        /// <summary>
        /// get the canvas height
        /// </summary>
        /// <returns>the canvas height</returns>
        double GetCanvasHeight();
    }
}
