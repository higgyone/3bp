using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace _3BodyProblem.Body
{
    public class Body
    {
        /// <summary>
        /// mass of the body
        /// </summary>
        public float Mass { get; private set; }

        /// <summary>
        /// 2nd body
        /// </summary>
        //public Body SecondBody { get; private set; }

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

        public Body(float mass, Vector2 initialPos, Vector2 initialVel)
        {
            Mass = mass;
            Position = initialPos;
            Velocity = initialVel;
        }
    }
}
