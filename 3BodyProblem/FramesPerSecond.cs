using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3BodyProblem
{
    /// <summary>
    /// class that works out the frames per second
    /// </summary>
    public class FramesPerSecond
    {
        /// <summary>
        /// number of frames counted per second
        /// </summary>
        private int numberOfFrames = 0;

        /// <summary>
        /// total time elapsed
        /// </summary>
        private float timePassed = 0;

        /// <summary>
        /// current frames per second
        /// </summary>
        public float CurrentFPS { get; private set; }

        /// <summary>
        /// process the number of frames per second
        /// </summary>
        /// <param name="timeElapsed">elapsed time</param>
        public void Process(float timeElapsed)
        {
            numberOfFrames++;
            timePassed = timePassed + timeElapsed;

            // reset after 1 second
            if (timePassed > 1)
            {
                CurrentFPS = (float)numberOfFrames / timePassed;
                timePassed = 0;
                numberOfFrames = 0;
            }
        }
    }
}
