using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tic_Tac_Toe_2.ViewLogic;

namespace Tic_Tac_Toe_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Visualizer visualizer;
        public MainWindow()
        {
            InitializeComponent();
            visualizer = new Visualizer(GameField, log);
         
        }

        private void GameField_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            visualizer.drawTurn(e.GetPosition(GameField).X, e.GetPosition(GameField).Y);
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            visualizer.cleanBoard();
        }
    }
}
