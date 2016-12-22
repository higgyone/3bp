using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3BodyProblem
{
    public class Renderer
    {
        public readonly IDisplayCanvas displayCanvas;

        public Renderer(IDisplayCanvas displayCanvas)
        {
            this.displayCanvas = displayCanvas;
        }

        public void DrawSprite(Sprite[] sprite)
        {
            foreach (Sprite s in sprite)
            {
                displayCanvas.AddCanvasChild(s.DisplaySprite);
            }
        }

        public void RemoveSprite(Sprite sprite)
        {
            displayCanvas.RemoveCanvasChild(sprite.DisplaySprite);
        }

        public double GetRenderWidth()
        {
            return displayCanvas.GetCanvasWidth();
        }

        public double GetRenderHeight()
        {
            return displayCanvas.GetCanvasHeight();
        }

        public void ClearCanvas()
        {
            displayCanvas.ClearCanvas();
        }
    }
}
