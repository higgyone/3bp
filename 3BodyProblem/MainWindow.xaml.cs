using _3BodyProblem.Body;
using _3BodyProblem.Loop;
using System.Numerics;
using System.Windows;

namespace _3BodyProblem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// fastloop
        /// </summary>
        private FastLoop fastloop;

        /// <summary>
        /// display canvas
        /// </summary>
        private IDisplayCanvas displayCanvas;

        /// <summary>
        /// renderer for the sprites
        /// </summary>
        private Renderer renderer;

        /// <summary>
        /// body manager object
        /// </summary>
        private BodyManager bodyManager;

        /// <summary>
        /// time to speed it up by
        /// </summary>
        private uint timeMultiplier = 100;

        public MainWindow()
        {
            InitializeComponent();

            fastloop = new FastLoop(GameLoop);
            displayCanvas = new DisplayCanvas(canvas);
            renderer = new Renderer(displayCanvas);

            renderer.ClearCanvas();

            bodyManager = new BodyManager
            (
                renderer, 
                new Body.Body(100000f, 25, new Vector2(1, 1), new Vector2(0.9f, 0.001f)),
                new Body.Body(10000000000000f, 35, new Vector2(500, 300), new Vector2(0.00000001f, 0.0000000001f))
            );
        }

        /// <summary>
        /// loop that updates everything
        /// </summary>
        /// <param name="elapsedTime">time elapsed since last render</param>
        private void GameLoop(float elapsedTime)
        {
            bodyManager.UpdatePositions(elapsedTime * timeMultiplier, timeMultiplier);
            bodyManager.Render();
        }
    }
}
