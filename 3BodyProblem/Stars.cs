using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace _3BodyProblem
{
    public struct Star
    {
        public Point point;
        public Pen pen;

        public Star(Point point, Pen pen)
        {
            this.point = point;
            this.pen = pen;
        }
    }

    public class Stars
    {
        public List<Star> starList { get; private set; }

        private const double Thickness = 1.0;

        private const int NumStars = 300;

        private const uint NumStarsToTwinkle = 5;

        public Pen[] penColours = {
                                    new Pen(Brushes.Blue, Thickness),
                                    new Pen(Brushes.AntiqueWhite, Thickness),
                                    new Pen(Brushes.Red, Thickness),
                                    new Pen(Brushes.Yellow, Thickness)
                                };

        private Random random = new Random();

        private IDisplayCanvas displayCanvas;

        public Stars(IDisplayCanvas displayCanvas)
        {
            this.displayCanvas = displayCanvas;
            starList = new List<Star>();
        }

        public void CreateStars()
        {
            for (int i = 0; i < NumStars; i++)
            {
                starList.Add(GenerateRandomStar());
            }
        }

        private Star GenerateRandomStar()
        {
            Star newStar;
            Point randomPoint;

            int randomX = random.Next((int)displayCanvas.GetCanvasWidth());
            int randomY = random.Next((int)displayCanvas.GetCanvasHeight());

            randomPoint = new Point(randomX, randomY);

            int colourIndex = random.Next(penColours.Length);
            Pen pen = penColours[colourIndex];


            newStar = new Star(randomPoint, pen);

            return newStar;
        }

        private void Twinkle()
        {
            for (int i = 0; i < NumStarsToTwinkle; i++)
            {
                starList.RemoveAt(random.Next(NumStars - i));
            }

            for (int i = 0; i < NumStarsToTwinkle; i++)
            {
                starList.Add(GenerateRandomStar());
            }
        }

        public void Update(double elapsedTime)
        {
            Twinkle();
        }
    }
}
