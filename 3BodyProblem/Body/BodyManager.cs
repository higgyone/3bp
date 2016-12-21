using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3BodyProblem.Calculations;
using System.Threading;

namespace _3BodyProblem.Body
{
    public class BodyManager
    {
        /// <summary>
        /// is this the first run and need to calculate initial acceleration?
        /// </summary>
        private bool firstRun = true;

        /// <summary>
        /// calculation position vertlet 2D
        /// </summary>
        private PositonVertlet2D posVer = new PositonVertlet2D();

        /// <summary>
        /// body 1
        /// </summary>
        public Body body1 { get; private set; }

        /// <summary>
        /// body 2
        /// </summary>
        public Body body2 { get; private set; }

        public BodyManager(Body body1, Body body2)
        {
            this.body1 = body1;
            this.body2 = body2;
        }

        public void UpdatePositions(float deltaTime)
        {
            posVer.SetDeltaTime(deltaTime);

            if (firstRun)
            {
                // calculate acceleration dependant on second body
                body1.Acceleration = posVer.CalculateAcceleration(body2.Mass, body1.Position, body2.Position);
                body2.Acceleration = posVer.CalculateAcceleration(body1.Mass, body2.Position, body1.Position);

                // use acceleration value to calcualte XHalf initial
                body1.PositionHalf = posVer.CalcualteInitialXhalf(body1.Position, body1.Velocity, body1.Acceleration);
                body2.PositionHalf = posVer.CalcualteInitialXhalf(body2.Position, body2.Velocity, body2.Acceleration);
            }

            // calculate an+1/2 values from either x1/2 or xn+1/2
            body1.AccelerationHalf = posVer.CalculateAcceleration(body2.Mass, body1.PositionHalf, body2.PositionHalf);
            body2.AccelerationHalf = posVer.CalculateAcceleration(body1.Mass, body2.PositionHalf, body1.PositionHalf);

            // calculate vn+1 using an+1/2 eq 23 
            body1.Velocity = posVer.CalculateVelocityNPlusOne(body1.Velocity, body1.AccelerationHalf);
            body2.Velocity = posVer.CalculateVelocityNPlusOne(body2.Velocity, body2.AccelerationHalf);

            // calculate xn+1 using vn+1 eq 24
            body1.Position = posVer.CalculatePositionPlusOne(body1.PositionHalf, body1.Velocity);
            body2.Position = posVer.CalculatePositionPlusOne(body2.PositionHalf, body2.Velocity);

            // calculate next xn+1/2 using xn+1 eq 22
            body1.PositionHalf = posVer.CalculatePositionNPlusHalf(body1.Position, body1.Velocity);
            body2.PositionHalf = posVer.CalculatePositionNPlusHalf(body2.Position, body2.Velocity);

        }
    }
}
