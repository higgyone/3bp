namespace _3BodyProblem
{
    /// <summary>
    /// class to put stuff on the display canvas
    /// </summary>
    public class Renderer : IRenderer
    {
        /// <summary>
        /// display canvas where everything is drawn
        /// </summary>
        private readonly IDisplayCanvas displayCanvas;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="displayCanvas">canvas to diaply everything on</param>
        public Renderer(IDisplayCanvas displayCanvas)
        {
            this.displayCanvas = displayCanvas;
        }

        /// <inherit/>
        public void DrawSprite(Sprite[] sprite)
        {
            foreach (Sprite s in sprite)
            {
                displayCanvas.AddCanvasChild(s.DisplaySprite);
            }
        }

        /// <inherit/>
        public void RemoveSprite(Sprite sprite)
        {
            displayCanvas.RemoveCanvasChild(sprite.DisplaySprite);
        }

        /// <inherit/>
        public double GetRenderWidth()
        {
            return displayCanvas.GetCanvasWidth();
        }

        /// <inherit/>
        public double GetRenderHeight()
        {
            return displayCanvas.GetCanvasHeight();
        }

        /// <inherit/>
        public void ClearCanvas()
        {
            displayCanvas.ClearCanvas();
        }

        /// <inherit/>
        public IDisplayCanvas GetDisplayCanvas()
        {
            return displayCanvas;
        }
    }
}
