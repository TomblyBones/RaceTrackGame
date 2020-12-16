using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RaceTrackPrj
{
    class Map
    {
        public Point[] Inner { get; private set; }
        public Point[] Outer { get; private set; }
        public Cartesian[] InnerCart { get; private set; }
        public Cartesian[] OuterCart { get; private set; }

        private Random rand = new Random();
        public int _Width { get; set; }
        public int _Height { get; set; }

        public Map(int gridSpacing, int Width, int Height, int trackWidth)
        {
            //points = new Point[7];
            _Height = Height;
            _Width = Width;

            //Calculating rough outline for track
            Inner = CalcInner();
            Outer = CalcOuter(gridSpacing, trackWidth);

            //Smoothing


            //Store cartesian
            StoreAsCart();
        }

        private Point[] CalcInner()
        {
            int x;
            int y;

            Point[] ogFour = new Point[8];
            for (int i = 0; i < 4; i++)
            {
                //Calculating First 4 corners of polygon
                x = Convert.ToInt32(_Width / 4 + rand.NextDouble() * (_Width / 4) - _Width / 5);
                double angle = Math.Tan(Math.PI / 4 + (rand.NextDouble() * (Math.PI / 16) - Math.PI / 32));
                y = Convert.ToInt32((double)_Height / (double)_Width * x * angle);

                //Orienting each random point
                if (i == 0)
                    ogFour[i*2] = new Point(x, y);
                if (i == 1)
                    ogFour[i*2] = new Point(-x, y);
                if (i == 2)
                    ogFour[i*2] = new Point(-x, -y);
                if (i == 3)
                    ogFour[i*2] = new Point(x, -y);
            }

            for (int i = 0; i <= 6; i+=2)
            {
                if (i != 6)
                    ogFour[i + 1] = new Point(rand.Next(_Width / 10) - _Width / 20 + (ogFour[i].X + ogFour[i + 2].X) / 2, rand.Next(_Height / 10) - _Height / 20 + (ogFour[i].Y + ogFour[i + 2].Y) / 2);
                else
                    ogFour[i + 1] = new Point(rand.Next(_Width / 10) - _Width / 20 + (ogFour[i].X + ogFour[0].X) / 2, rand.Next(_Height / 10) - _Height / 20 + (ogFour[i].Y + ogFour[0].Y) / 2);
            }

            //Will return different value eventually
            return ogFour;
        }

        private Point[] CalcOuter(int gridSpacing, int trackWidth)
        {
            Point[] _outer = new Point[Inner.Length];

            double d = trackWidth * gridSpacing;

            //Each corner is moved by out exactly n grid spaces from the middle
            
            for (int i = 0; i < Inner.Length; i++)
            {
                double l = Math.Sqrt(Math.Pow(Inner[i].X, 2) + Math.Pow(Inner[i].Y, 2));
                _outer[i] = new Point(Convert.ToInt32((l + d) * (Inner[i].X / l)), Convert.ToInt32((l + d) * (Inner[i].Y / l)));
            }

            //Each point is moved along a perpendicular line of the line by n distance



            return _outer;
        }

        //Store all points as a cartesian line equation
        private void StoreAsCart()
        {
            InnerCart = new Cartesian[Inner.Length];
            for (int i = 0; i < Inner.Length - 1; i++)
                InnerCart[i] = new Cartesian(Inner[i], Inner[i + 1]);
            InnerCart[Inner.Length - 1] = new Cartesian(Inner[Inner.Length - 1], Inner[0]);

            OuterCart = new Cartesian[Outer.Length];
            for (int i = 0; i < Outer.Length - 1; i++)
                OuterCart[i] = new Cartesian(Outer[i], Outer[i + 1]);
            OuterCart[Outer.Length - 1] = new Cartesian(Outer[Outer.Length - 1], Outer[0]);
        }

        //Checks whether a point is inside the track
        public bool IsInsideTrack(Point point)
        {
            //Outside innet
            if (!IsInInner(point) && IsInOuter(point))
                return true;
            return false;
        }

        private bool IsInInner(Point p)
        {
            Point p1, p2;
            bool inside = false;

            if (Inner.Length < 3)
            {
                return inside;
            }

            var oldPoint = new Point(
                Inner[Inner.Length - 1].X, Inner[Inner.Length - 1].Y);

            for (int i = 0; i < Inner.Length; i++)
            {
                var newPoint = new Point(Inner[i].X, Inner[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                    && (p.Y - (long)p1.Y) * (p2.X - p1.X)
                    < (p2.Y - (long)p1.Y) * (p.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
        }

        private bool IsInOuter(Point p)
        {
            Point p1, p2;
            bool inside = false;

            if (Outer.Length < 3)
            {
                return inside;
            }

            var oldPoint = new Point(
                Outer[Outer.Length - 1].X, Outer[Outer.Length - 1].Y);

            for (int i = 0; i < Outer.Length; i++)
            {
                var newPoint = new Point(Outer[i].X, Outer[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                    && (p.Y - (long)p1.Y) * (p2.X - p1.X)
                    < (p2.Y - (long)p1.Y) * (p.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
        }
    }
}
