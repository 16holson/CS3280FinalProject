using CS3280FinalProject.Items;
using CS3280FinalProject.Search;
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

namespace CS3280FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuSearch_Click(object sender, RoutedEventArgs e)
        {
            wndSearch Search = new wndSearch();
            Search.Owner = this;  //this sets it so that the search window owner is this window (so it loads where this window is currently)

            this.Hide();  //hide this from the user
            Search.ShowDialog();  //open the search window and pause here in the code until the window is closed
            this.Show();  //shows this window to the user
        }

        private void MenuEditItems_Click(object sender, RoutedEventArgs e)
        {
            wndItems EditItems = new wndItems();  //make a new instance of the edit items window
            EditItems.Owner = this;  //this sets it so that the edit items window owner is this window (so it loads where this window is currently)

            this.Hide();  //hide this from the user
            EditItems.ShowDialog();  //open the edit items window and pause here in the code until the window is closed
            this.Show();  //shows this window to the user
        }
    }
}
