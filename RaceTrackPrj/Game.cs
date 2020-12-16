using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RaceTrackPrj
{
    class Game
    {
        public int currentPlayer { get; private set; }
        public Player[] players { get; private set; }
        public bool isGameOver { get; private set; } = false;

        private Map map;
        private int gridSpacing;
        private Point startPos;

        public Game(Map m, int g, Point s, PaintEventArgs e)
        {
            map = m;
            gridSpacing = g;
            startPos = s;

            //code to create players/AI
            

            // TEMP DELETE DELETE
            int noOfPlayers = 1;


            players = new Player[noOfPlayers];
            if (noOfPlayers == 1)
                players[0] = new Player(startPos, new Pen(Color.Green, 3), gridSpacing, e);

            currentPlayer = 0;

            players[currentPlayer].Turn(0);
        }

        
    }
}
