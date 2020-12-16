using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RaceTrackPrj
{
    //Stores lines in cartesian from to make intersection calculations far easier
    class Cartesian
    {
        public Point start { get; private set; }
        public Point end { get; private set; }
        public float xCo { get; private set; }
        public float yCo { get; private set; }
        public float C { get; private set; }

        
        public Cartesian(Point point1, Point point2)
        {
            start = point1;
            end = point2;
            xCo = point1.Y - point2.Y;
            yCo = point2.X - point1.X;
            C = -(point1.X * point2.Y - point2.X * point1.Y); 

        }



        public virtual bool isCrossing(Point point1, Point point2)
        {
            int dir1 = direction(start, end, point1);
            int dir2 = direction(start, end, point2);
            int dir3 = direction(point1, point2, start);
            int dir4 = direction(point1, point2, end);

            if (dir1 != dir2 && dir3 != dir4)
                return true; 

            if (dir1 == 0 && onLine(start, end, point1)) 
                return true;

            if (dir2 == 0 && onLine(start, end, point2)) 
                return true;

            if (dir3 == 0 && onLine(point1, point2, start)) 
                return true;

            if (dir4 == 0 && onLine(point1, point2, end)) 
                return true;

            return false;
        }

        private int max(int no1, int no2)
        {
            if (no1 > no2)
                return no1;
            else
                return no2;
        }

        private int min(int no1, int no2)
        {
            if (no1 < no2)
                return no1;
            else
                return no2;
        }

        private int direction(Point a, Point b, Point c)
        {
            int val = (b.Y - a.Y) * (c.X - b.X) - (b.X - a.X) * (c.Y - b.Y);
            if (val == 0)
                return 0;     
            else if (val < 0)
                return 2;    
            return 1;
        }

        //check whether p is on the line or not
        private bool onLine(Point p1, Point p2, Point p)
        {
            if (p.X <= max(p1.X, p2.X) && p.X <= min(p1.X, p2.X) && (p.Y <= max(p1.Y, p2.Y) && p.Y <= min(p1.Y, p2.Y)))
                return true;

            return false;
        }


        public Point intersection(Point point1, Point point2)
        {
            float xCoTemp = point1.Y - point2.Y;
            float yCoTemp = point2.X - point1.X;
            float CTemp = -(point1.X * point2.Y - point2.X * point1.Y);

            float delta = xCo * yCoTemp - xCoTemp * yCo;

            float x = (yCoTemp * C - yCo * CTemp) / delta;
            float y = (xCo * CTemp - xCoTemp * C) / delta;

            return new Point(Convert.ToInt32(x), Convert.ToInt32(y));
        }
    }
}
