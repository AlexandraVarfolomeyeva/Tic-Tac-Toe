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

        private static readonly SolidColorBrush fieldBackBrush = Brushes.Blue;
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

            gameField.Children.Add(createLine(0, 0, 0, gameBoardHeight));
            gameField.Children.Add(createLine(gameBoardWidth / 3, 0, gameBoardWidth / 3, gameBoardHeight));
            gameField.Children.Add(createLine(gameBoardWidth / 3 * 2, 0, gameBoardWidth / 3 * 2, gameBoardHeight));
            gameField.Children.Add(createLine(gameBoardWidth, 0, gameBoardWidth, gameBoardHeight));

            gameField.Children.Add(createLine(0, 0, gameBoardWidth, 0));
            gameField.Children.Add(createLine(0, gameBoardHeight / 3, gameBoardWidth, gameBoardHeight/3));
            gameField.Children.Add(createLine(0, gameBoardHeight / 3 * 2, gameBoardWidth, gameBoardHeight / 3 * 2));
            gameField.Children.Add(createLine(0, gameBoardHeight, gameBoardWidth, gameBoardHeight));
        }

        private Line createLine(double fromX, double fromY, double toX, double toY)
        {
            Line myLine = new Line();

            myLine.X1 = fromX;
            myLine.Y1 = fromY;

            myLine.X2 = toX;
            myLine.Y2 = toY;
            
            myLine.Stroke = fieldBackBrush;
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
                log.Text = "Победили " + (crossZero == TurnsLogic.zero ? "нолики." : "крестики");
                return;
            }
        }

        private void drawZero(Point leftTop)
        {
            Ellipse myEllipse = new Ellipse();
            myEllipse.Width = figureSize;
            myEllipse.Height = figureSize;
            myEllipse.Stroke = Brushes.Black;
            myEllipse.StrokeThickness = 2;
            Canvas.SetLeft(myEllipse, leftTop.X + figureOffset);
            Canvas.SetTop(myEllipse, leftTop.Y + figureOffset);
            gameField.Children.Add(myEllipse); 
        }

        private void drawCross(Point leftTop)
        {
            Polyline myPolyline = new Polyline();
            myPolyline.FillRule = FillRule.Nonzero;

            Point PointTopLeft = new Point(0, 0);
            Point PointCenter = new Point(figureSize / 2, figureSize / 2);
            Point PointTopRight = new Point(0, figureSize);
            Point PointBottomLeft = new Point(figureSize, 0);
            Point PointBottomRight= new Point(figureSize, figureSize);

            PointCollection myPointCollection1 = new PointCollection();
            
            myPointCollection1.Add(PointTopRight);
            myPointCollection1.Add(PointBottomLeft);
            myPointCollection1.Add(PointTopLeft);
            myPointCollection1.Add(PointBottomRight);

            myPolyline.Points = myPointCollection1;


            myPolyline.Width = figureSize;
            myPolyline.Height = figureSize;
            myPolyline.Stroke = Brushes.Black;
            myPolyline.StrokeThickness = 2;
            Canvas.SetLeft(myPolyline, leftTop.X + figureOffset);
            Canvas.SetTop(myPolyline, leftTop.Y + figureOffset);
            gameField.Children.Add(myPolyline);
        }
    }
}
