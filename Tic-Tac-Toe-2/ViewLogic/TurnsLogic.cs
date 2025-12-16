using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tic_Tac_Toe_2.ViewLogic
{
    public class TurnsLogic
    {
        public static int zero = 1;
        public static int cross = -1;

        private int[,] board;

        public TurnsLogic()
        {
            board = new int[3,3];
            board.Initialize();
        }

        public (int, int) GetBoxNumber(double xClickedLocation, double yClickedLocation, double boxWidth, double boxHeight)
        {
            int x = (int) (xClickedLocation / boxWidth);
            int y = (int)(yClickedLocation / boxHeight);
            return (x, y);
        }

        private bool checkIfBoxIsEmpty(int x,int y)
        {
            return board[x, y] == 0;
        }

        private int checkWhoseTurn()
        {
            return board.Cast<int>().Sum() == 0 ? cross : zero;
        }

        public int makeMove(int x, int y)
        {
            if (!checkWin() && checkIfBoxIsEmpty(x, y))
            {
                board[x, y] = checkWhoseTurn();
                return board[x, y];
            }
            return 0;
        }

        public void cleanBoard()
        {
            for (int i = 0; i < board.GetLength(1); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    board[i, j] = 0;
                }
            }
        }

        public bool checkWin() {
            if (Math.Abs(board[0, 0] + board[1, 1] + board[2, 2]) == 3
                || Math.Abs(board[0, 2] + board[1, 1] + board[2, 0]) == 3
                || Math.Abs(board[0, 0] + board[0, 1] + board[0, 2]) == 3
                || Math.Abs(board[1, 0] + board[1, 1] + board[1, 2]) == 3
                || Math.Abs(board[2, 0] + board[2, 1] + board[2, 2]) == 3
                || Math.Abs(board[0, 0] + board[1, 0] + board[2, 0]) == 3
                || Math.Abs(board[0, 1] + board[1, 1] + board[2, 1]) == 3
                || Math.Abs(board[0, 2] + board[1, 2] + board[2, 2]) == 3) 
            {
                return true;
            }
            return false; 
        }       
    }
}
