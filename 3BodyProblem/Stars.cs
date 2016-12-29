using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3BodyProblem
{
    /// <summary>
    /// create twikling starts for the screen
    /// </summary>
    public class Stars
    {
        /// <summary>
        /// array of the star sprites
        /// </summary>
        private Sprite[] sprites;

        /// <summary>
        /// width and height of the stars
        /// </summary>
        private const double Thickness = 1.0;

        /// <summary>
        /// number of stars on the screen
        /// </summary>
        private const int NumStars = 300;

        /// <summary>
        /// number of stars to twinkle each update cycle
        /// </summary>
        private const uint NumStarsToTwinkle = 5;

        /// <summary>
        /// is it the first run of the update
        /// </summary>
        private bool firstRun = true;

        /// <summary>
        /// colours for the stars
        /// </summary>
        private Brush[] brushColours = {
                                    Brushes.Blue,
                                    Brushes.AntiqueWhite,
                                    Brushes.Red,
                                    Brushes.Yellow
                                };

        /// <summary>
        /// randomiser
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// canvas display
        /// </summary>
        private IDisplayCanvas displayCanvas;

        /// <summary>
        /// renderer
        /// </summary>
        private IRenderer renderer;

        /// <summary>
        /// class to implement displaying background stars
        /// </summary>
        /// <param name="renderer">renderer to display stars</param>
        public Stars(IRenderer renderer)
        {
            this.displayCanvas = renderer.GetDisplayCanvas();
            this.renderer = renderer;
            sprites = new Sprite[NumStars];
        }

        /// <summary>
        /// create the stars
        /// </summary>
        private void CreateStars()
        {
            for (int i = 0; i < NumStars; i++)
            {
                sprites[i] = new Sprite(GenerateRandomEllipse());
            }

            renderer.DrawSprite(sprites);
        }

        /// <summary>
        /// generate the elipses used as a star
        /// </summary>
        /// <returns>an elipse star proxy</returns>
        private Ellipse GenerateRandomEllipse()
        {
            Ellipse e = new Ellipse();
            double randomX = random.NextDouble() * displayCanvas.GetCanvasWidth();
            double randomY = random.NextDouble() * displayCanvas.GetCanvasHeight();
            e.Margin = new Thickness(randomX, randomY, 0, 0);
            e.Width = Thickness;
            e.Height = Thickness;

            int colourIndex = random.Next(brushColours.Length);
            e.Fill = brushColours[colourIndex];

            return e;
        }
   
        /// <summary>
        /// twinkle the stars
        /// </summary>
        private void Twinkle()
        {
            int starChoice = 0;

            Sprite[] addStars = new Sprite[NumStarsToTwinkle];

            for (int i = 0; i < NumStarsToTwinkle; i++)
            {
                starChoice = random.Next(NumStars - 1);
                renderer.RemoveSprite(sprites[starChoice]);
                sprites[starChoice] = new Sprite(GenerateRandomEllipse());
                addStars[i] = sprites[starChoice];
            }

            renderer.DrawSprite(addStars);
        }

        /// <summary>
        /// update the stars on display
        /// </summary>
        /// <param name="elapsedTime">double elapsed time</param>
        public void Update(double elapsedTime)
        {
            // need to create the stars on first update as the canvas will be 
            // setup properly with actual height and width
            // if done in the constructor the canvas wont have been setup yet
            // so display canvas height and width will be 0
            if (firstRun)
            {
                firstRun = false;

                CreateStars();
            }

            Twinkle();
        }
    }
}
