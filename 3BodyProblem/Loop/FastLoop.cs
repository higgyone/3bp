using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _3BodyProblem.Loop
{
    /// <summary>
    /// class that runs the game loop, checks the application is idle 
    /// and returns the elapsed time since last loop
    /// </summary>
    public class FastLoop
    {
        /// <summary>
        /// precision timer timer for game
        /// </summary>
        private PrecisionTimer precisionTimer = new PrecisionTimer();

        /// <summary>
        /// callback for game loop
        /// </summary>
        /// <param name="elapsedTime">elapsed time since last loop</param>
        public delegate void LoopCallback(float elapsedTime);

        /// <summary>
        /// loop callback
        /// </summary>
        private LoopCallback loopCallback;

        /// <summary>
        /// constructor for fastloop
        /// </summary>
        /// <param name="callback">method to run in each loop</param>
        public FastLoop(LoopCallback callback)
        {
            loopCallback = callback;

            CompositionTarget.Rendering += OnApplicationRender;
        }

        /// <summary>
        /// run the fast loop everytime it is about to render
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">event args e</param>
        private void OnApplicationRender(object sender, EventArgs e)
        {
            loopCallback(precisionTimer.GetElapsedTime());
        }
    }
}
