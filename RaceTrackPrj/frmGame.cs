using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace RaceTrackPrj
{
    

    public partial class frmGame : Form
    {
        Pen Grid = new Pen(Color.Gray);
        Pen Track = new Pen(Color.Black, 3);


        int gridSpacing = 20;
        int trackWidth = 10;
        int width, height;
        Game runGame;
        Graphics g;
        Map map;

        public frmGame()
        {
            InitializeComponent();
        }

        private void frmGame_Load(object sender, EventArgs e)
        {
            pnlGameGrid.Refresh();

            width = pnlGameGrid.Width;
            height = pnlGameGrid.Height;
        }

        
        private void pnlGameGrid_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            map = new Map(gridSpacing, width, height, trackWidth);

            runGame = new Game(map, gridSpacing, InitialiseStartLine(), e);
        }

        public void refreshMap()
        {
            g.Clear(Color.LightGray);
            initialiseGrid();
            DrawTrack();
            InitialiseStartLine();
        }
            
        


        //2) update outer track
        //3) smooth
        private void DrawTrack()
        {
            Point[] pPoints = new Point[map.Inner.Length];
            for (int i = 0; i < map.Inner.Length; i++)
                pPoints[i] = convertPoint(map.Inner[i]);
            GraphicsPath paInner = new GraphicsPath();
            paInner.AddPolygon(pPoints);
            g.DrawPath(Track, paInner);

            pPoints = new Point[map.Outer.Length];
            for (int i = 0; i < map.Outer.Length; i++)
                pPoints[i] = convertPoint(map.Outer[i]);
            GraphicsPath paOuter = new GraphicsPath();
            paOuter.AddPolygon(pPoints);
            g.DrawPath(Track, paOuter);
        }

        //Draws grid lines inside of map
        private void initialiseGrid()
        {
            //Vertical Lines
            for (int i = -width/2; i < width/2; i += gridSpacing)
            {
                int count = 0;
                Point[] crossing = new Point[1];

                //Calculate Points
                for (int j = 0; j < map.InnerCart.Length; j++)
                    if (map.InnerCart[j].isCrossing(new Point(i, height), new Point(i, -height)))
                    {
                        Array.Resize(ref crossing, count + 1);
                        crossing[count] = map.InnerCart[j].intersection(new Point(i, height), new Point(i, -height));
                        count += 1;
                    }
                for (int j = 0; j < map.OuterCart.Length; j++)
                    if (map.OuterCart[j].isCrossing(new Point(i, height), new Point(i, -height)))
                    {
                        Array.Resize(ref crossing, count + 1);
                        crossing[count] = map.OuterCart[j].intersection(new Point(i, height / 2), new Point(i, -height));
                        count += 1;
                    }

                //Sort Points
                crossing = orderByY(crossing);

                //Draw
                if (crossing.Length >= 2)
                    for (int j = 0; j <= crossing.Length - 2; j++)
                        if (map.IsInsideTrack(new Point(crossing[j].X, crossing[j].Y - 1)))
                            g.DrawLine(Grid, convertPoint(crossing[j]), convertPoint(crossing[j + 1]));
            }

            //Horizontal Lines
            for (int i = -height / 2; i < height / 2; i += gridSpacing)
            {
                int count = 0;
                Point[] crossing = new Point[1];

                //Calculate Points
                for (int j = 0; j < map.InnerCart.Length; j++)
                    if (map.InnerCart[j].isCrossing(new Point(width, i), new Point(-width, i)))
                    {
                        Array.Resize(ref crossing, count + 1);
                        crossing[count] = map.InnerCart[j].intersection(new Point(width, i), new Point(-width, i));
                        count += 1;
                    }
                for (int j = 0; j < map.OuterCart.Length; j++)
                    if (map.OuterCart[j].isCrossing(new Point(width, i), new Point(-width, i)))
                    {
                        Array.Resize(ref crossing, count + 1);
                        crossing[count] = map.OuterCart[j].intersection(new Point(width, i), new Point(-width, i));
                        count += 1;
                    }

                //Sort Points
                crossing = orderByX(crossing);

                //Draw
                if (crossing.Length >= 2)
                    for (int j = 0; j <= crossing.Length - 2; j++)
                        if (map.IsInsideTrack(new Point(crossing[j].X -1, crossing[j].Y)))
                            g.DrawLine(Grid, convertPoint(crossing[j]), convertPoint(crossing[j + 1]));
            }
        }

        //Orders the points so the grid lines know when to start drawing and when to stop
        private Point[] orderByY(Point[] ps)
        {
            for (int i = 0; i < ps.Length; i++)
                for (int j = 0; j < ps.Length - i - 1; j++)
                    if (ps[j].Y < ps[j + 1].Y)
                    {
                        Point temp = ps[j];
                        ps[j] = ps[j + 1];
                        ps[j + 1] = temp;
                    }

            return ps;
        }

        private Point[] orderByX(Point[] ps)
        {
            for (int i = 0; i < ps.Length; i++)
                for (int j = 0; j < ps.Length - i - 1; j++)
                    if (ps[j].X < ps[j + 1].X)
                    {
                        Point temp = ps[j];
                        ps[j] = ps[j + 1];
                        ps[j + 1] = temp;
                    }

            return ps;
        }

        //Draws start line and calculates starting position.
        private Point InitialiseStartLine()
        {
            
            Point left = new Point(0, 0);
            Point right = new Point (0, 0);
            for (int j = 0; j < map.InnerCart.Length; j++)
                if (map.InnerCart[j].isCrossing(new Point(0, 0), new Point(width, 0)))
                {
                    left = map.InnerCart[j].intersection(new Point(0, 0), new Point(width, 0));
                    break;
                }
            for (int j = 0; j < map.OuterCart.Length; j++)
                if (map.OuterCart[j].isCrossing(new Point(0, 0), new Point(width, 0)))
                {
                    right = map.OuterCart[j].intersection(new Point(0, 0), new Point(width, 0));
                    break;
                }

            g.DrawLine(new Pen(Color.Blue, 2), convertPoint(left), convertPoint(right));


            double d = (left.X + right.X) / (gridSpacing * 2);
            return new Point(Convert.ToInt32(Math.Round(d, 0, MidpointRounding.ToEven) * gridSpacing), 0);
        }


        // -----------------------------------------
        //Code for gameplay
        private void frmGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    runGame.players[runGame.currentPlayer].Turn(-1);
                    break;
            }
        }

        


        //Converts from arbitrary coordinates to pixels
        public Point convertPoint(Point point)
        {
            return new Point(point.X + width / 2, point.Y + height / 2);
        }
    }
}
