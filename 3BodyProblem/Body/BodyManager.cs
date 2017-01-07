using _3BodyProblem.Calculations;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        /// body 3
        /// </summary>
        public Body body3 { get; private set; }

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
        /// sprite for body 3
        /// </summary>
        private Sprite sprite3;

        /// <summary>
        /// body1 image
        /// </summary>
        public Image body1Image { get; private set; }

        /// <summary>
        /// outline of body1
        /// </summary>
        public EllipseGeometry body1Outline { get; private set; }

        /// <summary>
        /// body2 image
        /// </summary>
        public Image body2Image { get; private set; }

        /// <summary>
        /// outline of the body2
        /// </summary>
        public EllipseGeometry body2Outline { get; private set; }

        /// <summary>
        /// body3 image
        /// </summary>
        public Image body3Image { get; private set; }

        /// <summary>
        /// outline of the body3
        /// </summary>
        public EllipseGeometry body3Outline { get; private set; }

        /// <summary>
        /// framespersecond object
        /// </summary>
        private FramesPerSecond fps = new FramesPerSecond();

        /// <summary>
        /// label to display the FPS
        /// </summary>
        private Label fpsLabel;

        /// <summary>
        /// divisor for the positions to fit larger orbits on the canvas
        /// </summary>
        private uint positionDivider = 5;

        /// <summary>
        /// label to display the speed
        /// </summary>
        private Label speedLabel;

        /// <summary>
        /// add 100 to speed button
        /// </summary>
        private Button add100Button;

        /// <summary>
        /// subtract 100 from speed button
        /// </summary>
        private Button sub100Button;

        /// <summary>
        /// current speed value
        /// </summary>
        private uint speedValue = 100;

        /// <summary>
        /// wider field button
        /// </summary>
        private Button widerFieldButton;

        /// <summary>
        /// narrower field button
        /// </summary>
        private Button narrowerFieldButton;

        /// <summary>
        /// field value
        /// </summary>
        private uint fieldValue = 50;

        /// <summary>
        /// field label
        /// </summary>
        private Label fieldLabel;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="body1">body1 info</param>
        /// <param name="body2">body 2 info</param>
        /// <param name="body3">body3</param>
        public BodyManager(IRenderer renderer, Body body1, Body body2, Body body3)
        {
            this.renderer = renderer;
            displayCanvas = renderer.GetDisplayCanvas();
            this.body1 = body1;
            this.body2 = body2;
            this.body3 = body3;

            SetupFPSCanvas();
            SetupSpeedLabel();
            SetupFieldLabel();

            SetupAdd100Button();
            SetupSub100Button();
            SetupNarrowerFieldButton();
            SetupWiderFieldButton();

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

            if (!displayCanvas.CanvasChildContainsElement(fpsLabel))
            {
                displayCanvas.AddCanvasChild(fpsLabel);
            }
        }

        /// <summary>
        /// add the plus 100 speed button to the canvas
        /// </summary>
        private void SetupAdd100Button()
        {
            add100Button = new Button();
            add100Button.Margin = new Thickness(100, 0, 0, 0);
            add100Button.Click += new RoutedEventHandler(Add100ButtonClick);
            add100Button.Content = "+100";
            add100Button.Width = 35;

            if (!displayCanvas.CanvasChildContainsElement(add100Button))
            {
                displayCanvas.AddCanvasChild(add100Button);
            }
        }

        /// <summary>
        /// click event for the add 100 button
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void Add100ButtonClick(object obj, RoutedEventArgs e)
        {
            speedValue = speedValue + 100;
            //speedLabel.Content = speedValue;
        }

        /// <summary>
        /// add the subtract 100 from speed value button
        /// </summary>
        private void SetupSub100Button()
        {
            sub100Button = new Button();
            sub100Button.Margin = new Thickness(100, 25, 0, 0);
            sub100Button.Click += new RoutedEventHandler(Sub100ButtonClick);
            sub100Button.Content = "-100";
            sub100Button.Width = 35;

            if (!displayCanvas.CanvasChildContainsElement(sub100Button))
            {
                displayCanvas.AddCanvasChild(sub100Button);
            }
        }

        /// <summary>
        /// add a reduce speed by 100 button
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="e">routed event args e</param>
        private void Sub100ButtonClick(object obj, RoutedEventArgs e)
        {
            if (speedValue >= 100)
            {
                speedValue = speedValue - 100;
            }
        }

        /// <summary>
        /// setup on the canvas the speed label
        /// </summary>
        private void SetupSpeedLabel()
        {
            speedLabel = new Label();
            speedLabel.Foreground = Brushes.Red;
            speedLabel.Margin = new Thickness(2, 15, 0, 0);

            if (!displayCanvas.CanvasChildContainsElement(speedLabel))
            {
                displayCanvas.AddCanvasChild(speedLabel);
            }
        }

        /// <summary>
        /// add the wider field x5 button
        /// </summary>
        private void SetupWiderFieldButton()
        {
            widerFieldButton = new Button();
            widerFieldButton.Margin = new Thickness(100, 50, 0, 0);
            widerFieldButton.Click += new RoutedEventHandler(WiderFieldButtonClick);
            widerFieldButton.Content = "+5";
            widerFieldButton.Width = 35;

            if (!displayCanvas.CanvasChildContainsElement(widerFieldButton))
            {
                displayCanvas.AddCanvasChild(widerFieldButton);
            }
        }

        /// <summary>
        /// wider field x5 button click
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="e">routed event args e</param>
        private void WiderFieldButtonClick(object obj, RoutedEventArgs e)
        {
            fieldValue += 5;
            fieldLabel.Content = fieldValue;
        }

        /// <summary>
        /// add the narrower field x5 button
        /// </summary>
        private void SetupNarrowerFieldButton()
        {
            narrowerFieldButton = new Button();
            narrowerFieldButton.Margin = new Thickness(100, 75, 0, 0);
            narrowerFieldButton.Click += new RoutedEventHandler(NarrowerFieldButtonClick);
            narrowerFieldButton.Content = "-5";
            narrowerFieldButton.Width = 35;

            if (!displayCanvas.CanvasChildContainsElement(narrowerFieldButton))
            {
                displayCanvas.AddCanvasChild(narrowerFieldButton);
            }
        }

        /// <summary>
        /// narrower field x5 button click
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="e">routed event args e</param>
        private void NarrowerFieldButtonClick(object obj, RoutedEventArgs e)
        {
            if (fieldValue > 5)
            {
                fieldValue -= 5;
            }
        }

        /// <summary>
        /// setup on the canvas the field label
        /// </summary>
        private void SetupFieldLabel()
        {
            fieldLabel = new Label();
            fieldLabel.Foreground = Brushes.Red;
            fieldLabel.Margin = new Thickness(2, 30, 0, 0);

            if (!displayCanvas.CanvasChildContainsElement(fieldLabel))
            {
                displayCanvas.AddCanvasChild(fieldLabel);
            }
        }

        /// <summary>
        /// create and add the body sprites
        /// </summary>
        private void SetupBodySprites()
        {
            CreateBody1Sprite();
            CreateBody2Sprite();
            CreateBody3Sprite();

            sprite1 = new Sprite(body1Image);
            sprite2 = new Sprite(body2Image);
            sprite3 = new Sprite(body3Image);

            sprite1.SetPosition(body1.Position);
            sprite2.SetPosition(body2.Position);
            sprite3.SetPosition(body3.Position);

            AddSprites();
        }

        /// <summary>
        /// add the bodies to the display
        /// </summary>
        private void AddSprites()
        {
            Sprite[] sprites = new Sprite[] { sprite1, sprite2, sprite3 };
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
        /// create body3 sprite
        /// </summary>
        private void CreateBody3Sprite()
        {
            body3Image = new Image();
            Uri imageUri = new Uri(@"target.png", UriKind.Relative);

            body3Image.Source = new BitmapImage(imageUri);
            body3Image.Width = body3.Diameter;
            body3Image.Height = body3.Diameter;
            body3Image.HorizontalAlignment = HorizontalAlignment.Left;

            // Use an EllipseGeometry to define the clip region. 
            body3Outline = new EllipseGeometry();
            body3Outline.Center = new Point(body3.Diameter / 2, body3.Diameter / 2);
            body3Outline.RadiusX = body3.Diameter / 2;
            body3Outline.RadiusY = body3.Diameter / 2;
            body3Outline.Freeze();
            body3Image.Clip = body3Outline;
        }

        /// <summary>
        /// update the background stars and FPS outside body loop
        /// </summary>
        /// <param name="deltaTime">time difference between frames</param>
        /// <param name="timeMultiplier">what the elapsed time has been multiplied by to speed smulation up</param>
        public void UpdateBackground(float deltaTime, uint timeMultiplier = 1)
        {
            fps.Process(deltaTime / timeMultiplier);

            stars.Update(deltaTime / timeMultiplier);
        }

        /// <summary>
        /// update the body positions 
        /// </summary>
        /// <param name="deltaTime">difference between each calculation loop</param>
        public void UpdatePositions2Bodies(float deltaTime)
        {
            for (int i = 0; i < speedValue; i++)
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

        /// <summary>
        /// updates the positions for 3bodiws in 2d space
        /// </summary>
        /// <param name="deltaTime">time between each step</param>
        public void UpdatePositions3Bodies(float deltaTime)
        {
            for (int i = 0; i < speedValue; i++)
            {
                posVer.SetDeltaTime(deltaTime);

                if (firstRun)
                {
                    // calculate acceleration dependant on second body
                    body1.Acceleration = posVer.CalculateAcceleration3Bodies(body2.Mass, body3.Mass, body1.Position, body2.Position, body3.Position);
                    body2.Acceleration = posVer.CalculateAcceleration3Bodies(body1.Mass, body3.Mass, body2.Position, body1.Position, body3.Position);
                    body3.Acceleration = posVer.CalculateAcceleration3Bodies(body1.Mass, body2.Mass, body3.Position, body1.Position, body2.Position);

                    // use acceleration value to calcualte XHalf initial
                    body1.PositionHalf = posVer.CalcualteInitialXhalf(body1.Position, body1.Velocity, body1.Acceleration);
                    body2.PositionHalf = posVer.CalcualteInitialXhalf(body2.Position, body2.Velocity, body2.Acceleration);
                    body3.PositionHalf = posVer.CalcualteInitialXhalf(body3.Position, body3.Velocity, body3.Acceleration);
                }

                // calculate an+1/2 values from either x1/2 or xn+1/2
                body1.AccelerationHalf = posVer.CalculateAcceleration3Bodies(body2.Mass, body3.Mass, body1.Position, body2.Position, body3.Position);
                body2.AccelerationHalf = posVer.CalculateAcceleration3Bodies(body1.Mass, body3.Mass, body2.Position, body1.Position, body3.Position);
                body3.AccelerationHalf = posVer.CalculateAcceleration3Bodies(body1.Mass, body2.Mass, body3.Position, body1.Position, body2.Position);

                // calculate vn+1 using an+1/2 eq 23 
                body1.Velocity = posVer.CalculateVelocityNPlusOne(body1.Velocity, body1.AccelerationHalf);
                body2.Velocity = posVer.CalculateVelocityNPlusOne(body2.Velocity, body2.AccelerationHalf);
                body3.Velocity = posVer.CalculateVelocityNPlusOne(body3.Velocity, body3.AccelerationHalf);

                // calculate xn+1 using vn+1 eq 24
                body1.Position = posVer.CalculatePositionPlusOne(body1.PositionHalf, body1.Velocity);
                body2.Position = posVer.CalculatePositionPlusOne(body2.PositionHalf, body2.Velocity);
                body3.Position = posVer.CalculatePositionPlusOne(body3.PositionHalf, body3.Velocity);

                // calculate next xn+1/2 using xn+1 eq 22
                body1.PositionHalf = posVer.CalculatePositionNPlusHalf(body1.Position, body1.Velocity);
                body2.PositionHalf = posVer.CalculatePositionNPlusHalf(body2.Position, body2.Velocity);
                body3.PositionHalf = posVer.CalculatePositionNPlusHalf(body3.Position, body3.Velocity);
            }
        }

        /// <summary>
        /// redender to the screen
        /// </summary>
        public void Render()
        {
            fpsLabel.Content = "FPS: " + fps.CurrentFPS.ToString("F2");
            speedLabel.Content = "Speed: " + speedValue + "x";
            fieldLabel.Content = "Field: " + fieldValue + "x";

            sprite1.SetPosition(body1.Position / fieldValue);
            sprite2.SetPosition(body2.Position / fieldValue);
            sprite3.SetPosition(body3.Position / fieldValue);
        }
    }
}
