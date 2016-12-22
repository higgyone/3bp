using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace _3BodyProblem.Loop
{
    public class PrecisionTimer
    {
        /// <summary>
        /// import to get the frequency of the processor ticks
        /// </summary>
        /// <param name="performanceFrequency">number of ticks per second</param>
        /// <returns>true if all ok</returns>
        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        private static extern bool QueryPerformanceFrequency(ref long performanceFrequency);

        /// <summary>
        /// import to et the current tick count
        /// </summary>
        /// <param name="performanceCount">tick count</param>
        /// <returns>true if all ok</returns>
        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        private static extern bool QueryPerformanceCounter(ref long performanceCount);

        /// <summary>
        /// number of ticks per second (frequency)
        /// </summary>
        private long ticksPerSecond = 0;

        /// <summary>
        /// previouse time
        /// </summary>
        private long previousElapsedTime = 0;

        /// <summary>
        /// constructor for precision ti,er
        /// </summary>
        public PrecisionTimer()
        {
            // get then number of ticks per second
            QueryPerformanceFrequency(ref ticksPerSecond);

            // get rid of rubbish first result
            // as previousElapsedTime is rubbish value and needs set
            GetElapsedTime();

        }

        /// <summary>
        /// get the elapsed time since last run
        /// </summary>
        /// <returns>elapsed time in seconds</returns>
        public float GetElapsedTime()
        {
            long time = 0;

            // get the current tick count
            QueryPerformanceCounter(ref time);

            // convert ticks to elapsed time
            float elapsedTime = (float)(time - previousElapsedTime) / (float)ticksPerSecond;
            previousElapsedTime = time;
            return elapsedTime;
        }
    }
}
