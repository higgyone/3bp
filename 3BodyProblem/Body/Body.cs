using System;
using System.Numerics;

namespace _3BodyProblem.Body
{
    /// <summary>
    /// holder of the information on the body
    /// </summary>
    public class Body
    {
        /// <summary>
        /// mass of the body
        /// </summary>
        public float Mass { get; private set; }

        /// <summary>
        /// Diameter
        /// </summary>
        public Double Diameter { get; private set; }

        /// <summary>
        /// the initial X1/2 posiytion
        /// </summary>
        public Vector2 PositionHalf { get; internal set; }

        /// <summary>
        /// 2D  position vector
        /// </summary>
        public Vector2 Position { get; internal set; }

        /// <summary>
        /// 2D velocity vector
        /// </summary>
        public Vector2 Velocity { get; internal set; }

        /// <summary>
        /// 2D acceleration half vector
        /// </summary>
        public Vector2 AccelerationHalf { get; internal set; }

        /// <summary>
        /// 2D acceleration vector
        /// </summary>
        public Vector2 Acceleration { get; internal set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mass">mass of the body</param>
        /// <param name="diameter">display diameter of the body</param>
        /// <param name="initialPos">initial 2D vector position of the body</param>
        /// <param name="initialVel">initial 2D velocity vector of the body</param>
        public Body(float mass, double diameter, Vector2 initialPos, Vector2 initialVel)
        {
            Mass = mass;
            Diameter = diameter;
            Position = initialPos;
            Velocity = initialVel;
        }
    }
}
