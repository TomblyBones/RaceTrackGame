using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RaceTrackPrj
{
    class Player
    {
        //Defining velocity structure
        private struct Velocity
        {
            public int X { get; set; } 
            public int Y { get; set; }
                
            public Velocity(int x, int y)
            {
                X = x;
                Y = y;
            }
        }


        public Point[] moves { get; private set; }
        public bool isAlive { get; private set; } = true;
        public bool hasCompleted { get; private set; } = false;
        public int choice { private get; set; } = 0;


        int gridSpacing;
        bool isConfirmed;
        Pen pen;
        Velocity currentV;
        Graphics g;
        int selection;

        public Player(Point startPos, Pen p, int gr, PaintEventArgs e)
        {
            moves = new Point[1];
            moves[0] = startPos;
            pen = p;
            gridSpacing = gr;
            Velocity velocity = new Velocity(0, 0);

            g = e.Graphics;

        }

        

        public void Turn(int offSet)
        {
            if (offSet == 0)
                selection = 4;

            Point currentPos = moves[moves.Length - 1];
            Point[] possibleMoves = calcPossibleMoves(currentPos);

            isConfirmed = false;
            //check if selection is valid

            
            Global.game.refreshMap();

            DrawOptions(possibleMoves, selection, currentPos);
        }

        private Point[] calcPossibleMoves(Point currentPos)
        {
            Point[] possibleMoves = new Point[9];

            //Add collision detection
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    possibleMoves[x * 3 + y] = new Point(currentPos.X + currentV.X + (x - 1) * gridSpacing, currentPos.Y + currentV.Y + (y - 1) * gridSpacing);

            return possibleMoves;
        }

        private void DrawOptions(Point[] possibleMoves, int selection, Point currentPos)
        {
            for (int i = 0; i < 9; i++)
                if (possibleMoves[i] != null)
                    g.DrawLine(pen, Global.game.convertPoint(currentPos), Global.game.convertPoint(possibleMoves[i]));
        }

        
    }
}
