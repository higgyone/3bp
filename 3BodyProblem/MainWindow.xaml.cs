﻿using _3BodyProblem.Body;
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
        private IRenderer renderer;

        /// <summary>
        /// body manager object
        /// </summary>
        private BodyManager bodyManager;

        /// <summary>
        /// time to speed it up by
        /// </summary>
        private uint timeMultiplier = 200;

        /// <summary>
        /// time difference between each update step for positions
        /// </summary>
        private float deltaTime = 0.1f;

        /// <summary>
        /// initialisation
        /// </summary>
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
                new Body.Body(10000000000000f, 25, new Vector2(46000, 10000), new Vector2(0.3f, 0.000000001f)),
                new Body.Body(10000000000000f, 20, new Vector2(39000, 13000), new Vector2(-0.3f, 0.0000000001f)),
                new Body.Body(10000000000000f, 15, new Vector2(33000, 15000), new Vector2(-0.1f, 0.0000000001f))
            );
        }

        /// <summary>
        /// loop that updates everything
        /// </summary>
        /// <param name="elapsedTime">time elapsed since last render</param>
        private void GameLoop(float elapsedTime)
        {

           bodyManager.UpdatePositions3Bodies(deltaTime);

            bodyManager.UpdateBackground(elapsedTime * timeMultiplier, timeMultiplier);

            bodyManager.Render();
        }
    }
}
