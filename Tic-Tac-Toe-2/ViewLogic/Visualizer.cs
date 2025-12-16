using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Tic_Tac_Toe_2.ViewLogic
{
    public class Visualizer
    {
        private readonly Canvas gameField;
        private readonly TextBlock log;
        private readonly double gameBoardWidth;
        private readonly double gameBoardHeight;
        private readonly double boxWidth;
        private readonly double boxHeight;
        private readonly double figureSize;
        private TurnsLogic turn;

        private const int lineThickness = 2;
        private readonly double figureOffset;

        public Visualizer(Canvas canvas, TextBlock log)
        {
            gameField = canvas;
            this.log = log;

            gameBoardWidth = canvas.Width;
            gameBoardHeight = canvas.Width;

            boxWidth = gameBoardWidth / 3;
            boxHeight = gameBoardHeight / 3;
            gameBoardHeight = canvas.Height;
            InitializeField();
            figureSize = gameBoardWidth / 4;
            figureOffset = (boxWidth - figureSize) / 2;
            turn = new TurnsLogic();
        }

        public void cleanBoard()
        {
            gameField.Children.RemoveRange(8, gameField.Children.Count);
            turn.cleanBoard();
            log.Text = "";
        }

        private void InitializeField()
        {
            Brush fieldBackBrush = Brushes.Blue;


            gameField.Children.Add(createLine(0, 0, 0, gameBoardHeight, fieldBackBrush));
            gameField.Children.Add(createLine(gameBoardWidth / 3, 0, gameBoardWidth / 3, gameBoardHeight, fieldBackBrush));
            gameField.Children.Add(createLine(gameBoardWidth / 3 * 2, 0, gameBoardWidth / 3 * 2, gameBoardHeight, fieldBackBrush));
            gameField.Children.Add(createLine(gameBoardWidth, 0, gameBoardWidth, gameBoardHeight, fieldBackBrush));

            gameField.Children.Add(createLine(0, 0, gameBoardWidth, 0, fieldBackBrush));
            gameField.Children.Add(createLine(0, gameBoardHeight / 3, gameBoardWidth, gameBoardHeight/3, fieldBackBrush));
            gameField.Children.Add(createLine(0, gameBoardHeight / 3 * 2, gameBoardWidth, gameBoardHeight / 3 * 2, fieldBackBrush));
            gameField.Children.Add(createLine(0, gameBoardHeight, gameBoardWidth, gameBoardHeight, fieldBackBrush));
        }

        private Line createLine(double fromX, double fromY, double toX, double toY, Brush brush)
        {
            Line myLine = new Line();

            myLine.X1 = fromX;
            myLine.Y1 = fromY;

            myLine.X2 = toX;
            myLine.Y2 = toY;
            myLine.Stroke = brush;
            myLine.StrokeThickness = lineThickness;

            return myLine;
        }

        private Point getBoxLeftTop(int xBox,int yBox)
        {
            double xCenter, yCenter;
            xCenter = xBox * boxWidth;
            yCenter = yBox * boxHeight;
            return new Point(xCenter, yCenter);
        }

        public void drawTurn(double xClickedLocation, double yClickedLocation)
        {
            int x, y;

            (x, y) = turn.GetBoxNumber(xClickedLocation, yClickedLocation, boxWidth, boxHeight);

            Point leftTop = getBoxLeftTop(x, y);
            int crossZero = turn.makeMove(x, y);
            if (crossZero == TurnsLogic.cross)
            {
                drawCross(leftTop);
            }
            else if (crossZero == TurnsLogic.zero)
            {
                drawZero(leftTop);
            }

            if (crossZero !=0 && turn.checkWin())
            {
                log.Text = "Победили " + (crossZero == TurnsLogic.zero ? "нолики." : "крестики.");
                return;
            }
        }

        private void drawZero(Point leftTop)
        {
            Ellipse myEllipse = new Ellipse();
            myEllipse.Width = figureSize;
            myEllipse.Height = figureSize;
            myEllipse.Stroke = Brushes.Black;
            myEllipse.StrokeThickness = lineThickness;
            Canvas.SetLeft(myEllipse, leftTop.X + figureOffset);
            Canvas.SetTop(myEllipse, leftTop.Y + figureOffset);
            gameField.Children.Add(myEllipse); 
        }

        private void drawCross(Point leftTop)
        {
            double offsetX = figureOffset + leftTop.X;
            double offsetY = figureOffset + leftTop.Y;

            Point PointTopLeft = new Point(0 + offsetX, 0 + offsetY);
            Point PointBottomLeft = new Point(0 + offsetX, figureSize + offsetY);
            Point PointTopRight = new Point(figureSize + offsetX, offsetY);
            Point PointBottomRight= new Point(figureSize + offsetX, figureSize + offsetY);

            gameField.Children.Add(createLine(PointTopRight.X, PointTopRight.Y, PointBottomLeft.X, PointBottomLeft.Y, Brushes.Black));
            gameField.Children.Add(createLine(PointTopLeft.X, PointTopLeft.Y, PointBottomRight.X, PointBottomRight.Y, Brushes.Black));
        }
    }
}
