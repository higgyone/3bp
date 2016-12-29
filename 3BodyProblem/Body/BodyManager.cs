using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using _3BodyProblem.Calculations;
using System.Threading;
using System.Numerics;

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

        /// <summary>
        /// renderer for the bodies
        /// </summary>
        private IRenderer renderer;

        /// <summary>
        /// display canvas
        /// </summary>
        private IDisplayCanvas displayCanvas;

        /// <summary>
        /// to display a twinkling starry background
        /// </summary>
        private Stars stars;

        /// <summary>
        /// sprite for body1
        /// </summary>
        private Sprite sprite1;

        /// <summary>
        /// sprite for body2
        /// </summary>
        private Sprite sprite2;

        /// <summary>
        /// body1 image
        /// </summary>
        public Image body1Image { get; private set; }

        /// <summary>
        /// outline of body1
        /// </summary>
        public EllipseGeometry body1Outline { get; private set; }

        /// <summary>
        /// body1 image
        /// </summary>
        public Image body2Image { get; private set; }

        /// <summary>
        /// outline of the body2
        /// </summary>
        public EllipseGeometry body2Outline { get; private set; }

        /// <summary>
        /// framespersecond object
        /// </summary>
        private FramesPerSecond fps = new FramesPerSecond();

        /// <summary>
        /// label to display the FPS
        /// </summary>
        private Label fpsLabel;

        public BodyManager(IRenderer renderer, Body body1, Body body2)
        {
            this.renderer = renderer;
            displayCanvas = renderer.GetDisplayCanvas();
            this.body1 = body1;
            this.body2 = body2;

            SetupFPSCanvas();

            SetupBodySprites();

            stars = new Stars(renderer);
        }

        /// <summary>
        /// initialise the canvas
        /// </summary>
        private void SetupFPSCanvas()
        {
            fpsLabel = new Label();
            fpsLabel.Foreground = Brushes.Red;
            fpsLabel.Margin = new Thickness(2);
        }

        /// <summary>
        /// create and add the body sprites
        /// </summary>
        private void SetupBodySprites()
        {
            CreateBody1Sprite();
            CreateBody2Sprite();
            sprite1 = new Sprite(body1Image);
            sprite2 = new Sprite(body2Image);

            sprite1.SetPosition(body1.Position);
            sprite2.SetPosition(body2.Position);

            AddSprites();
        }

        /// <summary>
        /// add the bodies to the display
        /// </summary>
        private void AddSprites()
        {
            Sprite[] sprites = new Sprite[] { sprite1, sprite2 };
            renderer.DrawSprite(sprites);
        }

        /// <summary>
        /// create body1 sprite
        /// </summary>
        private void CreateBody1Sprite()
        {
            body1Image = new Image();
            Uri imageUri = new Uri(@"target.png", UriKind.Relative);
            //Uri imageUri = new Uri(string.Format(@"{0}", textureManager.Get("redTarget")), UriKind.Relative);

            body1Image.Source = new BitmapImage(imageUri);
            body1Image.Width = body1.Diameter;
            body1Image.Height = body1.Diameter;
            body1Image.HorizontalAlignment = HorizontalAlignment.Left;

            // Use an EllipseGeometry to define the clip region. 
            body1Outline = new EllipseGeometry();
            body1Outline.Center = new Point(body1.Diameter / 2, body1.Diameter / 2);
            body1Outline.RadiusX = body1.Diameter / 2;
            body1Outline.RadiusY = body1.Diameter / 2;
            body1Outline.Freeze();
            body1Image.Clip = body1Outline;
        }

        /// <summary>
        /// create body2 sprite
        /// </summary>
        private void CreateBody2Sprite()
        {
            body2Image = new Image();
            Uri imageUri = new Uri(@"target.png", UriKind.Relative);

            body2Image.Source = new BitmapImage(imageUri);
            body2Image.Width = body2.Diameter;
            body2Image.Height = body2.Diameter;
            body2Image.HorizontalAlignment = HorizontalAlignment.Left;

            // Use an EllipseGeometry to define the clip region. 
            body2Outline = new EllipseGeometry();
            body2Outline.Center = new Point(body2.Diameter / 2, body2.Diameter / 2);
            body2Outline.RadiusX = body2.Diameter / 2;
            body2Outline.RadiusY = body2.Diameter / 2;
            body2Outline.Freeze();
            body2Image.Clip = body2Outline;
        }

        /// <summary>
        /// update the body positions 
        /// </summary>
        /// <param name="deltaTime">time difference between frames</param>
        /// <param name="timeMultiplier">what the elapsed time has been multiplied by to speed smulation up</param>
        public void UpdatePositions(float deltaTime, uint timeMultiplier = 1)
        {
            fps.Process(deltaTime / timeMultiplier);

            stars.Update(deltaTime / timeMultiplier);

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

        public void Render()
        {
            if (!displayCanvas.CanvasChildContainsElement(fpsLabel))
            {
                displayCanvas.AddCanvasChild(fpsLabel);
            }

            fpsLabel.Content = "FPS: " + fps.CurrentFPS.ToString("F2");

            sprite1.SetPosition(body1.Position/2);
            sprite2.SetPosition(body2.Position/2);
        }
    }
}
