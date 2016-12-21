using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3BodyProblem.Calculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace _3BodyProblem.Calculations.Tests
{
    [TestClass()]
    public class PositonVertlet2DTests
    {

        PositonVertlet2D posVer = new PositonVertlet2D();

        [TestMethod()]
        public void CalcualteInitialXhalfTest()
        {
            var xInitial = new Vector2(1, 1);
            var vInitial = new Vector2(2, 2);
            var aInitial = new Vector2(4, 4);

            posVer.SetDeltaTime(1);

            var xHalf = posVer.CalcualteInitialXhalf(xInitial, vInitial, aInitial);

            Assert.IsTrue(xHalf == new Vector2(3, 3));
        }

        [TestMethod()]
        public void CalculateDistanceTest()
        {
            var v1 = new Vector2(1, 1);
            var v2 = new Vector2(2, 2);

            var distance = posVer.CalculateDistance(v1, v2);

            // impossible to compare 2 floats as equal
            // just check it is almost the same
            float delta = distance - 1.414f;

            var abs = Math.Abs(delta);

            Assert.IsTrue(abs < 0.1);
        }

        [TestMethod()]
        public void CalculateAccelerationTest()
        {
            var v1 = new Vector2(1, 1);
            var v2 = new Vector2(2, 2);
            var mass2 = 1.0f;

            var accVector = posVer.CalculateAcceleration(mass2, v1, v2);

            float delta = accVector.X - 0.0000000000236f;

            var abs = Math.Abs(delta);

            Assert.IsTrue(abs < 0.000000000001);
        }

        [TestMethod()]
        public void PositionNPlusHalfTest()
        {
            var initialPos = new Vector2(1, 1);
            var initialVel = new Vector2(2, 2);

            posVer.SetDeltaTime(1);

            var xNPlusHalfExpected = new Vector2(2, 2);
            var xNPlusHalf = posVer.PositionNPlusHalf(initialPos, initialVel);

            Assert.IsTrue(xNPlusHalfExpected == xNPlusHalf);
        }

        [TestMethod()]
        public void VelocityNPlusOneTest()
        {
            var initialVel = new Vector2(1, 1);
            var initialAcc = new Vector2(2, 2);

            posVer.SetDeltaTime(1);

            var velNPlusOneExpected = new Vector2(3, 3);
            var velNPlusOne = posVer.VelocityNPlusOne(initialVel, initialAcc);

            Assert.IsTrue(velNPlusOneExpected == velNPlusOne);
        }
    }
}