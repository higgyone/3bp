using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace _3BodyProblem.Calculations
{
    public class PositonVertlet2D
    {
        /// <summary>
        /// very small time step between calculations
        /// </summary>
        public float deltaTime { get; private set; }

        /// <summary>
        /// Gravitiational constant
        /// </summary>
        public readonly float G = 0.0000000000667408f;

        /// <summary>
        /// Calculate the initial 2D vector from the initial position, velocity and acceleration vectors
        /// </summary>
        /// <param name="xInitial">initial position vector</param>
        /// <param name="vInitial">initial velocity vector</param>
        /// <param name="aInitial">initial acceleration vector</param>
        /// <returns></returns>
        public Vector2 CalcualteInitialXhalf(Vector2 xInitial, Vector2 vInitial, Vector2 aInitial )
        {
            Vector2 vComp = Vector2.Multiply(0.5f * deltaTime, vInitial);
            Vector2 aComp = Vector2.Multiply(0.25f * (float)Math.Pow(deltaTime, 2), aInitial);

            return Vector2.Add(Vector2.Add(xInitial, vComp), aComp);
        }

        /// <summary>
        /// Calculates the acceleration 2D vector from body1 position heading towards body2 position
        /// </summary>
        /// <param name="mass2">mass of body2</param>
        /// <param name="pos1">position of body1</param>
        /// <param name="pos2">position of body2</param>
        /// <returns>acceleration vector for body1 heading towards body2</returns>
        public Vector2 CalculateAcceleration(float mass2, Vector2 pos1, Vector2 pos2)
        {
            // calculate the r cubed value
            var alpha = CalculateAlpha(pos1, pos2);

            // calculate Big G time Mass constant
            var Gm = G * mass2;

            var xDiff = pos2.X - pos1.X;
            var yDiff = pos2.Y - pos1.Y;

            float accX = (Gm * xDiff) / alpha;
            float accY = (Gm * yDiff) / alpha;

            return new Vector2(accX, accY);
        }

        /// <summary>
        /// calculate the alpha between 2 vectors which is radius cubed (r^3)
        /// </summary>
        /// <param name="selfPosition">1st 2D vector position</param>
        /// <param name="otherBodyPosition">2nd 2D vector position</param>
        public float CalculateAlpha(Vector2 selfPosition, Vector2 otherBodyPosition)
        {
            var alpha = (float)Math.Pow(CalculateDistance(selfPosition, otherBodyPosition), 3);

            // set to return no less than 1 as 0 alpha means the two bodies 
            // have the same position and will have infinite acceleration 
            return Math.Max(alpha, 1);
        }

        /// <summary>
        /// calculate the scalar distance between 2 2D vectors
        /// Eq 18 & 19 and 20 & 21
        /// </summary>
        /// <param name="v1">1st 2D vector position</param>
        /// <param name="v2">2nd 2D vector position</param>
        /// <returns></returns>
        public float CalculateDistance(Vector2 v1, Vector2 v2)
        {
            return Vector2.Distance(v1, v2);
        }

        /// <summary>
        /// set the delta time step
        /// </summary>
        /// <param name="time">time step</param>
        public void SetDeltaTime(float time)
        {
            deltaTime = time;
        }

        /// <summary>
        /// calculates the Xn+1/2 form the Xn position and its Veln
        /// Eq 22
        /// </summary>
        /// <param name="posN">Position 2D vector at step n</param>
        /// <param name="velN">Velocity 2D vector at step n</param>
        /// <returns>2D vector of position at step n+1/2</returns>
        public Vector2 CalculatePositionNPlusHalf(Vector2 posN, Vector2 velN)
        {
            var velBit = Vector2.Multiply((0.5f * deltaTime), velN);
            return Vector2.Add(posN, velBit);
        }

        /// <summary>
        /// calculate the Velocity at n+1 from the velocity at step n 
        /// and acceleration at step n+1/2
        /// Eq 23
        /// </summary>
        /// <param name="velN">Velocity 2D vector at step n</param>
        /// <param name="accNPlusHalf">Acceleration 2D vector at step n+1/2</param>
        /// <returns>2D vector of velocity at step n+1</returns>
        public Vector2 CalculateVelocityNPlusOne(Vector2 velN, Vector2 accNPlusHalf)
        {
            var accBit = Vector2.Multiply(deltaTime, accNPlusHalf);
            return Vector2.Add(velN, accBit);
        }

        /// <summary>
        /// calculates the Xn+1 from the Xn+1/2 position and its Veln+1
        /// Same as <see cref="CalculatePositionNPlusHalf(Vector2, Vector2)"/>
        /// Eq 24
        /// </summary>
        /// <param name="posNPlusHalf">Position 2D vector at step n+1/2</param>
        /// <param name="velNPlusOne">Velocity 2D vector at step n+1</param>
        /// <returns>2D vector of position at step n+1</returns>
        public Vector2 CalculatePositionPlusOne(Vector2 posNPlusHalf, Vector2 velNPlusOne)
        {
            var velBit = Vector2.Multiply((0.5f * deltaTime), velNPlusOne);
            return Vector2.Add(posNPlusHalf, velBit);
        }
    }
}
